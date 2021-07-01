using UnityEngine;
using UnityEngine.UI;

public class InventoryAndShopController : MonoBehaviour {

    // Toggle object mechanics. 
    [SerializeField] RectTransform uiHandleRectTransform;
    public Toggle toggleShopOrInventory;
    Vector2 handlePosition;

    // Panel mechanics
    public GameObject inventoryPanel;

    // Modified objects
    public Text panelTitle;

    public Inventory inventory;

    private void Awake() {

        handlePosition = uiHandleRectTransform.anchoredPosition;

        toggleShopOrInventory.onValueChanged.AddListener(OnSwitch);

        if (toggleShopOrInventory.isOn) {
            OnSwitch(true);
        }
    }

    void OnSwitch(bool on) {
        if (on) {
            Debug.Log("Toggled Shop");
            uiHandleRectTransform.anchoredPosition = handlePosition * -1;
            panelTitle.text = "Shop";
        } else {
            Debug.Log("Toggled Inventory");
            uiHandleRectTransform.anchoredPosition = handlePosition;
            panelTitle.text = "Inventory";
        }
    }

    private void OnDestroy() {
        toggleShopOrInventory.onValueChanged.RemoveListener(OnSwitch);
    }

    public void ShowHideMenu() {
        if (inventoryPanel != null) {
            Animator animator = inventoryPanel.GetComponent<Animator>();

            if (animator != null) {
                bool isOpen = animator.GetBool("ShowInventory");
                animator.SetBool("ShowInventory", !isOpen);
            }
        }
    }

    public void OpenResourcesTab() {
        inventory.PopulateResourcesTab();
    }

    public void OpenStructuresTab() {
        inventory.PopulateStructuresTab();
    }

    public void OpenDecorTab() {
        inventory.PopulateDecorTab();
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
