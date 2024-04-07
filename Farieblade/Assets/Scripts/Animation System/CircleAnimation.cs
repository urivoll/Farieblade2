using Spine.Unity;
using UnityEngine;

public class CircleAnimation : AbstractAnimation
{
    [SerializeField] private AnimationReferenceAsset _idle, _circleEnemy, _circleOur, _circleSpell, _circleEnemyTurn;
    public void SetCaracterState(string state)
    {
        if (state.Equals("idle"))
            SetAnimation(_idle, true, 1f, "circle");
        else if (state.Equals("circleEnemy")) 
            SetAnimation(_circleEnemy, true, 1f, "circle");
        else if (state.Equals("circleOur")) 
            SetAnimation(_circleOur, true, 1f, "circle");
        else if (state.Equals("circleSpell"))
            SetAnimation(_circleSpell, true, 1f, "circle");
        else if (state.Equals("circleEnemyTurn"))
            SetAnimation(_circleEnemyTurn, true, 1f, "circle");
    }
}
