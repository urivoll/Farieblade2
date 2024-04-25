/*using System.Collections;
using UnityEngine;

public class StepHandler : MonoBehaviour
{
    private IEnumerator AfterTurn()
    {
        attackData = BattleNetwork.attackResultQueue[gameCount - 1];
        currentTryDeath = attackData.tryDeathSend;
        if (turnUnit != null && turnUnit.pathSpells.modeIndex != attackData.mode)
        {
            turnUnit.pathSpells.SetState(attackData.mode);
            yield return new WaitForSeconds(0.5f);
        }
        if (attackData.spell != -20) turnUnit.pathEnergy.SetEnergy(attackData.energy);
        //Удар
        GetComponent<CheckAllowHit>().TurnOver();
        if (attackData.spell == -666)
        {
            turnUnit.AttackedUnit(attackData.makeMove);
        }
        //Защита
        else if (attackData.spell == -10)
        {
            Instantiate(_defendPrefub, turnUnit.pathDebuffs);
            hitDone = true;
        }
        //Баш или смерть
        else if (attackData.spell == -20) hitDone = true;
        //Магия
        else turnUnit.pathSpells.UseActive(attackData.spell, attackData.makeMove);
        while (hitDone == false) yield return null;
        TurnOver?.Invoke();
    }
}
*/