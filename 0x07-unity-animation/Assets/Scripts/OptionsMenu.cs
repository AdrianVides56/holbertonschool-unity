using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle yAxis;

    void Start()
    {
        if (yAxis != null)
        {
            if (PlayerPrefs.GetInt("invertY") == 0)
                yAxis.isOn = false;
            else
                yAxis.isOn = true;
        }
    }
    void Update()
    {
        Cursor.visible = true;
    }
    public void Back()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("previousScene"));
    }

    public void Apply()
    {
        if (yAxis.isOn == false)
        {
            PlayerPrefs.SetInt("invertY", 0);
            Debug.Log("invertY Is Off");
        }
        else
        {
            PlayerPrefs.SetInt("invertY", 1);
            Debug.Log("invertY Is On");
        }
    }
}
