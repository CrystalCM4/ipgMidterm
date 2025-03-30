using System.Collections.Generic;
using EnemyType;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    //enemies
    [SerializeField]
    private GameObject genericPrefab;

    [SerializeField]
    private GameObject player;

    //waves
    private WaveData waveNum;
    private ParentEnemy randomEnem;
    private GameData gameData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //read info from json
        TextAsset json = Resources.Load<TextAsset>("waves");
        if (json != null) {
            gameData = JsonUtility.FromJson<GameData>(json.text);
            
            if (gameData == null) {
                Debug.LogError("failed to parse GameData from json");
            }
        }

        else {
            Debug.LogError("waves.json doesnt exist");
        }

        //wave always starts at 0
        waveNum = gameData.wave0;
        
        if (waveNum == null) {
            Debug.LogError("wave0 doesnt exist");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.spawnTimer <= 0){

            //spawn enemy
            GameObject enemy = Instantiate(genericPrefab, 
            new Vector3(transform.position.x, 2.5f, transform.position.z), Quaternion.identity);

            int wave = GameManager.wave;

            //set enemy type

            //spawn enemies depending on wave
            if (wave == 0) {
                waveNum = gameData.wave0;
                //Debug.Log("wave0: " + (waveNum != null ? "exists" : "doesnt exist"));
            }
            else if (wave == 1) {
                waveNum = gameData.wave1;
                //Debug.Log("wave1: " + (waveNum != null ? "exists" : "doesnt exist"));
            }
            else if (wave == 2) {
                waveNum = gameData.wave2;
                //Debug.Log("wave2: " + (waveNum != null ? "exists" : "doesnt exist"));
            }
            else if (wave == 3) {
                waveNum = gameData.wave3;
                //Debug.Log("wave3: " + (waveNum != null ? "exists" : "doesnt exist"));
            }
            else if (wave == 4) {
                waveNum = gameData.wave4;
                //Debug.Log("wave4: " + (waveNum != null ? "exists" : "doesnt exist"));
            }

            //get animator from spawned enemy gameobject
            GameObject spriteHolder = enemy.transform.GetChild(0).gameObject;
            Animator sprites = spriteHolder.GetComponent<Animator>();

            if (CompareTag("Spawner1")){
                
                //type
                if (waveNum.enemies[0].Equals("Normal")){
                    randomEnem = enemy.AddComponent<NormalEnemy>();
                    sprites.SetInteger("enem",1);
                    //print("normal enemy spawned");
                }
                else if (waveNum.enemies[0].Equals("Fire")){
                    randomEnem = enemy.AddComponent<FireEnemy>();
                    sprites.SetInteger("enem",2);
                }
                else if (waveNum.enemies[0].Equals("Mono")){
                    randomEnem = enemy.AddComponent<MonoEnemy>();
                    sprites.SetInteger("enem",3);
                }
                else if (waveNum.enemies[0].Equals("Metal")){
                    randomEnem = enemy.AddComponent<MetalEnemy>();
                    sprites.SetInteger("enem",4);
                }
                else Debug.LogError("waveNum.enemies doesnt exist");
                ParentEnemy script = randomEnem;

                if (waveNum.stats == null) {
                    Debug.LogError("waveNum.stats doesnt exist");
                }

                //stats
                script.MakeEnemy(waveNum.stats[0], waveNum.stats[1]);
                script.Player = player;
            }

            else if (CompareTag("Spawner2")){

                //type
                if (waveNum.enemies[1].Equals("Normal")){
                    randomEnem = enemy.AddComponent<NormalEnemy>();
                    sprites.SetInteger("enem",1);
                }
                else if (waveNum.enemies[1].Equals("Fire")){
                    randomEnem = enemy.AddComponent<FireEnemy>();
                    sprites.SetInteger("enem",2);
                }
                else if (waveNum.enemies[1].Equals("Mono")){
                    randomEnem = enemy.AddComponent<MonoEnemy>();
                    sprites.SetInteger("enem",3);
                }
                else if (waveNum.enemies[1].Equals("Metal")){
                    randomEnem = enemy.AddComponent<MetalEnemy>();
                    sprites.SetInteger("enem",4);
                }
                else Debug.LogError("waveNum.enemies doesnt exist");
                ParentEnemy script = randomEnem;

                //stats
                script.MakeEnemy(waveNum.stats[2], waveNum.stats[3]);
                script.Player = player;
            }

            else if (CompareTag("Spawner3")){

                //type
                if (waveNum.enemies[2].Equals("Normal")){
                    randomEnem = enemy.AddComponent<NormalEnemy>();
                    sprites.SetInteger("enem",1);
                }
                else if (waveNum.enemies[2].Equals("Fire")){
                    randomEnem = enemy.AddComponent<FireEnemy>();
                    sprites.SetInteger("enem",2);
                }
                else if (waveNum.enemies[2].Equals("Mono")){
                    randomEnem = enemy.AddComponent<MonoEnemy>();
                    sprites.SetInteger("enem",3);
                }
                else if (waveNum.enemies[2].Equals("Metal")){
                    randomEnem = enemy.AddComponent<MetalEnemy>();
                    sprites.SetInteger("enem",4);
                }
                else Debug.LogError("waveNum.enemies doesnt exist");
                ParentEnemy script = randomEnem;

                //stats
                script.MakeEnemy(waveNum.stats[4], waveNum.stats[5]);
                script.Player = player;
            }
        }
    }
}

