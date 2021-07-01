using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyDisplay : MonoBehaviour {
    public Text currencyText;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    // Sets the display to the current currency amount. 
    public void SetCurrencyText(long currency) {
        currencyText.text = currency.ToString();
    }
}
