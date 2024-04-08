using Spine;
using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    private SkeletonAnimation _skeletonAnimation;
    private IntentAnimation[] _animations;
    public AnimationReferenceAsset _attack, _idle, _hit, _death, _spell, _spell2, _spell3, _aura, _mode, _passive;
    private TrackEntry animationEntry = null;
    private UnitProperties _model;
    public void Init(IntentAnimation[] animations)
    {
        _animations = animations;
        _model = GetComponent<UnitProperties>();
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
    }
    public void SetCaracterState(string state)
    {
        if (state == "idle") SetAnimation(_idle, true);

        else if (state == "attack") SetAnimation(_attack, false);

        else if (state == "hit") SetAnimation(_hit, false);

        else if (state == "death") SetAnimation(_death, false);

        else if (state == "spell") SetAnimation(_spell, false);

        else if (state == "spell2") SetAnimation(_spell2, false);

        else if (state == "spell3") SetAnimation(_spell3, false);

        else if (state == "aura") SetAnimation(_aura, false);

        else if (state == "mode") SetAnimation(_mode, false);

        else if (state == "passive") SetAnimation(_passive, false);

    }

    private void AnimationEntry_Complete(TrackEntry trackEntry)
    {
        if (_model != null && _model.pathCircle.newObject != null && !_model.resurect) SetCaracterState("idle");
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
