using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlider : MonoBehaviour {


    public GameObject inventoryPanel;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void ShowHideMenu() {
        if(inventoryPanel != null) {
            Animator animator = inventoryPanel.GetComponent<Animator>();

            if(animator != null) {
                bool isOpen = animator.GetBool("ShowInventory");
                animator.SetBool("ShowInventory", !isOpen);
            }
        }
    }
}
