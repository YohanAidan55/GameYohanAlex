using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class menuLose : MonoBehaviour
{
    public int score;

    [SerializeField]
    AudioSource backMusic;

    InterstitialAdsButton addButton;

    void Start()
    {
        addButton = GameObject.Find("AdsButton").GetComponent<InterstitialAdsButton>();

        Debug.Log("Menu lose");
        GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().mute = false;

        if (PlayerPrefs.GetInt("Mute") == 0)
        {
            GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().mute = false;
        }
        else
        {
            GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().mute = true;
        }

        
    }

    void Update()
        {
            score = PlayerPrefs.GetInt("score");
            GameObject.Find("ScoreFinal").GetComponent<Text>().text = "Score: " +score;
        }

    public void Play()
    {
        PlayerPrefs.SetInt("deadCount", PlayerPrefs.GetInt("deadCount") + 1);
        Debug.Log(PlayerPrefs.GetInt("deadCount"));
        addButton.RunAd("game");
    }

    public void MainMenu()
    {
        addButton.RunAd("menu");
    }

}



