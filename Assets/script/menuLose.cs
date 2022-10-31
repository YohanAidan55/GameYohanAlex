using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuLose : MonoBehaviour
{
    public int score;
        
        
        void Update()
        {
            score = PlayerPrefs.GetInt("score");
            GameObject.Find("ScoreFinal").GetComponent<Text>().text = "Score: " +score;
        }
        
        public void Play()
        {
            SceneManager.LoadScene("game");
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("menu");
        } 
    }
