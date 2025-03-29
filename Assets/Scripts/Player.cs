using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{   
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private NavMeshAgent player;

    [HideInInspector]
    public static GameObject playerRef;

    public GameObject bull1;
    public GameObject bull2;
    public GameObject bull3;
    public GameObject bull4;
    public GameObject bull5;
    public GameObject bull6;

    [HideInInspector]
    public static int character;

    private float timer;
    private float atkSpd = 0;
    private float atkSpdTimer;
    private bool shot;
    private Vector3 bullTarg;
    private bool secBub = false;
    private bool thirdBub = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRef = gameObject;
        character = 1;
    }

    // Update is called once per frame
    void Update()
    {     
        //lev stuff
        GameObject secBull;
        GameObject thirdBull;
        bullTarg = new Vector3(100, transform.position.y, transform.position.z);
        atkSpdTimer += Time.deltaTime;

        //right click to move
        if (Input.GetMouseButtonDown(1)){

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                player.SetDestination(hit.point);
            }
        }

        //left click to shoot bullets
        if (Input.GetMouseButtonDown(0) && atkSpdTimer >= atkSpd){
            if (character == 1) {
                shot = true;
                secBub = true;
                thirdBub = true;
            }
            else if (character == 2) {
                shot = true;
            }
            ShootBullets();
        }

        if (shot){
            timer += Time.deltaTime;
            float bullFreq = 0.2f;

            //lev stuff
            if (timer >= bullFreq && secBub){
                secBull = Instantiate(bull1,transform.position, Quaternion.identity);
                secBull.transform.LookAt(bullTarg);
                secBub = false;
            }

            if (timer >= bullFreq * 2 && thirdBub){
                thirdBull = Instantiate(bull1,transform.position, Quaternion.identity);
                thirdBull.transform.LookAt(bullTarg);
                thirdBub = false;
                shot = false;
            }
        }
        

        //change characters
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            character = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ResetBubbles();
            character = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ResetBubbles();
            character = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ResetBubbles();
            character = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ResetBubbles();
            character = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ResetBubbles();
            character = 6;
        }

    }

    public void ShootBullets(){

        atkSpdTimer = 0;

        switch (character){

            //lev
            case 1:

                atkSpd = 1;
                timer = 0;
                GameObject firstBull;
                firstBull = Instantiate(bull1,transform.position, Quaternion.identity);
                firstBull.transform.LookAt(bullTarg);

                break;

            //nicole
            case 2:

                atkSpd = 0.7f;
                timer = 0;
                Instantiate(bull2, new Vector3(transform.position.x + 50,
                transform.position.y, transform.position.z), Quaternion.Euler(90f, 90f, 0f));

                break;

            //some
            case 3:

                atkSpd = 3;
                timer = 0;
                Instantiate(bull3, new Vector3(0, 0, 0), Quaternion.identity);

                break;

            //ti
            case 4:

                atkSpd = 1.2f;
                timer = 0;
                GameObject boomerang;
                boomerang = Instantiate(bull4, new Vector3(transform.position.x + 4.5f,
                transform.position.y, transform.position.z), Quaternion.identity);
                boomerang.transform.LookAt(bullTarg);

                break;

            //rain
            case 5:

                atkSpd = 1f;
                timer = 0;
                Instantiate(bull5,transform.position, Quaternion.identity);

                break;

            //reme       
            case 6:
                atkSpd = 1f;
                timer = 0;
                GameObject punch;
                punch = Instantiate(bull6, new Vector3(transform.position.x + 25,
                4, transform.position.z), Quaternion.Euler(0,90,0));
                break;
        }
    }

    public void ResetBubbles(){
        shot = false;
        secBub = false;
        thirdBub = false;
    }
}
