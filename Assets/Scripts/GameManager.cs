using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    public static int wave;
    public static float spawnTimer;
    public static int score = 0;

    public GameObject levIcon;
    public GameObject nicIcon;
    public GameObject someIcon;
    public GameObject tiIcon;
    public GameObject rainIcon;
    public GameObject remeIcon;

    public TextMeshProUGUI levHp;
    public TextMeshProUGUI nicHp;
    public TextMeshProUGUI someHp;
    public TextMeshProUGUI TiHp;
    public TextMeshProUGUI RainHp;
    public TextMeshProUGUI RemeHp;

    public TextMeshProUGUI scoreText;

    //increase stat
    public static double damageAdd = 0;
    public static double damageMult = 1;
    public static double enemyHealthMult = 1;

    //upgrades
    public Upgrades damage1;
    public Upgrades damage2;
    public Upgrades damage3;
    public Upgrades damage4;
    public Upgrades damage5;
    public Upgrades damage6;

    public Upgrades health1;
    public Upgrades health2;

    public Upgrades special1;
    public Upgrades special2;
    public Upgrades special3;
    public Upgrades special4;
    public Upgrades special5;
    public Upgrades special6;
    public Upgrades special7;

    public GameObject upgradeScreen;
    private List<Upgrades> upgradeList = new();
    public static List<Upgrades> specialList = new();
    public static Upgrades[] options = new Upgrades[3];

    public static bool turretSpecial;
    public static bool megaPunch;
    public static bool globalPassive;
    public static bool mcSyndrome;
    public static bool catchTheRat;
    public static bool tasteTheRainbow;
    public static bool bubbleHunter;

    public static int megaPunchSizeMult;
    public static int megaPunchDamageMult;
    public static int catchTheRatSpeedMult;
    public static int catchTheRatDamageMult;
    public static int tasteTheRainbowMult;

    public static float upgradeTimer;
    public static float upgradeTimerBase = 20;
    public static bool upgradePause = false;


    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        //Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wave = 0;
        score = 0;
        spawnTimer = 4;
        upgradeTimer = upgradeTimerBase;

        //recolor icons
        levIcon.GetComponent<Image>().color = Color.white;
        nicIcon.GetComponent<Image>().color = Color.white;
        someIcon.GetComponent<Image>().color = Color.white;
        tiIcon.GetComponent<Image>().color = Color.white;
        rainIcon.GetComponent<Image>().color = Color.white;
        remeIcon.GetComponent<Image>().color = Color.white;


        //put upgrades in list
        upgradeList = new List<Upgrades>{
            damage1, damage2, damage3, damage4, damage5, damage6,
            health1, health2
        };

        specialList = new List<Upgrades>{
            special1, special2, special3, special4, special5, special6, special7
        };

        for (int i = 0; i < specialList.Count; i ++){
            specialList[i].chosen = false;
        }

        //reset upgrade booleans
        turretSpecial = false;
        megaPunch = false;
        globalPassive = false;
        mcSyndrome = false;
        catchTheRat = false;
        tasteTheRainbow = false;
        bubbleHunter = false;

        megaPunchSizeMult = 1;
        megaPunchDamageMult = 1;
        catchTheRatDamageMult = 1;
        catchTheRatSpeedMult = 1;
        tasteTheRainbowMult = 1;

    }

    void FixedUpdate()
    {
        spawnTimer -= Time.deltaTime;
        upgradeTimer -= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {   
        //print(upgradeTimer);

        //dead icons
        Color death = Color.red;
        if (Player.levHealth <= 0){
            levIcon.GetComponent<Image>().color = death;
        }
        else levIcon.GetComponent<Image>().color = Color.white;

        if (Player.nicHealth <= 0){
            nicIcon.GetComponent<Image>().color = death;
        }
        else nicIcon.GetComponent<Image>().color = Color.white;

        if (Player.someHealth <= 0){
            someIcon.GetComponent<Image>().color = death;
        }
        else someIcon.GetComponent<Image>().color = Color.white;

        if (Player.tiHealth <= 0){
            tiIcon.GetComponent<Image>().color = death;
        }
        else tiIcon.GetComponent<Image>().color = Color.white;

        if (Player.rainHealth <= 0){
            rainIcon.GetComponent<Image>().color = death;
        }
        else rainIcon.GetComponent<Image>().color = Color.white;

        if (Player.remeHealth <= 0){
            remeIcon.GetComponent<Image>().color = death;
        }
        else remeIcon.GetComponent<Image>().color = Color.white;

        //display hp
        levHp.text = Player.levHealth.ToString() + " HP";
        nicHp.text = Player.nicHealth.ToString() + " HP";
        someHp.text = Player.someHealth.ToString() + " HP";
        TiHp.text = Player.tiHealth.ToString() + " HP";
        RainHp.text = Player.rainHealth.ToString() + " HP";
        RemeHp.text = Player.remeHealth.ToString() + " HP";

        scoreText.text = "Score: " + score.ToString();


        //choose three random upgrade options
        if (upgradeTimer <= 0 && !upgradePause){
            
            //make enemies stronger
            enemyHealthMult += score / 1000;

            int randomSelect;

            //choose three random things from the list. there can be overlaps            
            for (int i = 0; i < options.Length; i ++){

                randomSelect = Random.Range(0, upgradeList.Count + 1); //+1 is for special

                if (randomSelect != upgradeList.Count){
                    options[i] = upgradeList[randomSelect];
                    //print("option " + i + " is filled with " + upgradeList[randomSelect].upgradeName);
                }
                //choose random special option
                else {

                    print("special chosen");

                    //if every special upgrade is chosen, pick a normal upgrade
                    int chosenCount = 0;
                    for (int j = 0; j < specialList.Count; j ++){
                        if (specialList[j].chosen){
                            chosenCount += 1;
                        }
                    }
                    if (chosenCount == specialList.Count){
                        randomSelect = Random.Range(0,upgradeList.Count);
                        options[i] = upgradeList[randomSelect];
                    }

                    //pick special upgrade
                    else {
                        bool valid = false;
                        int randomSpecial = Random.Range(0,specialList.Count);
                        
                        while (!valid){
                            if (!specialList[randomSpecial].chosen){
                                //choose
                                options[i] = specialList[randomSpecial];
                                valid = true;
                            }
                            else {
                                //if this special is already chosen, rerandomize
                                randomSpecial = Random.Range(0,specialList.Count);
                            }
                        }
                    }
                }
            }
            upgradePause = true;
        }

        //upgrade screen
        if (upgradePause){
            //upgrade pause screen
            upgradeScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else {
            //reset upgrade timer
            Time.timeScale = 1;
            upgradeScreen.SetActive(false);
        }

        //upgrade check
        if (megaPunch){
            megaPunchSizeMult = 3;
            megaPunchDamageMult = 6;
        }

        if (globalPassive){

            int reviveHealth = Random.Range(1,6); //1~5
            if (Player.levHealth <= 0) Player.levHealth = reviveHealth;
            if (Player.nicHealth <= 0) Player.nicHealth = reviveHealth;
            if (Player.someHealth <= 0) Player.someHealth = reviveHealth;
            if (Player.tiHealth <= 0) Player.tiHealth = reviveHealth;
            if (Player.rainHealth <= 0) Player.rainHealth = reviveHealth;
            if (Player.remeHealth <= 0) Player.remeHealth = reviveHealth;
            globalPassive = false;
        }

        if (mcSyndrome){
            int newHealth = Player.levHealth + Player.nicHealth + Player.someHealth + Player.tiHealth + Player.rainHealth + Player.remeHealth;
            if (Player.character == 1){
                Player.levHealth = newHealth;
                Player.nicHealth = 0;
                Player.someHealth = 0;
                Player.tiHealth = 0;
                Player.rainHealth = 0;
                Player.remeHealth = 0;
            }
            else if (Player.character == 2){
                Player.levHealth = 0;
                Player.nicHealth = newHealth;
                Player.someHealth = 0;
                Player.tiHealth = 0;
                Player.rainHealth = 0;
                Player.remeHealth = 0;
            }
            else if (Player.character == 3){
                Player.levHealth = 0;
                Player.nicHealth = 0;
                Player.someHealth = newHealth;
                Player.tiHealth = 0;
                Player.rainHealth = 0;
                Player.remeHealth = 0;
            }
            else if (Player.character == 4){
                Player.levHealth = 0;
                Player.nicHealth = 0;
                Player.someHealth = 0;
                Player.tiHealth = newHealth;
                Player.rainHealth = 0;
                Player.remeHealth = 0;
            }
            else if (Player.character == 5){
                Player.levHealth = 0;
                Player.nicHealth = 0;
                Player.someHealth = 0;
                Player.tiHealth = 0;
                Player.rainHealth = newHealth;
                Player.remeHealth = 0;
            }
            else if (Player.character == 6){
                Player.levHealth = 0;
                Player.nicHealth = 0;
                Player.someHealth = 0;
                Player.tiHealth = 0;
                Player.rainHealth = 0;
                Player.remeHealth = newHealth;
            }
            mcSyndrome = false;
        }
        
        if (catchTheRat){
            catchTheRatDamageMult = 2;
            catchTheRatSpeedMult = 2;
        }

        if (tasteTheRainbow){
            tasteTheRainbowMult = 0;
        }
    }

    void LateUpdate()
    {   
        //spawn random wave
        if (spawnTimer <= 0){
            wave = Random.Range(0,5);
            spawnTimer = 4;
        }
    }
}
