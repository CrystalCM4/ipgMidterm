using EnemyType;
using UnityEngine;
using UnityEngine.UI;

public class Bullets : MonoBehaviour
{   
    private int projectileSpeed = 50;
    private float timer = 0;
    private float rotate = 0;
    private Vector3 basePos;

    private GameObject FollowPlayer{ get; set; }

    [HideInInspector]
    public Color flash;
    private GameObject ElectricFlash;
    private float flashTime = 0;


    [SerializeField]
    private GameObject subGO;

    [HideInInspector]
    public string bullType;

    [HideInInspector]
    public int bullDmg;

    private AudioSource enemyHit;
    private AudioSource enemyCrit;

    //upgrade stuff
    private Vector3 basePunchScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        basePos = transform.position;

        FollowPlayer = Player.playerRef;
        subGO = transform.GetChild(0).gameObject;

        enemyHit = GameObject.Find("GameManager").GetComponents<AudioSource>()[1];
        enemyCrit = GameObject.Find("GameManager").GetComponents<AudioSource>()[2];

        basePunchScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {   
        //destroy bullet when character switches
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2)
        || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4)
        || Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Alpha6)){
            Destroy(gameObject);
            timer = 0;
        }

        timer += Time.deltaTime;

        switch (Player.character){

            //lev
            case 1:

                bullType = "Water";
                bullDmg = 3;
                transform.Translate(0.0f, 0.0f, projectileSpeed * Time.deltaTime);

                break;

            //nicole
            case 2:

                transform.position = new Vector3(FollowPlayer.transform.position.x + 50,
                FollowPlayer.transform.position.y, FollowPlayer.transform.position.z);

                bullType = "Pastel";
                bullDmg = 4;

                //fade out
                Color laser = subGO.GetComponent<SpriteRenderer>().color;
                laser.a -= 10 * Time.deltaTime;
                subGO.GetComponent<SpriteRenderer>().color = laser;

                if (timer >= 0.1){
                    Destroy(gameObject);
                }

                break;

            //some
            case 3:

                transform.position = new Vector3(FollowPlayer.transform.position.x,
                FollowPlayer.transform.position.y, FollowPlayer.transform.position.z);

                bullType = "Electric";
                bullDmg = 1;

                //fade out
                ElectricFlash = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
                flashTime += 5 * Time.deltaTime;
                flash = Color.white;
                flash.a = 1 - flashTime;
                ElectricFlash.GetComponent<Image>().color = flash;

                if (timer >= 0.1){
                    Destroy(gameObject);
                }

                break;

            //ti
            case 4:

                bullType = "Metal";
                bullDmg = 4;

                
                //projectileSpeed = 0;

                rotate -= 400 * Time.deltaTime;
                subGO.transform.rotation = Quaternion.Euler(0, 0, rotate);

                if (timer <= 0.6){
                    transform.Translate(0.0f, 0.0f, projectileSpeed * Time.deltaTime);
                }
                else if (timer > 0.6){
                    transform.Translate(0.0f, 0.0f, -projectileSpeed * Time.deltaTime);
                }   
                else Destroy(gameObject);

                break;

            //rain
            case 5:

                transform.position = new Vector3(FollowPlayer.transform.position.x,
                FollowPlayer.transform.position.y, FollowPlayer.transform.position.z);

                bullType = "Ghost";
                bullDmg = 20 * GameManager.catchTheRatDamageMult;
                if (timer >= 0.5){
                    Destroy(gameObject);
                }

                break;

            //reme       
            case 6:

                if (GameManager.megaPunch){
                    transform.position = new Vector3(basePos.x,
                    basePos.y + 5, basePos.z);
                }

                transform.localScale = new Vector3(
                    basePunchScale.x * GameManager.megaPunchSizeMult, 
                    basePunchScale.y * GameManager.megaPunchSizeMult, 
                    basePunchScale.z * GameManager.megaPunchSizeMult);

                bullType = "Strike";
                bullDmg = 10 * GameManager.megaPunchDamageMult;
                if (timer >= 0.5){
                    Destroy(gameObject);
                }

                break;
        }
        
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Wall") && Player.character == 1){
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Character") && Player.character == 4){
            Destroy(gameObject);
            timer = 0;
        }

        if (col.gameObject.CompareTag("Wall") && Player.character == 4){
            if (timer >= 0.6) Destroy(gameObject);
            else timer = 0.6f;
        }

        if (col.gameObject.CompareTag("Enemy")){
            
            if (Player.character == 1) Destroy(gameObject);

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
