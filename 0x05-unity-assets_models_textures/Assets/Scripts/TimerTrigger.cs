using UnityEngine;

public class TimerTrigger : MonoBehaviour
{    public GameObject player, timeTrigger; 
 
    void Update()
    {
        if (player.transform.position.x < -1f || player.transform.position.x > 1f)
        {
            player.GetComponent<Timer>().enabled = true;
            timeTrigger.SetActive(false);
        }
    }
}
