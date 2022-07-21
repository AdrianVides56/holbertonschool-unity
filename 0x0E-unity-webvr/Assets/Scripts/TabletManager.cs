using UnityEngine;
using WebXR;
using Zinnia.Action;

public class TabletManager : MonoBehaviour
{
    public GameObject tablet;
    public WebXRController leftController, rightController;
    public Vector3 leftTabletOffset, rightTabletOffset;

    bool check;

    void Update()
    {
        if (leftController.GetButtonDown(WebXRController.ButtonTypes.ButtonA))
        {
            check = IsTabletEnabled();
            EnableTablet("left");
        }
        else if (leftController.GetButtonUp(WebXRController.ButtonTypes.ButtonA))
        {
            DisableTablet();
        }

        if (rightController.GetButtonDown(WebXRController.ButtonTypes.ButtonA))
        {
            check = IsTabletEnabled();
            EnableTablet("right");
        }
        else if (rightController.GetButtonUp(WebXRController.ButtonTypes.ButtonA))
        {
            DisableTablet();
        }
    }

    public void EnableTablet(string hand)
    {
        tablet.SetActive(true);
        if (hand == "left")
        {
            tablet.transform.SetParent(leftController.transform);
            tablet.transform.localPosition = leftTabletOffset;
        }
        else if (hand == "right")
        {
            tablet.transform.SetParent(rightController.transform);
            tablet.transform.localPosition = rightTabletOffset;
        }
    }

    public void DisableTablet()
    {
        tablet.SetActive(false);
    }

    public bool IsTabletEnabled()
    {
        if (tablet.activeSelf)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
