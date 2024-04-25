using Spine;
using Spine.Unity;
using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    private SkeletonAnimation _skeletonAnimation;
    private AnimationAssets _animations;
    private TrackEntry animationEntry = null;
    private HpCharacter _hpCharacter;
    public void Init(AnimationAssets animations)
    {
        _animations = animations;
        _hpCharacter = GetComponent<HpCharacter>();
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    public void TryGetAnimation(string state)
    {
        AnimationReferenceAsset tempAnimation;
        if (state == "idle") tempAnimation = _animations.Idle;
        else if (state == "attack") tempAnimation = _animations.Attack;
        else if (state == "hit") tempAnimation = _animations.Hit;
        else if (state == "death") tempAnimation = _animations.Death;
        else if (state == "spell") tempAnimation = _animations.Spell;
        else if (state == "spell2") tempAnimation = _animations.Spell2;
        else if (state == "mode") tempAnimation = _animations.Mode;
        else tempAnimation = _animations.Passive;
        SetAnimation(tempAnimation, false);
    }

    private void AnimationComplete(TrackEntry trackEntry)
    {
        if (_hpCharacter != null && !_hpCharacter.resurect) 
            SetAnimation(_animations.Idle, true);
    }

    private void SetAnimation(AnimationReferenceAsset animation, bool loop)
    {
        if (animationEntry != null) animationEntry.Complete -= AnimationComplete;
        animationEntry = _skeletonAnimation.state.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = 1;
        animationEntry.Complete += AnimationComplete;
    }
}
