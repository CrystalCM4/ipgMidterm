using Unity.Mathematics;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private float shootTimer;
    public GameObject turretBullet;
    private GameObject FollowPlayer{ get; set; }

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

        //spawn when you get the turret upgrade
        if (Player.tiHealth > 0){
            gameObject.SetActive(true);
        }
        else gameObject.SetActive(false);

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0){

            //shoot bullet
            Instantiate(turretBullet, new Vector3(transform.position.x + 2.5f,
            transform.position.y, transform.position.z), quaternion.identity);
            shootTimer = 2;
        }
    }
}
