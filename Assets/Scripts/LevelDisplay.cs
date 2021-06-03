using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{

    public Text levelText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame **** TEMPORARY *****
    void Update()
    {
    }

    // Sets the display to the current level. 
    public void SetLevelText(ulong level)
    {
        levelText.text = level.ToString();
    }
}