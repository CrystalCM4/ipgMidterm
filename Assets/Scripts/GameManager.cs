using TMPro;
using UnityEngine;
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
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wave = 0;
        spawnTimer = 5;
    }

    void FixedUpdate()
    {
        spawnTimer -= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {   
        //print(spawnTimer);

        //dead icons
        Color death = Color.red;
        if (Player.levHealth <= 0){
            levIcon.GetComponent<Image>().color = death;
        }
        if (Player.nicHealth <= 0){
            nicIcon.GetComponent<Image>().color = death;
        }
        if (Player.someHealth <= 0){
            someIcon.GetComponent<Image>().color = death;
        }
        if (Player.tiHealth <= 0){
            tiIcon.GetComponent<Image>().color = death;
        }
        if (Player.rainHealth <= 0){
            rainIcon.GetComponent<Image>().color = death;
        }
        if (Player.remeHealth <= 0){
            remeIcon.GetComponent<Image>().color = death;
        }

        //display hp
        levHp.text = Player.levHealth.ToString() + " HP";
        nicHp.text = Player.nicHealth.ToString() + " HP";
        someHp.text = Player.someHealth.ToString() + " HP";
        TiHp.text = Player.tiHealth.ToString() + " HP";
        RainHp.text = Player.rainHealth.ToString() + " HP";
        RemeHp.text = Player.remeHealth.ToString() + " HP";

        scoreText.text = "Score: " + score.ToString();

    }

    void LateUpdate()
    {
        if (spawnTimer <= 0){
            wave = Random.Range(0,5);
            spawnTimer = 10;
        }
    }
}
