using Spine;
using Spine.Unity;
using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    private SkeletonAnimation _skeletonAnimation;
    public AnimationReferenceAsset _attack;
    public AnimationReferenceAsset _idle, _hit, _death, _spell, _spell2, _spell3, _aura, _mode, _passive;
    private TrackEntry animationEntry = null;
    private UnitProperties UnitProp;
    private void Awake()
    {
        UnitProp = transform.parent.GetComponent<UnitProperties>();
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
    }
    public void SetCaracterState(string state)
    {
        if (state.Equals("idle"))
            SetAnimation(_idle, true);
        else if (state.Equals("attack"))
            SetAnimation(_attack, false);
        else if (state.Equals("hit"))
            SetAnimation(_hit, false);
        else if (state.Equals("death"))
            SetAnimation(_death, false);
        else if (state.Equals("spell"))
            SetAnimation(_spell, false);
        else if (state.Equals("spell2"))
            SetAnimation(_spell2, false);
        else if (state.Equals("spell3"))
            SetAnimation(_spell3, false);
        else if (state.Equals("aura"))
            SetAnimation(_aura, false);
        else if (state.Equals("mode"))
            SetAnimation(_mode, false);
        else if (state.Equals("passive"))
            SetAnimation(_passive, false);
    }

    private void AnimationEntry_Complete(TrackEntry trackEntry)
    {
        if (UnitProp != null && UnitProp.pathCircle.newObject != null && !UnitProp.resurect) SetCaracterState("idle");
    }

    //Установка анимации персонажа
    public void SetAnimation(AnimationReferenceAsset animation, bool loop)
    {
        if (animationEntry != null) animationEntry.Complete -= AnimationEntry_Complete;
        animationEntry = _skeletonAnimation.state.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = 1;
        animationEntry.Complete += AnimationEntry_Complete;
    }
}
