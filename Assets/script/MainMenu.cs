using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{

    private int mode;

    public void PlayMode1()
    {
        mode = 1;
        PlayerPrefs.SetInt("mode", mode);
        PlayerPrefs.SetInt("deadCount", 0);
        SceneManager.LoadScene("game");
    }
    
    public void PlayMode2()
    {
        mode = 2;
        PlayerPrefs.SetInt("mode", mode);
        PlayerPrefs.SetInt("deadCount", 0);
        SceneManager.LoadScene("game");
    }
    
    public void loadBonusList()
    {
        SceneManager.LoadScene("listeDesBonus");
    }
}
