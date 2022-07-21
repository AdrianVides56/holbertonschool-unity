using UnityEngine;

public class LocomotionManager : MonoBehaviour
{
    public GameObject slide, teleport;

    public void Slide()
    {
        try
        {
            slide.SetActive(true);
            teleport.SetActive(false);
        }
        catch (System.Exception)
        {
            Debug.Log("Slide button not found");
        }
    }

    public void Teleport()
    {
        try
        {
            slide.SetActive(false);
            teleport.SetActive(true);
        }
        catch (System.Exception)
        {
            Debug.Log("Teleport button not found");
        }
    }
}
