using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour {

    public int level = 1; // TEMPORARY TILL BETTER SOLUTION

    public Text levelText;
    public LevelBar levelBar;

    // Start is called before the first frame update
    void Start() {
        levelBar.SetExperienceNeeded(50f);
    }

    // Update is called once per frame **** TEMPORARY *****
    void Update() {

        levelText.text = level.ToString();

        if (Input.GetKeyDown(KeyCode.Space)) {
            levelBar.SetCurrentExperience(levelBar.GetCurrentProgressExperience() + 1f);

            if (levelBar.GetCurrentProgressExperience() >= (level * 50f)) {
                level++;
                levelBar.SetExperienceNeeded(50f * level);
            }
        }

    }

    // Sets the display to the current level. 
    void setLevelText(string level) {
        levelText.text = level.ToString();
    }
}
