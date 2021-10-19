using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource rollover;
    public AudioSource click;

    public void ButtonRollover()
    {
        rollover.Play();
    }

    public void ButtonClick()
    {
        click.Play();
    }
}
