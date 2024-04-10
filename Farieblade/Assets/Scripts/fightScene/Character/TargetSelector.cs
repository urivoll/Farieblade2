using UnityEngine;
using UnityEngine.EventSystems;

public class TargetSelector : MonoBehaviour, IPointerClickHandler
{
    private UnitProperties _unitProperties;
    private void Start()
    {
        _unitProperties = GetComponent<UnitProperties>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_unitProperties.pathCircle.newObject == null ||
            !eventData.pointerClick.GetComponent<UnitProperties>().allowHit ||
            SideUnitUi.modeBlock == true ||
            SideUnitUi.spell == -555) return;
        StartIni.battleNetwork.AttackQuery(SideUnitUi.spell, Turns.turnUnit.pathSpells.modeIndex, BattleNetwork.ident, _unitProperties.Side, _unitProperties.Place);
        SideUnitUi.spell = -555;
    }
}
