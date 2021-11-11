using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;

    public void startAnimation() => animator.SetBool("Found", true);

    public void endAnimation() => animator.SetBool("Found", false);

    public void GoToLink(string link) => Application.OpenURL(link);
}
