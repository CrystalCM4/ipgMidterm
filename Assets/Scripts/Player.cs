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

    private Animator sprites;

    private float timer;
    private float atkSpd = 0;
    private float atkSpdTimer;
    private bool shot;
    private Vector3 bullTarg;
    private bool secBub = false;
    private bool thirdBub = false;

    //character unique health;
    public static int levHealth = 4;
    public static int nicHealth = 3;
    public static int someHealth = 2;
    public static int tiHealth = 3;
    public static int rainHealth = 4;
    public static int remeHealth = 5;

    private bool hit = false;
    private float iFrameTime = 1;
    private Color alpha; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRef = gameObject;
        character = 1;

        //get child object's animator
        sprites = transform.GetChild(0).gameObject.GetComponent<Animator>();
        sprites.SetInteger("char",1);

        //get child object's sprite's color
        alpha = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {   
        //lev stuff
        GameObject secBull;
        GameObject thirdBull;
        bullTarg = new Vector3(100, transform.position.y, transform.position.z);
        atkSpdTimer += Time.deltaTime;

        //iframes
        if (hit){
            alpha.a = 0.5f;
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = alpha;
            iFrameTime -= Time.deltaTime;
            if (iFrameTime <= 0){
                alpha.a = 1;
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = alpha;
                hit = false;
                iFrameTime = 1;
            }
        }

        //character death manager
        if (character == 1 && levHealth <= 0){
            character = 2;
            sprites.SetInteger("char",2);
        }
        if (character == 2 && nicHealth <= 0){
            character = 3;
            sprites.SetInteger("char",3);
        }
        if (character == 3 && someHealth <= 0){
            character = 4;
            sprites.SetInteger("char",4);
        }
        if (character == 4 && tiHealth <= 0){
            character = 5;
            sprites.SetInteger("char",5);
        }
        if (character == 5 && rainHealth <= 0){
            character = 6;
            sprites.SetInteger("char",6);
        }
        if (character == 6 && remeHealth <= 0){
            character = 1;
            sprites.SetInteger("char",1);
        }

        //all dead
        if (levHealth <= 0 && nicHealth <= 0 && someHealth <= 0
        && tiHealth <= 0 && rainHealth <= 0 && remeHealth <= 0){
            print("game over");
        }



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
        if (Input.GetKeyDown(KeyCode.Alpha1) && levHealth > 0)
        {
            character = 1;
            sprites.SetInteger("char",1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && nicHealth > 0)
        {
            ResetBubbles();
            character = 2;
            sprites.SetInteger("char",2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && someHealth > 0)
        {
            ResetBubbles();
            character = 3;
            sprites.SetInteger("char",3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && tiHealth > 0)
        {
            ResetBubbles();
            character = 4;
            sprites.SetInteger("char",4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && rainHealth > 0)
        {
            ResetBubbles();
            character = 5;
            sprites.SetInteger("char",5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && remeHealth > 0)
        {
            ResetBubbles();
            character = 6;
            sprites.SetInteger("char",6);
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
                Instantiate(bull6, new Vector3(transform.position.x + 25,
                4, transform.position.z), Quaternion.Euler(0,90,0));
                break;
        }
    }

    public void ResetBubbles(){
        shot = false;
        secBub = false;
        thirdBub = false;
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Enemy")){

            hit = true;

            if (character == 1){
                levHealth -= 1;
            }

            else if (character == 2){
                nicHealth -= 1;
            }

            else if (character == 3){
                someHealth -= 1;
            }

            else if (character == 4){
                tiHealth -= 1;
            }

            else if (character == 5){
                rainHealth -= 1;
            }

            else if (character == 6){
                remeHealth -= 1;
            }
        }
    }
}
