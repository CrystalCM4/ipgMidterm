using EnemyType;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    
    private float timer = 0;
    private int projectileSpeed = 50;
    private float rotate = 0;

    [SerializeField]
    private GameObject subGO;

    [HideInInspector]
    public string bullType;

    [HideInInspector]
    public int bullDmg;

    private AudioSource enemyHit;
    private AudioSource enemyCrit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyHit = GameObject.Find("GameManager").GetComponents<AudioSource>()[1];
        enemyCrit = GameObject.Find("GameManager").GetComponents<AudioSource>()[2];
    }

    // Update is called once per frame
    void Update()
    {
        bullType = "Metal";
        bullDmg = 4;
        
        //projectileSpeed = 0;

        rotate -= 400 * Time.deltaTime;
        subGO.transform.rotation = Quaternion.Euler(0, 0, rotate);
        transform.rotation = Quaternion.Euler(0, 90, 0);

        if (timer <= 0.6){
            transform.Translate(0.0f, 0.0f, projectileSpeed * Time.deltaTime);
        }
        else if (timer > 0.6){
            transform.Translate(0.0f, 0.0f, -projectileSpeed * Time.deltaTime);
        }   
        else Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col) {

        if (col.gameObject.CompareTag("Turret")){
            Destroy(gameObject);
            timer = 0;
        }

        if (col.gameObject.CompareTag("Wall")){
            if (timer >= 0.6) Destroy(gameObject);
            else timer = 0.6f;
        }

        if (col.gameObject.CompareTag("Enemy")){

            for (int i = 0; i < col.gameObject.GetComponent<ParentEnemy>().weakness.Count; i ++){
                if (col.gameObject.GetComponent<ParentEnemy>().weakness[i].Equals(bullType)){
                    enemyCrit.Play();
                }
                else enemyHit.Play();
            }

            if (enemyHit.time >= 100 || enemyCrit.time >= 100){
                Destroy(gameObject);
            }
        }
    }
}
