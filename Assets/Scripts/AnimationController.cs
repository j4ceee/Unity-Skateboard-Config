using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator flipAnimator;
    public UIManager uiManager;
    private static readonly int IsFlipped = Animator.StringToHash("isFlipped");

    public void FlipAnimation()
    {
        bool currentStateFlip = flipAnimator.GetBool(IsFlipped);
        flipAnimator.SetBool(IsFlipped, !currentStateFlip);
        uiManager.ShowUI("none");
    }
}
