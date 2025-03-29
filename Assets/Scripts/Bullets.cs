using UnityEngine;

public class Bullets : MonoBehaviour
{   
    private int projectileSpeed = 50;
    private float timer = 0;

    private GameObject FollowPlayer{ get; set; }

    public GameObject player;

    [HideInInspector]
    public string bullType;

    [HideInInspector]
    public int bullDmg;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FollowPlayer = player;
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
                if (timer >= 0.1){
                    Destroy(gameObject);
                }

                break;

            //some
            case 3:

                bullType = "Electric";
                bullDmg = 1;
                if (timer >= 0.1){
                    Destroy(gameObject);
                }

                break;

            //ti
            case 4:

                bullType = "Metal";
                bullDmg = 4;
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

                transform.position = new Vector3(FollowPlayer.transform.position.x + 50,
                FollowPlayer.transform.position.y, FollowPlayer.transform.position.z);
                
                bullType = "Ghost";
                bullDmg = 20;
                if (timer >= 0.5){
                    Destroy(gameObject);
                }

                break;

            //reme       
            case 6:

                bullType = "Strike";
                bullDmg = 5;
                if (timer >= 0.5){
                    Destroy(gameObject);
                }

                break;
        }
        
    }

    void OnCollisionEnter(Collision col) {
        if ((col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Enemy"))
        && Player.character == 1){
            Destroy(gameObject);
        }

        if ((col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Character"))
        && Player.character == 4){
            Destroy(gameObject);
            timer = 0;
        }
    }
}
