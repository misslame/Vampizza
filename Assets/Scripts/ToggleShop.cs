using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ToggleShop : MonoBehaviour
{
    [SerializeField] GameObject InventoryGameObj;
    Toggle shopToggle;
    void Start(){
        shopToggle = GetComponent<Toggle>();
        shopToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(shopToggle);
        });
    }
    void ToggleValueChanged(Toggle change)
    {
        Debug.Log("toggled");
        InventoryGameObj.GetComponent<Inventory>().PopulateStructuresTab();
    }
}
