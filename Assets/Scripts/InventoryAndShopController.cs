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

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
