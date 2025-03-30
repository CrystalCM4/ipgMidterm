using Unity.VisualScripting;
using UnityEngine;

public class Cam : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.playerRef.transform.position.x
        , 20, Player.playerRef.transform.position.z - 20);
    }
}
