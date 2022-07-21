using Zinnia.Action;
using WebXR;

public class WebButtonB : BooleanAction
{
    public WebXRController controller;

    void Update()
    {
        Receive(controller.GetButton(WebXRController.ButtonTypes.ButtonB));
    }
}
