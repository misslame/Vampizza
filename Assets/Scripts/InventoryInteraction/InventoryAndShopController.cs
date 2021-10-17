using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryAndShopController : MonoBehaviour,  IDeselectHandler, IPointerEnterHandler, IPointerExitHandler {

    // Singleton stuff
    private static InventoryAndShopController instance = null;
    public static InventoryAndShopController Instance { // MUST BE READ-ONLY DO NOT ADD A SET PROPERTY. 
        get { return instance; } 
    }

    //PLAYER'S PERSONAL INVENTORY OF ITEMS (STRUCTURES/RESOURCES)
    private ItemInventory inventory = new ItemInventory();

    // Toggle object mechanics. 
    [SerializeField] private RectTransform uiHandleRectTransform;
    [SerializeField] private Toggle toggleShopOrInventory;
    private Vector2 handlePosition;

    public Toggle ShopAndInventoryToggle {
        get { return toggleShopOrInventory; }
    }

    // Modified objects
    [SerializeField] private Text panelTitle;
    private string currentTab;

    // Slot-related
    [SerializeField] private GridLayoutGroup slotHolder;
    [SerializeField] private GameObject slotInventoryCopy;
    [SerializeField] private GameObject slotShopCopy;
    private bool mouseIsOver = false;
    private void Awake() {
        if(instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }

        handlePosition = uiHandleRectTransform.anchoredPosition;
        toggleShopOrInventory.onValueChanged.AddListener(OnSwitch);
        if (toggleShopOrInventory.isOn) {
            OnSwitch(true);
        }

        hideSlot(slotInventoryCopy);
        hideSlot(slotShopCopy);

        inventory.Init();

        PopulateResourcesTab();
    }
    public void OnDeselect(BaseEventData eventData)
    {
        Animator animator = gameObject.GetComponent<Animator>();
        bool isOpen = animator.GetBool("ShowInventory");
        if (isOpen && !mouseIsOver)
            animator.SetBool("ShowInventory", false);
    }
 
    public void OnPointerEnter(PointerEventData eventData) {
        mouseIsOver = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData) {
        mouseIsOver = false;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    private void hideSlot(GameObject slot){
        // Hide component via set alpha to 0 and prevent receiving of input events
        slot.GetComponent<CanvasGroup>().alpha = 0f;
        slot.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    // Use to show newly cloned slots derived from slotCopy, which will eventually be deleted
    private void showSlot(GameObject slot){
        // Show component via set alpha to 1 and allow receiving of input events
        slot.GetComponent<CanvasGroup>().alpha = 1f;
        slot.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    void OnSwitch(bool on) {
        if (on) {
            // Debug.Log("Toggled Shop");
            uiHandleRectTransform.anchoredPosition = handlePosition * -1;
            panelTitle.text = "Shop";
        } else {
            // Debug.Log("Toggled Inventory");
            uiHandleRectTransform.anchoredPosition = handlePosition;
            panelTitle.text = "Inventory";
        }
    }

    private void OnDestroy() {
        toggleShopOrInventory.onValueChanged.RemoveListener(OnSwitch);
    }

    public void ShowHideMenu() {
        Animator animator = gameObject.GetComponent<Animator>();
        bool isOpen = animator.GetBool("ShowInventory");

        if (animator != null) {
            animator.SetBool("ShowInventory", !isOpen);
            if (currentTab == "Structures" && !isOpen){
                PopulateStructuresTab();
            }
        }
        if (!isOpen && !mouseIsOver){
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }

    public void OpenResourcesTab() {
        PopulateResourcesTab();
    }

    public void OpenStructuresTab() {
        PopulateStructuresTab();
    }

    public void OpenDecorTab() {
        PopulateDecorTab();
    }

    public void PopulateResourcesTab(){
        
        currentTab = "Resources";
        
        Sprite newSprite;
        GameObject newSlot;

        EmptyInventoryPanel();

        foreach(Item item in inventory) {
            if (item.GetType().IsSubclassOf(typeof(Resource))) {
                newSlot = Instantiate(slotInventoryCopy, transform);
                newSlot.GetComponent<SlotInteraction>().SlotContent = item;
                showSlot(newSlot);
                newSlot.transform.SetParent(slotHolder.transform);

                newSprite = Resources.Load<Sprite>(item.GetImageURL());
                newSlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = newSprite;
                newSlot.transform.GetChild(0).gameObject.GetComponent<Image>().preserveAspect = true;

                Resource cast = (Resource)item;
                newSlot.transform.GetChild(1).gameObject.GetComponent<Text>().text = "x" + cast.GetQuantity();
            }
        }
    }

    public void PopulateStructuresTab() {
        currentTab = "Structures";
        EmptyInventoryPanel();

        bool shopMode = this.GetComponentInParent<InventoryAndShopController>().toggleShopOrInventory.isOn;
        Sprite newSprite;
        GameObject newSlot;
        GameObject slotCopy;

        foreach (Item item in inventory) {
            if(item.GetType().IsSubclassOf(typeof(Structure))) {
                if (!shopMode && Player.Instance.structureQuantities[item.GetType().ToString()] == 0) {
                    continue;
                }
                slotCopy = shopMode ? slotShopCopy:slotInventoryCopy;

                //Create a new slot gameobj from slotCopy, then put the corresponding item object into DragDrop.SlotContent
                newSlot = Instantiate(slotCopy, transform);
                newSlot.GetComponent<SlotInteraction>().SlotContent = item;

                // Show slot, then update transform
                showSlot(newSlot);
                newSlot.transform.SetParent(slotHolder.transform);

                if (shopMode) {
                    // Do sprite stuff
                    newSprite = Resources.Load<Sprite>(item.GetImageURL());
                    newSlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = newSprite;

                    slotHolder.cellSize = new Vector2(250f, 80f);

                    newSlot.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Text>().text = ((Structure)item).GetPrice().ToString();
                    newSlot.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<Text>().text = Player.Instance.structureQuantities[item.GetType().ToString()].ToString();

                    // Fancy Color changing for the text for feedback. 
                    if(Player.Instance.CurrentCurrency >= ((Structure)item).GetPrice()) {
                        newSlot.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.green; // If player can afford, make text color green. 
                    } else {
                        newSlot.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.red; // If player cannot afford, make text color red. 
                    }

                    if(Player.Instance.structureQuantities[item.GetType().ToString()] <= 0) {
                        newSlot.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.red; // If they have 0 of this item in their inventory. 
                    }
                } else {
                    if (Player.Instance.structureQuantities[item.GetType().ToString()] == 0) {
                        continue;
                    }

                    slotHolder.cellSize = new Vector2(80f, 80f);

                    // Do sprite stuff
                    newSprite = Resources.Load<Sprite>(item.GetImageURL());
                    newSlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = newSprite;
                    newSlot.transform.GetChild(1).gameObject.GetComponent<Text>().text = "x" + Player.Instance.structureQuantities[item.GetType().ToString()];
                }
            }
        }
        
    }

    public void PopulateDecorTab() {
        currentTab = "Decor";
        /* DEBUG: temporary action. Will eventually implement official "empty list" */

        EmptyInventoryPanel();

        // GameObject newSlot = Instantiate(slotCopy, transform);
        // newSlot.transform.GetChild(1).gameObject.GetComponent<Text>().text = " ";
    }
    void EmptyInventoryPanel() {
       
        int numberOfChildren = slotHolder.transform.childCount;
        // Debug.Log(numberOfChildren);
        if (numberOfChildren >= 1) {
            // Delete all children of SlotHolder
            for (int i = 0; i < numberOfChildren; i++) {
                Destroy(slotHolder.transform.GetChild(i).gameObject);
            }
        }
    }
}
