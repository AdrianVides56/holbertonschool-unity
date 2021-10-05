using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook cam;

    void Update()
    {
        if (PlayerPrefs.GetInt("invertY") == 0)
        {
            cam.m_YAxis.m_InvertInput = false;
        }
        else
        {
            cam.m_YAxis.m_InvertInput = true;
        }
    }
}
