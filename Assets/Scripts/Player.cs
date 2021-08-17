using System.Collections.Generic;
using UnityEngine;

/***********************************
 * Player Class
 *  -- Implements Singleton Pattern: 
 *     should only exist with one instance
 *     of itself. 
 *     
 * Acts as game manager class
 ***********************************/
public class Player : MonoBehaviour {

    //CONSTANTS
    const float EXP_MODIFIER = 50f;

    //PLAYER INSTANCE
    private static Player instance = null;
    public static Player Instance { // MUST BE READ-ONLY DO NOT ADD A SET PROPERTY. 
        get { return instance; } 
    }



    //PRIVATE PLAYER ATTRIBUTES
    private PlayerData data;
    private string playerName;
    private float exp;
    private ulong level;
    private ulong citizens;
    private double currency;
    private ulong blood;

    //PLAYER ATTRIBUTE PROPERTIES
    public string PlayerName { 
        get { return playerName; }
        set { playerName = value; }
    }

    public float CurrentEXP {
        get { return exp; }
        set { exp += value; }
    }

    public ulong CurrentLevel { // Read-only
        get { return level; }
    }

    public ulong CitizenCount { // Read-only
        get { return citizens; }
    }

    public double CurrentCurrency { // Read-only
        get { return currency; }
    }

    public ulong BloodCount{
        get { return blood; }
        set { blood = value; }
    }
    
    public Dictionary<string, int> structureQuantities = new Dictionary<string, int>
    {
        {"TownHome", 2},
        {"FarmPlot", 3}
    };

    //UI ELEMENT REFERENCES
    [SerializeField] private GenericBar levelBar;
    [SerializeField] private GenericBar bloodBar;
    [SerializeField] private LevelDisplay levelDisplay;
    [SerializeField] private CitizenDisplay citizenDisplay;
    [SerializeField] private CurrencyDisplay currencyDisplay;

    //CUSTOM CONTRUCTOR
    private Player() {
        data = GameState.GetPlayerData();
        exp = 0f;
        level = 1;
        currency = 0;
        citizens = 100;
        blood = 0;
        playerName = "Chuck";

    }

    private void Awake() {
        if(instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start() {
        exp = 0f;
        level = 1;
        currency = 1000;
        citizens = 100;
        blood = 0;
        playerName = "Chuck";
        levelBar.SetAmountNeeded(EXP_MODIFIER * level);
        levelDisplay.SetLevelText(level);
        citizenDisplay.SetCitizenText(citizens);
        currencyDisplay.SetCurrencyText(currency);
        bloodBar.SetAmountNeeded(level * 10);
        bloodBar.SetCurrentProgress(blood);


    }

    // Update is called once per frame
    private void Update() {
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
            levelBar.SetAmountNeeded(EXP_MODIFIER * level);
            bloodBar.SetAmountNeeded(level * 10);
            ChangeExp(temp);
        } else {
            exp += expMod;
            levelBar.SetCurrentProgress(exp);
        }
    }

    public void ApplyBloodPenalty(){
        SetCurrency(currency * 0.9);
        citizens -= level;
        citizenDisplay.SetCitizenText(citizens);
    }
    
    public void SubtractCurrency(double amount) {
        if (amount > 0 && amount <= currency) {
            currency -= amount;
        }else {
            // TO DO/ DECISION NEEDED: THROW EXCEPTION OR HANDLE UNEXPECTED OUTCOME IN SOME WAY. 
        }
        UpdateCurrencyText();
    }

    public void AddCurrency(double amount) {
        if(amount > 0) {
            currency += amount;
        }else {
            // TO DO/ DECISION NEEDED: THROW EXCEPTION OR HANDLE UNEXPECTED OUTCOME IN SOME WAY. 
        }
        UpdateCurrencyText();
    }

    public void SetCurrency(double amount){
        currency = amount;
        UpdateCurrencyText();
    }

    public void UpdateCurrencyText(){
        currencyDisplay.SetCurrencyText(Player.Instance.CurrentCurrency);
    }
}
