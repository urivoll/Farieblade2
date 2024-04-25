using Spine;
using Spine.Unity;
using UnityEngine;

public class BulletAnimation : AbstractAnimation
{
    [SerializeField] private AnimationReferenceAsset _idle, _death;
    public void SetCaracterState(string state)
    {
        if (state.Equals("idle"))
            SetAnimation(_idle, true, 1f, "bullet");
        else if (state.Equals("death"))
            SetAnimation(_death, false, 1f, "bullet").Complete += AnimationEntry_Complete;
    }
    private void AnimationEntry_Complete(TrackEntry trackEntry) { Destroy(gameObject); }
}
