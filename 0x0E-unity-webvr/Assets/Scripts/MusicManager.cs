using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicManager : MonoBehaviour
{
    //public TextMesh volumeText;
    public TextMeshProUGUI volumeText;

    public void VolumeUp(float value)
    {
        AudioListener.volume += value;
        AudioListener.volume = Mathf.Clamp(AudioListener.volume, 0, 1);
        volumeText.text = $"Vol: {AudioListener.volume.ToString()}";
    }

    public void VolumeDown(float value)
    {
        AudioListener.volume -= value;
        AudioListener.volume = Mathf.Clamp(AudioListener.volume, 0, 1);
        volumeText.text = $"Vol: {AudioListener.volume.ToString()}";
    }
}
