using UnityEngine;

public class TimerTrigger : MonoBehaviour
{    
    public GameObject player, timeTrigger; 

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<Timer>().enabled = true;
            timeTrigger.SetActive(false);
        }
    }
}
