using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TargetSelector : MonoBehaviour, IPointerClickHandler
{
    private BattleNetwork _battleNetwork;

    [Inject]
    private void Construct(BattleNetwork battleNetwork)
    {
        print("df");
        _battleNetwork = battleNetwork;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        UnitProperties unitProperties = eventData.pointerClick.GetComponent<UnitProperties>();
        if (unitProperties.pathCircle.newObject == null ||
            !unitProperties.allowHit ||
            SideUnitUi.modeBlock == true ||
            SideUnitUi.spell == -555) 
            return;
        AttackFormSubmitter attackFormSubmitter = new();
        attackFormSubmitter.Spell = SideUnitUi.spell;
        attackFormSubmitter.ModeIndex = Turns.turnUnit.pathSpells.modeIndex;
        attackFormSubmitter.Ident = BattleNetwork.ident;
        attackFormSubmitter.Side = unitProperties.Side;
        attackFormSubmitter.Place = unitProperties.Place;
        if(_battleNetwork != null)
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
