using Spine;
using Spine.Unity;
using UnityEngine;

public abstract class AbstractAnimation : MonoBehaviour
{
    private SkeletonAnimation _skeletonAnimation;

    //Установка анимации персонажа
    public TrackEntry SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale, string type)
    {
        if (type == "unit") _skeletonAnimation = gameObject.GetComponentInChildren<SkeletonAnimation>();
        else _skeletonAnimation = gameObject.GetComponent<SkeletonAnimation>();

        TrackEntry animationEntry = _skeletonAnimation.state.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = timeScale;
        return animationEntry;
    }
}
