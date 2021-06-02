using UnityEngine;
using UnityEngine.UI;

public class CitizenDisplay : MonoBehaviour{
    public Text citizenText;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    // Sets the display to the current citizen amount. 
    public void SetCitizenText(ulong citizen){
        citizenText.text = citizen.ToString();
    }
}
