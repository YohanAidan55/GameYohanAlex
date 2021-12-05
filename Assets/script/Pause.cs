using UnityEngine;

public class Pause : MonoBehaviour
{
    bool isActive;

    void Start()
    {
        isActive = false;
    }
    
    void Update()
    {
        if (isActive)
        {
            Time.timeScale = 0;
        }else
        {
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        isActive = !isActive;
    }
}

