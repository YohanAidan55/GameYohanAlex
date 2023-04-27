using UnityEngine;
using UnityEngine.UI;

public class radialProgress : MonoBehaviour
{
    public float time;
    public Text ProgressIndicator;
    public Image LoadingBar;
    void Update()
    {
        if (time >= 0)
        {
            GameObject.Find("LoadingBar").GetComponent<Image>().enabled = true;
            GameObject.Find("Center").GetComponent<Image>().enabled = true;
            //gameObject.GetComponent<Text>().enabled = true;

            float seconds = Mathf.FloorToInt(time % 60);
            //ProgressIndicator.text = seconds.ToString() + 's';
            LoadingBar.fillAmount = seconds / 10;
            time -=  Time.deltaTime;
        }
        else
        {
            GameObject.Find("LoadingBar").GetComponent<Image>().enabled = false;
            GameObject.Find("Center").GetComponent<Image>().enabled = false;
            //gameObject.GetComponent<Text>().enabled = false;
        }
    }
}
