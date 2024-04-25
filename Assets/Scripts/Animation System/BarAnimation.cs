using Spine.Unity;
using UnityEngine;

public class BarAnimation : AbstractAnimation
{
    [SerializeField] private AnimationReferenceAsset _idle, _death;
    public void SetCaracterState(string state)
    {
        if (state.Equals("idle"))
            SetAnimation(_idle, true, 1f, "bar");
        else if (state.Equals("death"))
            SetAnimation(_death, true, 1f, "bar");
    }
}
