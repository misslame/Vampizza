using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //CONSTANTS
    const float EXP_MODIFIER = 50f;

    //PRIVATES
    private PlayerData data;

    //PLAYER ATTRIBUTES
    public float exp {
        get { return data.exp; }
        set { data.exp = value; }
    }

    public ulong level {
        get { return data.level; }
        set { data.level = value; }
    }

    public long currency {
        get { return data.currency; }
        set { data.currency = value; }
    }

    public string playerName {
        get { return data.playerName; }
        set { data.playerName = value; }
    }

    public ulong citizen {
        get { return data.citizen; }
        set { data.citizen = value; }
    }

    public ulong blood {
        get { return data.blood; }
        set { data.blood = value; }
    }

    //UI ELEMENT REFERENCES
    public LevelBar levelBar;
    public LevelDisplay levelDisplay;
    public CitizenDisplay citizenDisplay;
    [SerializeField] public CurrencyDisplay currencyDisplay;

    //CUSTOM CONTRUCTOR

    public Player() {
        data = GameState.GetPlayerData();

        exp = 0f;
        level = 1;
        currency = 0;
        citizen = 100;
        blood = 0;
        playerName = "Chuck";
    }


    // Start is called before the first frame update
    void Start() {
        exp = 0f;
        level = 1;
        currency = 1000000;
        citizen = 100;
        blood = 0;
        playerName = "Chuck";
        levelBar.SetExperienceNeeded(EXP_MODIFIER * level);
        levelDisplay.SetLevelText(level);
        citizenDisplay.SetCitizenText(citizen);
        currencyDisplay.SetCurrencyText(currency);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            ChangeExp(5000);
        }
    }

    public void ChangeExp(float expMod) {
        float temp;
        temp = (exp + expMod) - (EXP_MODIFIER * level);
        if (temp >= 0) {
            level++;
            exp = 0f;
            levelDisplay.SetLevelText(level);
            levelBar.SetExperienceNeeded(EXP_MODIFIER * level);
            ChangeExp(temp);
        } else {
            exp += expMod;
            levelBar.SetCurrentExperience(exp);
        }
    }

    public float GetExp() { return exp; }

    public ulong GetLevel() { return level; }
}
