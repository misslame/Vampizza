using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructureModeToggle : MonoBehaviour
{
    // Start is called before the first frame update
    Toggle toggle;
    void Start(){
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(ToggleValueChanged);
    }

    void ToggleValueChanged(bool isOn){
        ColorBlock cb = toggle.colors;
         if (isOn)
         {
             cb.normalColor = Color.gray;
             cb.highlightedColor = Color.gray;
         }
         else
         {
             cb.normalColor = Color.white;
             cb.highlightedColor = Color.white;
         }
         toggle.colors = cb;
    }

}
