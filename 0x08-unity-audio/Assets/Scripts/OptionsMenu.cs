using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Toggle yAxis;
    public Slider bgm, sfx;
    public AudioMixer masterMixer;
    public AudioMixerSnapshot normal;
    float bgmVolume = 1f, sfxVolume = 1f;

    void Start()
    {
        normal.TransitionTo(0);
        if (yAxis != null)
            yAxis.isOn = PlayerPrefs.GetInt("invertY") == 0 ? false : true;
        bgm.value = PlayerPrefs.GetFloat("bgmVolume");
        sfx.value = PlayerPrefs.GetFloat("sfxVolume");
    }
    void Update()
    {

        Cursor.visible = true;
        masterMixer.SetFloat("volumeBGM", ToDecibel(bgmVolume));
        masterMixer.SetFloat("volumeSFX", ToDecibel(sfxVolume));
    }

    public void UpdateBGMVolume(float vol) => bgmVolume = vol;
    public void UpdateSFXVolume(float vol) => sfxVolume = vol;

    public void Back()
    {
        normal.TransitionTo(0);
        SceneManager.LoadScene(PlayerPrefs.GetString("previousScene"));
    }

    public void Apply()
    {
        PlayerPrefs.SetInt("invertY", yAxis.isOn == false ? 0 : 1);
        PlayerPrefs.SetFloat("bgmVolume", bgmVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
    }

    float ToDecibel(float n)
    {
        float dB;
        dB = n != 0 ? 20.0f * Mathf.Log10(n) : -144.0f;

        return dB;
    }
}
