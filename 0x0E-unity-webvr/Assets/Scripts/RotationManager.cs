using UnityEngine;

public class RotationManager : MonoBehaviour
{
    public GameObject snap, smooth;

    public void Snap()
    {
        try
        {
            snap.SetActive(true);
            smooth.SetActive(false);
        }
        catch (System.Exception)
        {
            Debug.Log("Snap button not found");
        }
    }

    public void Smooth()
    {
        try
        {
            snap.SetActive(false);
            smooth.SetActive(true);
        }
        catch (System.Exception)
        {
            Debug.Log("Smooth button not found");
        }
    }
}
