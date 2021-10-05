using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    Animator anim;
    public GameObject player, timerCanvas, mainCamera;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (transform.position == new Vector3(0, 2.5f, -6.25f))
        {
            player.GetComponent<PlayerController>().enabled = true;
            timerCanvas.SetActive(true);
            mainCamera.SetActive(true);
            this.gameObject.GetComponent<CutsceneController>().enabled = false;
            this.gameObject.GetComponent<Camera>().enabled = false;
        }
    }

}