//enemy spawn stuff is here
[System.Serializable]
public class WaveData {
    public int[] stats;
    public string[] enemies;
}

[System.Serializable]
public class GameData {
    public WaveData wave0;
    public WaveData wave1;
    public WaveData wave2;
    public WaveData wave3;
    public WaveData wave4;
}


//enemy type stuff is all below here

public struct EnemyStats {
    public int hp;
    public int spd;

    public EnemyStats(int h, int s) {
        hp = h;
        spd = s;
    }
}

namespace EnemyType {

    //parent enemy - enemies inherit from this
    public class ParentEnemy : MonoBehaviour{
        public EnemyStats enemy;

        [SerializeField]
        private NavMeshAgent enemyNav;
        
        private TextMeshProUGUI hpText;

        private int scoreHolder;

        //getter setter for player gameobject
        public GameObject Player{ get; set; }

        public virtual void MakeEnemy(int h, int s){
            enemy.hp = h;
            enemy.spd = s;   
        }

        public virtual void TakeDamage(string bullType, int bullDmg){
            enemy.hp -= bullDmg;
        }

        void Start()
        {
            enemyNav = GetComponent<NavMeshAgent>();
            enemyNav.speed = enemy.spd;

            hpText = transform.GetChild(1).gameObject
            .transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            scoreHolder = enemy.hp;
        }
        
        void Update()
        {
            enemyNav.SetDestination(Player.transform.position);
            hpText.text = enemy.hp.ToString();

            if (enemy.hp <= 0){
                Destroy(gameObject);
                GameManager.score += scoreHolder;
            }
        }

        void OnCollisionEnter(Collision col) {
            if (col.gameObject.CompareTag("Bullet")){
                string incomingBT = col.gameObject.GetComponent<Bullets>().bullType;
                int incomingBD = col.gameObject.GetComponent<Bullets>().bullDmg;

                TakeDamage(incomingBT, incomingBD);   
            }
        }
        
    }

    //normal enemy - weak to strike damage / ignores ghost damage
    public class NormalEnemy : ParentEnemy{
        
        //weakness list
        public List<string> weakness = new();

        public override void MakeEnemy(int h, int s){
            base.MakeEnemy(h, s);
            weakness.Add("Strike");
        }

        public override void TakeDamage(string bullType, int bullDmg){

            int totalDmg = bullDmg;

            //ignores ghost damage
            if (bullType.Equals("Ghost")){
                totalDmg = 0;
            }

            //check if enemy is weak to player's current bullet type
            for (int i = 0; i < weakness.Count; i ++){
                if (weakness[i].Equals(bullType)){
                    
                    //weakness damage is doubled
                    totalDmg *= 2;

                }
            }
            

            base.TakeDamage(bullType, totalDmg);
        }
    }

    //fire enemy - weak to water damage / resists metal damage
    public class FireEnemy : ParentEnemy{

        //weakness list
        public List<string> weakness = new();
        public List<string> resist = new();

        public override void MakeEnemy(int h, int s){
            base.MakeEnemy(h, s);
            weakness.Add("Water");
            resist.Add("Metal");
        }

        public override void TakeDamage(string bullType, int bullDmg){
            
            int totalDmg = bullDmg;

            //check if enemy is weak to player's current bullet type
            for (int i = 0; i < weakness.Count; i ++){
                if (weakness[i].Equals(bullType)){
                    
                    //weakness damage is doubled
                    totalDmg *= 2;

                }
            }

            //check if enemy resists player's current bullet type
            for (int i = 0; i < resist.Count; i ++){
                if (resist[i].Equals(bullType)){
                    
                    //resist damage is halved
                    totalDmg *= 1/2;

                }
            }

            base.TakeDamage(bullType, totalDmg);
        }
    }

    //mono enemy - weak to pastel and strike damage
    public class MonoEnemy : ParentEnemy{

        //weakness list
        public List<string> weakness = new();

        public override void MakeEnemy(int h, int s){
            base.MakeEnemy(h, s);
            weakness.Add("Pastel");
            weakness.Add("Strike");
        }

        public override void TakeDamage(string bullType, int bullDmg){
            
            int totalDmg = bullDmg;

            //check if enemy is weak to player's current bullet type
            for (int i = 0; i < weakness.Count; i ++){
                if (weakness[i].Equals(bullType)){
                    
                    //weakness damage is doubled
                    totalDmg *= 2;

                }
            }

            base.TakeDamage(bullType, totalDmg);
        }
    }

    //metal enemy - weak to electric and strike damage
    public class MetalEnemy : ParentEnemy{

        //weakness list
        public List<string> weakness = new();

        public override void MakeEnemy(int h, int s){
            base.MakeEnemy(h, s);
            weakness.Add("Electric");
            weakness.Add("Strike");
        }

        public override void TakeDamage(string bullType, int bullDmg){
            
            int totalDmg = bullDmg;

            //check if enemy is weak to player's current bullet type
            for (int i = 0; i < weakness.Count; i ++){
                if (weakness[i].Equals(bullType)){
                    
                    //weakness damage is doubled
                    totalDmg *= 2;

                }
            }

            base.TakeDamage(bullType, totalDmg);
        }
    }

}
