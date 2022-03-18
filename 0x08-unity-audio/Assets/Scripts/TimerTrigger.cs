using UnityEngine;

public class TimerTrigger : MonoBehaviour
{    
    public GameObject player; 

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<Timer>().enabled = true;
            gameObject.SetActive(false);
        }
    }
}
