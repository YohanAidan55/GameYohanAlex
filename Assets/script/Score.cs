using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int sc;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Score").GetComponent<Text>().text = "Score : " + sc;  //affichage du score  
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Score").GetComponent<Text>().text = "Score : " + sc;
        PlayerPrefs.SetInt("score", sc);
    }
}
