using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    Animator anim;
    public GameObject player, timerCanvas, mainCamera, secondCamera;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (transform.position == new Vector3(0, 2.5f, -6.25f))
        {
            player.GetComponent<PlayerStateMachine>().enabled = true;
            player.GetComponent<AudioListener>().enabled = true;
            player.GetComponent<CharacterController>().enabled = true;
            timerCanvas.SetActive(true);
            mainCamera.SetActive(true);
            this.gameObject.SetActive(false);
            secondCamera.SetActive(true);
        }
    }

}
