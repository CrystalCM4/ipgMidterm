using System.Collections.Generic;
using EnemyType;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //enemies
    NormalEnemy normEnem;
    FireEnemy fireEnem;
    MonoEnemy monoEnem;
    MetalEnemy metalEnem;

    //variables
    [SerializeField]
    private GameObject enemy;
    private float timer;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        normEnem = new NormalEnemy(5, 120);
        fireEnem = new FireEnemy(5, 120);
        monoEnem = new MonoEnemy(5, 120);
        metalEnem = new MetalEnemy(5, 120);

        timer = 5;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0){
            //assume "enemy" takes values from one of the enemy types above
            Instantiate(enemy, transform.position, Quaternion.identity);
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
        
        public ParentEnemy(int h, int s)
        {
            enemy = new(h, s);
        }

        public virtual void TakeDamage(string bullType, int bullDmg){
            enemy.hp -= bullDmg;
        }
    }

    //normal enemy - takes neutral damage / ignores ghost damage
    public class NormalEnemy : ParentEnemy{

        public NormalEnemy(int h, int s) : base(h, s){}

        public override void TakeDamage(string bullType, int bullDmg){

            int totalDmg = bullDmg;

            //ignores ghost damage
            if (bullType.Equals("Ghost")){
                totalDmg = 0;
            }

            base.TakeDamage(bullType, totalDmg);
        }
    }

    //fire enemy - weak to water damage / resists metal damage
    public class FireEnemy : ParentEnemy{

        //weakness list
        public List<string> weakness = new();
        public List<string> resist = new();

        public FireEnemy(int h, int s): base(h, s){
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

        public MonoEnemy(int h, int s): base(h, s){
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

        public MetalEnemy(int h, int s): base(h, s){
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
