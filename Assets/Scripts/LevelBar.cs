using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour
{

    public Slider levelSlider;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    // Sets the experience needed before leveling up to the passed in float value
    public void SetExperienceNeeded(float experience)
    {
        levelSlider.maxValue = experience;
        levelSlider.value = 0;

    }

    // Sets the current value for experience to the passed in float value
    public void SetCurrentExperience(float experience)
    {
        levelSlider.value = experience;
    }

    // Retrieves current experience progress. 
    public float GetCurrentProgressExperience()
    {
        return levelSlider.value;
    }

}