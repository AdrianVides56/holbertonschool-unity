using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource rollover;
    public void ButtonRollover()
    {
        rollover.Play();
    }
}
