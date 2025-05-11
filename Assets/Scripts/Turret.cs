using Unity.Mathematics;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private float shootTimer;
    public GameObject turretBullet;
    private GameObject FollowPlayer{ get; set; }
    public AudioSource shootSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FollowPlayer = Player.playerRef;

        shootTimer = 2;
    }

    // Update is called once per frame
    void Update()
    {   
        transform.position = new Vector3(FollowPlayer.transform.position.x + 3,
        FollowPlayer.transform.position.y + 2, FollowPlayer.transform.position.z);

        //turret disappears when ti is dead
        if (Player.tiHealth <= 0){
            gameObject.GetComponent<Renderer>().enabled = false;
        }
        else {
            
            gameObject.GetComponent<Renderer>().enabled = true;

            //turret behavior
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0){

                //shoot bullet
                shootSound.Play();
                Instantiate(turretBullet, new Vector3(transform.position.x + 2.5f,
                transform.position.y, transform.position.z), quaternion.identity);
                shootTimer = 2;
            }
        }
    }
}
