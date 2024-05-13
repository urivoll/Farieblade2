using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TargetSelector : MonoBehaviour, IPointerClickHandler
{
    [Inject] private BattleNetwork _battleNetwork;

    public void OnPointerClick(PointerEventData eventData)
    {

        UnitProperties unitProperties = eventData.pointerClick.GetComponent<UnitProperties>();
        print(unitProperties.CharacterState.AllowHit);
        print(SideUnitUi.modeBlock);
        print(SideUnitUi.spell);
        if (unitProperties.ParentCircle.ChildCharacter == null ||
            !unitProperties.CharacterState.AllowHit ||
            SideUnitUi.modeBlock == true ||
            SideUnitUi.spell == -555) 
            return;
        print("df2");
        AttackFormSubmitter attackFormSubmitter = new();
        attackFormSubmitter.Spell = SideUnitUi.spell;
        attackFormSubmitter.ModeIndex = Turns.turnUnit.Spells.modeIndex;
        attackFormSubmitter.Ident = BattleNetwork.ident;
        attackFormSubmitter.Side = unitProperties.ParentCircle.Side;
        attackFormSubmitter.Place = unitProperties.ParentCircle.Place;
        _battleNetwork.AttackQuery(attackFormSubmitter);
        SideUnitUi.spell = -555;
    }
}
public class AttackFormSubmitter
{
    public int Spell;
    public int ModeIndex;
    public string Ident;
    public int Side;
    public int Place;
}
