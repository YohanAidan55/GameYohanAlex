using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    bool isActive;
    GameObject menuPause;

    void Start()
    {
        isActive = false;
        menuPause = GameObject.Find("MenuPause");
    }
    
    void Update()
    {
        if (isActive)
        {
            Time.timeScale = 0;
            menuPause.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            menuPause.SetActive(false);
        }
    }

    public void Resume()
    {
        isActive = !isActive;
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("menu");
    }
}

