using System.Collections.Generic;
using EnemyType;
using UnityEngine;

public struct Enemy {
    public int hp;
    public int spd;
    public List<string> weak;

    public Enemy(int h, int s, List<string> w) {
        hp = h;
        spd = s;
        weak = w;
    }
}

namespace EnemyType {

    //parent enemy - enemies inherit from this
    public class ParentEnemy : MonoBehaviour{
        public Enemy enemy;

        public ParentEnemy(int h, int s, List<string> w)
        {
            enemy = new(h, s, w);
        }

        public virtual void TakeDamage(string bullType, int bullDmg){
            
            bool weak = false;
            int weakMult = 1;

            //check if enemy is weak to player's current bullet type
            for (int i = 0; i < enemy.weak.Count; i ++){
                if (enemy.weak[i].Equals(bullType)){
                    weak = true;
                }
            }

            if (weak) { weakMult = 2; }

            enemy.hp -= bullDmg * weakMult;
        }
    }

    //normal enemy - takes neutral damage
    public class NormalEnemy : ParentEnemy{

        public NormalEnemy(int h, int s, List<string> w) : base(h, s, w){}

        public override void TakeDamage(string bullType, int bullDmg){
            base.TakeDamage(bullType, bullDmg);
        }
    }

    //fire enemy - weak to water damage
    public class FireEnemy : ParentEnemy{

        public FireEnemy(int h, int s, List<string> w) : base(h, s, w){}

        public override void TakeDamage(string bullType, int bullDmg){
            base.TakeDamage(bullType, bullDmg);
        }
    }
}


public class EnemyBehavior : MonoBehaviour
{
    NormalEnemy normEnem;
    FireEnemy fireEnem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        normEnem = new NormalEnemy(5, 120, new());
        fireEnem = new FireEnemy(5, 120, new());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
