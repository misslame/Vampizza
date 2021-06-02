using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    //CONSTANTS
    const float EXP_MODIFIER = 50f;

    //PLAYER ATTRIBUTES
    public float exp;
    public ulong level;
    public long currency;
    public string playerName;

    //UI ELEMENT REFERENCES
    public LevelBar levelBar;
    public LevelDisplay levelDisplay;


    //CUSTOM CONTRUCTOR
    public Player(){
        exp = 0f;
        level = 1;
        currency = 0;
        playerName = "Chuck";
    }


    // Start is called before the first frame update
    void Start(){
        exp = 0f;
        level = 1;
        currency = 0;
        playerName = "Chuck";
        levelBar.SetExperienceNeeded(EXP_MODIFIER * level);
        levelDisplay.SetLevelText(level);
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            ChangeExp(5);
        }
    }

    public void ChangeExp(float expMod){
        float temp;
        temp = (exp + expMod) - (EXP_MODIFIER * level);
        if(temp >=0){
            level++;
            exp = 0f;
            levelDisplay.SetLevelText(level);
            levelBar.SetExperienceNeeded(EXP_MODIFIER * level);
            ChangeExp(temp);
        }
        else{
            exp += expMod;
            levelBar.SetCurrentExperience(exp);
        }
    }

    public float GetExp() { return exp; }

    public ulong GetLevel() { return level; }
}
