using Zinnia.Action;
using WebXR;
using Unity;

public class WebFloatX : FloatAction
{
    public WebXRController controller;
    private float xAxis;

    void Update()
    {
        var value = controller.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick);
        xAxis = value.x;
        Receive(xAxis);
    }
}
