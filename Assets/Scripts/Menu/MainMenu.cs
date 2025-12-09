using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void loadScene(string SceneName)
    {
     
        SceneManager.LoadScene(SceneName);
    } 
}
