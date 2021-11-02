/*
 * ShopToggle.cs
 * Handles toggling of shop via shop switch in inventory
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleShop : MonoBehaviour
{
    Toggle shopToggle;

    void Start(){
        shopToggle = GetComponent<Toggle>();
        shopToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(shopToggle);
            shopToggle.interactable = false;
            Invoke("ResetCooldown", 0.5f);
        });
    }

    void ResetCooldown(){
        shopToggle.interactable = true;
    }

    void ToggleValueChanged(Toggle change)
    {
        InventoryAndShopController.Instance.PopulateStructuresTab();
    }
}
