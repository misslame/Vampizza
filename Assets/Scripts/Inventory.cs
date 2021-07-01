using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public GridLayoutGroup slotHolder;
    public GameObject firstSlot;

    // Start is called before the first frame update
    void Start() {

        GameObject newSlot;

        for(int i = 0; i < 5; i++) {
            newSlot = Instantiate(firstSlot, transform);
            newSlot.transform.parent = slotHolder.transform; 
        }

    }

    // Update is called once per frame
    void Update() { }
}
