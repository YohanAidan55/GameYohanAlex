using UnityEngine;

public class SpawnAnimation : MonoBehaviour
{
    public GameObject[] spawnAnimation;
    public int maxAnimation;
    private int i;
    public int startTime;
    public int maxRepeateTime;
    private int nbrAnimation;

    // Use this for initialization
    void Start ()
    {
        Invoke("spawnAnim",startTime);
    }

    void spawnAnim()
    {
        nbrAnimation = Random.Range(1, maxAnimation);
        for (int j = 0; j < nbrAnimation; j++)
        {
            Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
            i = Random.Range(0, spawnAnimation.Length);
            Instantiate(spawnAnimation[i], randomPositionOnScreen, Quaternion.identity);
        }
        Invoke("spawnAnim",Random.Range (startTime, maxRepeateTime));
    }
}
