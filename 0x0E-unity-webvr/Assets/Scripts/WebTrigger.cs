using Zinnia.Action;
using WebXR;

public class WebTrigger : BooleanAction
{
    public WebXRController controller;

    void Update()
    {
        Receive(controller.GetButton(WebXRController.ButtonTypes.Trigger));
    }
}
