using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public GameObject player, winTrigger;
    public Text timer;
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            Text textPosition = timer.GetComponent<Text>();
            textPosition.transform.position += new Vector3(0, -10f, 0);
            timer.color = Color.green;
            timer.fontSize = 100;
            player.GetComponent<Timer>().enabled = false;
            winTrigger.GetComponent<Collider>().enabled = false;
        }
    }
}
