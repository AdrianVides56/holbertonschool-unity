using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource rollover;
    public AudioSource click;
    public AudioMixer masterMixer;

    void Start()
    {
            masterMixer.SetFloat("volumeBGM", ToDecibel(PlayerPrefs.GetFloat("bgmVolume")));
            masterMixer.SetFloat("volumeSFX", ToDecibel(PlayerPrefs.GetFloat("sfxVolume")));
    }

    public void ButtonRollover()
    {
        rollover.Play();
    }

    public void ButtonClick()
    {
        click.Play();
    }

    float ToDecibel(float n)
    {
        float dB;
        dB = n != 0 ? 20.0f * Mathf.Log10(n) : -144.0f;

        return dB;
    }
}
