using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public GameObject player, winCanvas;
    public Text timer, winText;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            player.GetComponent<AudioSource>().Stop();
            player.GetComponent<PauseMenu>().enabled = false;
            player.GetComponent<Timer>().enabled = false;
            player.GetComponent<CharacterController>().enabled = false;

            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Collider>().enabled = false;

            Text textPosition = timer.GetComponent<Text>();
            winText.text = textPosition.text;
            textPosition.enabled = false;
        
            winCanvas.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0f;
        }
    }
}
