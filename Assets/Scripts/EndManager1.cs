using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager1 : MonoBehaviour
{
    public void RestartButton(){

        //load scene
        SceneManager.LoadScene("SampleScene",LoadSceneMode.Single);

        //GameManager.Instance.RestartGame();
    }
}
