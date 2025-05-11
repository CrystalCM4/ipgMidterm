using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{

    public void StartButton1(){
        SceneManager.LoadScene("Map1",LoadSceneMode.Single);
    }

    public void StartButton2(){
        SceneManager.LoadScene("Map2",LoadSceneMode.Single);
    }

    public void StartButton3(){
        SceneManager.LoadScene("Map3",LoadSceneMode.Single);
    }
}
