using UnityEngine;

public class AttachPlayer : MonoBehaviour
{
    public GameObject player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = transform;
            Debug.Log("Attaching");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player.transform.parent = null;
        Debug.Log("Se fue");
    }
}
