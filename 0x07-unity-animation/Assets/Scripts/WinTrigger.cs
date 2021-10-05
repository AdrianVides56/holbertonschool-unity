using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public GameObject player, winTrigger, winCanvas;
    public Text timer, winText;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            Cursor.visible = true;
            Text textPosition = timer.GetComponent<Text>();
            player.GetComponent<Timer>().enabled = false;
            winTrigger.GetComponent<Collider>().enabled = false;
            winText.text = textPosition.text;
            textPosition.enabled = false;
            winCanvas.SetActive(true);
            player.GetComponent<PauseMenu>().enabled = false;
            Time.timeScale = 0f;
        }
    }
}
