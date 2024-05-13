using UnityEngine;
using Zenject;

public class DefendReference : MonoBehaviour
{
    [Inject] private BattleNetwork _battleNetwork;

    public void Defend()
    {
        UnitProperties unitProperties = Turns.turnUnit;
        AttackFormSubmitter attackFormSubmitter = new();
        attackFormSubmitter.Spell = -10;
        attackFormSubmitter.ModeIndex = unitProperties.Spells.modeIndex;
        attackFormSubmitter.Ident = BattleNetwork.ident;
        attackFormSubmitter.Side = unitProperties.ParentCircle.Side;
        attackFormSubmitter.Place = unitProperties.ParentCircle.Place;
        _battleNetwork.AttackQuery(attackFormSubmitter);
    }
}