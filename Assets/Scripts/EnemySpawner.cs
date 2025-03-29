using System.Collections.Generic;
using EnemyType;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    //enemies
    [SerializeField]
    private GameObject genericPrefab;

    [SerializeField]
    private GameObject player;

    private float timer;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 5;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0){

            //spawn enemy
            GameObject enemy = Instantiate(genericPrefab, 
            new Vector3(transform.position.x, 2.5f, transform.position.z), Quaternion.identity);

            //enemy type
            ParentEnemy script = enemy.AddComponent<NormalEnemy>();
            script.MakeEnemy(5,3);
            script.Player = player;

            timer = 10;
        }

    }
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
        }
        
        void Update()
        {
            enemyNav.SetDestination(Player.transform.position);

            if (enemy.hp <= 0){
                Destroy(gameObject);
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
