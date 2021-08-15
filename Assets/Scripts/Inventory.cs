using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    // Singleton stuff
    private static Inventory instance = null;
    public static Inventory Instance { // MUST BE READ-ONLY DO NOT ADD A SET PROPERTY. 
        get { return instance; } 
    }
    
    public Dictionary<string, int> structureQuantities = new Dictionary<string, int>
    {
        {"TownHome", 2},
        {"FarmPlot", 3}
    };

    void Awake(){
        if(instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
