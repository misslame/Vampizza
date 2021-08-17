using UnityEngine;
using UnityEngine.UI;

public class GenericBar : MonoBehaviour {

    private Slider slider;
    private Text valueText;
    [SerializeField] private bool showingText;

    void Awake() {
        slider = gameObject.GetComponent<Slider>();
        valueText = gameObject.GetComponentInChildren<Text>();
        UpdateSlider();
        if (!showingText){
            valueText.color = new Color(0f, 0f ,0f, 0f);
        }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void SetText(int num, int den){
        valueText.text = num + " / " + den;
    }
    private void UpdateSlider(){
        SetText((int)slider.value, (int)slider.maxValue);
    }

    // Sets the experience needed before leveling up to the passed in float value
    public void SetAmountNeeded(float amount) {
        slider.maxValue = amount;
        slider.value = 0;
        UpdateSlider();
    }

    // Sets the current value for experience to the passed in float value
    public void SetCurrentProgress(float amount) {
        slider.value = amount;
        UpdateSlider();
    }

    // Retrieves current experience progress. 
    public float GetCurrent() {
        return slider.value;
    }

}