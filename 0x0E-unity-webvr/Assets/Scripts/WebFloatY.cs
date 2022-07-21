using Zinnia.Action;
using WebXR;
using Unity;

public class WebFloatY : FloatAction
{
    public WebXRController controller;
    private float yAxis;

    void Update()
    {
        var value = controller.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick);
        yAxis = value.y;
        Receive(yAxis);
    }
}
