using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuLose : MonoBehaviour
{
    public int score;

    [SerializeField]
    AudioSource backMusic;

    void Start()
    {
        GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().mute = false;

        if (PlayerPrefs.GetInt("Mute") == 0)
        {
            GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = false;
        }
    }

    void Update()
        {
            score = PlayerPrefs.GetInt("score");
            GameObject.Find("ScoreFinal").GetComponent<Text>().text = "Score: " +score;
        }

    public void Play()
        {
            RunAd();
            SceneManager.LoadScene("game");
        }

        public void MainMenu()
        {
            RunAd();
            SceneManager.LoadScene("menu");
        }

        private void RunAd()
        {
            if (PlayerPrefs.GetInt("deadCount") == 2)
            {
                GameObject.Find("AdsButton").GetComponent<InterstitialAdsButton>().ShowAd();
            }
            else if (PlayerPrefs.GetInt("deadCount") > 2)
            {
                PlayerPrefs.SetInt("deadCount", 0);
            }
        }
    }
