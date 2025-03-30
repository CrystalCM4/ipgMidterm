using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent enemy;

    [SerializeField]
    private GameObject player;

    [HideInInspector]
    public Animator sprites;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get child object's animator
        sprites = transform.GetChild(0).gameObject.GetComponent<Animator>();
        sprites.SetInteger("enem",1);
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(player.transform.position);
    }
}
