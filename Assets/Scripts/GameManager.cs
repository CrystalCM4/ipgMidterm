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

    public Upgrades health1;
    public Upgrades health2;

    public Upgrades special1;
    public Upgrades special2;
    public Upgrades special3;

    public GameObject upgradeScreen;
    private List<Upgrades> upgradeList = new();
    private List<Upgrades> specialList = new();
    public static Upgrades[] options = new Upgrades[3];

    public static float upgradeTimer;
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
        spawnTimer = 5;
        upgradeTimer = 20;

        //recolor icons
        levIcon.GetComponent<Image>().color = Color.white;
        nicIcon.GetComponent<Image>().color = Color.white;
        someIcon.GetComponent<Image>().color = Color.white;
        tiIcon.GetComponent<Image>().color = Color.white;
        rainIcon.GetComponent<Image>().color = Color.white;
        remeIcon.GetComponent<Image>().color = Color.white;


        //put upgrades in list
        upgradeList = new List<Upgrades>{
            damage1, damage2, damage3, damage4,
            health1, 
        };

        specialList = new List<Upgrades>{
            special1, special2, special3
        };
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
            enemyHealthMult *= 1.5f;

            int randomSelect;

            //choose three random things from the list. there can be overlaps            
            for (int i = 0; i < options.Length; i ++){

                randomSelect = Random.Range(0,upgradeList.Count + 1); //+2 is for special

                if (randomSelect != upgradeList.Count){
                    options[i] = upgradeList[randomSelect];
                    //print("option " + i + " is filled with " + upgradeList[randomSelect].upgradeName);
                }
                //choose random special option
                else {

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
                                specialList[randomSpecial].chosen = true;
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
        
    }

    void LateUpdate()
    {   
        //spawn random wave
        if (spawnTimer <= 0){
            wave = Random.Range(0,5);
            spawnTimer = 5;
        }

        

        
    }
}
