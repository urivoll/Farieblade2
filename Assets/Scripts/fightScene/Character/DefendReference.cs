using UnityEngine;
using Zenject;

public class DefendReference : MonoBehaviour
{
    private BattleNetwork _battleNetwork;

    [Inject]
    private void Construct(BattleNetwork battleNetwork)
    {
        _battleNetwork = battleNetwork;
    }
    public void Defend(UnitProperties unitProperties)
    {
        AttackFormSubmitter attackFormSubmitter = new();
        attackFormSubmitter.Spell = -10;
        attackFormSubmitter.ModeIndex = unitProperties.Spells.modeIndex;
        attackFormSubmitter.Ident = BattleNetwork.ident;
        attackFormSubmitter.Side = unitProperties.ParentCircle.Side;
        attackFormSubmitter.Place = unitProperties.ParentCircle.Place;
        _battleNetwork.AttackQuery(attackFormSubmitter);
    }
}