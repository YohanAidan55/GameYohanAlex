using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{

    private int mode;

    public void PlayMode1()
    {
        mode = 1;
        PlayerPrefs.SetInt("mode", mode);
        SceneManager.LoadScene("game");
    }
    
    public void PlayMode2()
    {
        mode = 2;
        PlayerPrefs.SetInt("mode", mode);
        SceneManager.LoadScene("game");
    }
}
