using Zinnia.Action;
using WebXR;

public class WebButtonA : BooleanAction
{
    public WebXRController controller;

    void Update()
    {
        Receive(controller.GetButton(WebXRController.ButtonTypes.ButtonA));
    }
}
