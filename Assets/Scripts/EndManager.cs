using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    public void RestartButton(){

        //load scene
        SceneManager.LoadScene("TitleScreen",LoadSceneMode.Single);
    }
}
