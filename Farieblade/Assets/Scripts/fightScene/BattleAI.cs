using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
public class BattleAI : MonoBehaviour
{
    private int magic;
    List<UnitProperties> allow = new();
    public void AI0()
    {
        if (!AI()) return;
        if (Turns.listAllowHit.Count == 0 || Turns.turnUnit.pathParent.state == 3 || Turns.turnUnit.pathParent.state == 4)
        {
            GetComponent<Turns>().Defend();
            return;
        }
        //Ïîèñê ïî âàíøîòó
        bool have = false;
        for (int i = 0; i < Turns.listAllowHit.Count; i++)
        {
            if (Turns.listAllowHit[i].hp > Turns.turnUnit.damage * Turns.turnUnit.pathParent.times) continue;
            have = true;
            Turns.unitChoose = Turns.listAllowHit[i];
            break;
        }
        if (!have)
        {
            //Ïîèñê ïî Óÿçâèìîìó ìåñòó
            for (int i = 0; i < Turns.listAllowHit.Count; i++)
            {
                if (Turns.turnUnit.pathParent.damageType != Turns.listAllowHit[i].pathParent.vulnerability) continue;
                have = true;
                Turns.unitChoose = Turns.listAllowHit[i];
                break;
            }
        }
        //Ïîèñê íå ïî ðåçèñòó
        if (!have)
        {
            for (int i = 0; i < Turns.listAllowHit.Count; i++)
            {
                if (Turns.turnUnit.pathParent.damageType == Turns.listAllowHit[i].pathParent.resist) continue;
                have = true;
                Turns.unitChoose = Turns.listAllowHit[i];
                break;
            }
        }
        if (!have) Turns.unitChoose = SeekMaxMinDamageHpUnit(Turns.listAllowHit, "max", "damage");
        StartIni.battleNetwork.AttackQuery(-666, Turns.turnUnit.pathSpells.modeIndex, BattleNetwork.ident, Turns.unitChoose.sideOnMap, Turns.unitChoose.placeOnMap);
    }

    public bool AI()
    {
        int energy = Turns.turnUnit.pathEnergy.energy;
        if (!Turns.turnUnit.pathSpells || energy < Turns.turnUnit.pathParent.EnergyÑonsumption ||
            Turns.turnUnit.silence == true) return true;

        List<int> magics = new();
        List<GameObject> spells = new();
        spells.AddRange(Turns.turnUnit.pathSpells.SpellList);

        //Óæàñíûé Ôëåíö!
        if (Turns.turnUnit.pathParent.ID == 35)
        {
            for (int i = 0; i < Turns.listAllowHit.Count; i++)
            {
                if (Turns.listAllowHit[i].idDebuff.Any(item => item.GetComponent<AbstractSpell>().id == 38))
                {
                    UseSpell(Turns.listAllowHit[i], 1);
                    return false;
                }
            }
            UseSpell(Turns.listAllowHit[Random.Range(0, Turns.listAllowHit.Count)], 0);
            return false;
        }
        //Poison Flance
        else if (Turns.turnUnit.pathParent.ID == 33)
        {
            for (int i = 0; i < Turns.listUnitEnemy.Count; i++)
            {
                List<GameObject> effects = Turns.listUnitEnemy[i].idDebuff;
                for (int i2 = 0; i2 < effects.Count; i2++)
                {
                    AbstractSpell Buff = effects[i2].GetComponent<AbstractSpell>();
                    if (Buff.id == 31 || 
                        Buff.id == 16 ||
                        spells[2].GetComponent<VenomFlancePassive>().Value * 2 >= Turns.listUnitEnemy[i].hp)
                    {
                        UseSpell(Turns.listUnitEnemy[0], 1);
                        return false;
                    }
                }
            }
            int maxCount = 0;
            UnitProperties maxCountUnit = null;
            for (int i = 0; i < Turns.listAllowHit.Count; i++)
            {
                List<GameObject> effects = Turns.listAllowHit[i].idDebuff;
                for (int i2 = 0; i2 < effects.Count; i2++)
                {
                    AbstractSpell Debuff = effects[i2].GetComponent<AbstractSpell>();
                    if (Debuff.id == 8)
                    {
                        maxCount = Debuff.gameObject.GetComponent<Poison>().stucks;
                        maxCountUnit = Turns.listAllowHit[i];
                        break;
                    }
                }
            }
            if (maxCount > 0)
            {
                UseSpell(maxCountUnit, 0);
                return false;
            }
            UseSpell(Turns.listUnitEnemy[0], 1);
            return false;
        }
        //Áîëîòíûé ôëåíö
        else if (Turns.turnUnit.pathParent.ID == 34)
        {
            bool need = false;
            for (int i = 0; i < Turns.listUnitEnemy.Count; i++)
            {
                if (Turns.listUnitEnemy[i].pathParent.damageType == 0 ||
                    Turns.listUnitEnemy[i].pathParent.damageType == 6 ||
                    Turns.listUnitEnemy[i].pathParent.damageType == 1 ||
                    Turns.listUnitEnemy[i].pathParent.damageType == 3)
                {
                    need = true;
                    break;
                }
            }
            if (need)
            {
                for (int i = 0; i < Turns.listUnitOur.Count; i++)
                {
                    if (!Turns.listUnitOur[i].idDebuff.Any(item => item.GetComponent<AbstractSpell>().id == 22))
                    {
                        UseSpell(Turns.listUnitOur[i], 0);
                        return false;
                    }
                }
            }
            return true;
        }
        //Dwarf!
        else if (Turns.turnUnit.pathParent.ID == 20)
        {
            for (int i = 0; i < Turns.listUnitOur.Count; i++)
            {
                List<GameObject> Effects = Turns.listUnitOur[i].idDebuff;
                int count = 0;
                for (int i2 = 0; i2 < Effects.Count; i2++)
                {
                    AbstractSpell Debuff = Effects[i2].GetComponent<AbstractSpell>();
                    if (Debuff.Type == "Debuff")
                    {
                        count++;
                        if (Debuff.id == 12 ||
                            Debuff.id == 3 ||
                            (Debuff.id == 8 && Debuff.gameObject.GetComponent<Poison>().stucks >= 3) ||
                            (Debuff.id == 38 && Debuff.gameObject.GetComponent<TerrableBleeding>().stucks >= 3) ||
                            (Debuff.id == 10 && Debuff.gameObject.GetComponent<SoulSeekerWeaknessRes>().stucks >= 3) ||
                            (Debuff.id == 11 && Debuff.gameObject.GetComponent<SoulSeekerWeaknessVul>().stucks >= 3) ||
                            Debuff.id == 6 ||
                            Debuff.id == 18 ||
                            Debuff.id == 27 ||
                            Debuff.id == 21 ||
                            count >= 3)
                        {
                            UseSpell(Turns.listUnitOur[i], 2);
                            return false;
                        }
                    }
                }
            }
            int rand = Random.Range(0, 101);
            if (rand >= 80)
            {
                for (int i = 0; i < Turns.listUnitOur.Count; i++)
                {
                    if (Turns.listUnitOur[i] != Turns.turnUnit)
                    {
                        UseSpell(Turns.listUnitOur[i], 1);
                        return false;
                    }
                }
            }
            else
            {
                int maxDamage = 0;
                UnitProperties maxDamageUnit = null;
                for (int i = 0; i < Turns.listUnitOur.Count; i++)
                {
                    if (Turns.listUnitOur[i].damage > maxDamage)
                    {
                        maxDamage = Turns.listUnitOur[i].damage * Turns.listUnitOur[i].times;
                        maxDamageUnit = Turns.listUnitOur[i];
                    }
                }
                if (maxDamage == 0) return true;
                else
                {
                    UseSpell(maxDamageUnit, 0);
                    return false;
                }
            }
        }
        //Walker!
        else if (Turns.turnUnit.pathParent.ID == 22)
        {
            int rand = Random.Range(0, 101);
            if (rand >= 40)
            {
                for (int i = 0; i < Turns.listUnitEnemy.Count; i++)
                {
                    List<GameObject> Effects = Turns.listUnitEnemy[i].idDebuff;
                    int count = 0;
                    for (int i2 = 0; i2 < Effects.Count; i2++)
                    {
                        AbstractSpell Debuff = Effects[i2].GetComponent<AbstractSpell>();
                        if (Debuff.Type == "Buff")
                        {
                            count++;
                            if (Debuff.id == 13 ||
                                Debuff.id == 15 ||
                                Debuff.id == 16 ||
                                Debuff.id == 26 ||
                                Debuff.id == 28 ||
                                Debuff.id == 31 ||
                                Debuff.id == 36 ||
                                count >= 3)
                            {
                                UseSpell(Turns.listUnitEnemy[i], 1);
                                return false;
                            }
                        }
                    }
                }
            }
            bool have = false;
            for (int i = 0; i < Turns.listUnitEnemy.Count; i++)
            {
                if (Turns.listUnitEnemy[i].pathParent.state == 2 ||
                    Turns.listUnitEnemy[i].pathParent.state == 1)
                {
                    have = true;
                    break;
                }
            }
            if (have == false) return true;

            int minDamage = 10000;
            UnitProperties minDamageUnit = null;
            for (int i = 0; i < Turns.listUnitOur.Count; i++)
            {
                if (!Turns.listUnitOur[i].idDebuff.Any(item => item.GetComponent<AbstractSpell>().id == 34) &&
                    Turns.listUnitOur[i].damage < minDamage)
                {
                    minDamage = Turns.listUnitOur[i].damage;
                    minDamageUnit = Turns.listUnitOur[i];
                }
            }
            for (int i = 0; i < Turns.listUnitEnemy.Count; i++)
            {
                if ((Turns.listUnitEnemy[i].pathParent.state == 1 ||
                    Turns.listUnitEnemy[i].pathParent.state == 2) &&
                    Turns.listUnitEnemy[i].damage > minDamageUnit.damage)
                {
                    UseSpell(minDamageUnit, 0);
                    return false;
                }
            }
            for (int i = 0; i < Turns.listUnitEnemy.Count; i++)
            {
                List<GameObject> effects = Turns.listUnitEnemy[i].idDebuff;
                for (int i2 = 0; i2 < effects.Count; i2++)
                {
                    AbstractSpell debuff = effects[i2].GetComponent<AbstractSpell>();
                    if (debuff.Type == "Buff")
                    {
                        {
                            UseSpell(Turns.listUnitEnemy[i], 1);
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        //Duelist
        else if (Turns.turnUnit.pathParent.ID == 30)
        {
            for (int i = 0; i < Turns.listUnitEnemy.Count; i++)
            {
                if (Turns.listUnitEnemy[i].pathEnergy.energy >= Turns.listUnitEnemy[i].pathParent.EnergyÑonsumption)
                {
                    if (Turns.listUnitEnemy[i].pathParent.state == 1 ||
                        Turns.listUnitEnemy[i].pathParent.state == 2)
                    {
                        UseSpell(Turns.listUnitEnemy[i], 1);
                        return false;
                    }
                }
            }
            for (int i = 0; i < Turns.listAllowHit.Count; i++)
            {
                if (Turns.listAllowHit[i].pathEnergy.energy >= Turns.listAllowHit[i].pathParent.EnergyÑonsumption &&
                    !Turns.listAllowHit[i].idDebuff.Any(item => item.GetComponent<AbstractSpell>().id == 27))
                {
                    UseSpell(Turns.listAllowHit[i], 0);
                    return false;
                }
            }
            return true;
        }
        //FatMan
        else if (Turns.turnUnit.pathParent.ID == 15)
        {
            int maxDamage = 0;
            UnitProperties maxDamageUnit = null;
            bool have = false;
            for (int i = 0; i < Turns.listUnitEnemy.Count; i++)
            {
                List<GameObject> effects = Turns.listUnitEnemy[i].idDebuff;
                if (Turns.listUnitEnemy[i].damage > maxDamage &&
                    !effects.Any(item => item.GetComponent<AbstractSpell>().id == 18))
                {
                    maxDamage = Turns.listUnitEnemy[i].damage * Turns.listUnitEnemy[i].pathParent.times;
                    maxDamageUnit = Turns.listUnitEnemy[i];
                }
                for (int i2 = 0; i2 < effects.Count; i2++)
                {
                    AbstractSpell debuff = effects[i2].GetComponent<AbstractSpell>();
                    if (debuff.Type == "Debuff" && debuff.id == 19)
                        have = true;
                }
            }
            if (have == false)
            {
                UseSpell(Turns.listUnitEnemy[0], 0);
                return false;
            }
            UseSpell(maxDamageUnit, 1);
            return false;
        }
        //LizardWarrior
        else if (Turns.turnUnit.pathParent.ID == 17)
        {
            for (int i = 0; i < Turns.listUnitEnemy.Count; i++)
            {
                if (Turns.listUnitEnemy[i].pathParent.state == 1 ||
                    Turns.listUnitEnemy[i].pathParent.state == 2 ||
                    Turns.listUnitEnemy[i].pathParent.state == 3 ||
                    Turns.listUnitEnemy[i].pathParent.state == 4)
                {
                    UseSpell(Turns.listUnitEnemy[i], 0);
                    return false;
                }
            }
            return true;
        }
        //MoonMage
        else if (Turns.turnUnit.pathParent.ID == 18)
        {
            int maxDamage = 0;
            UnitProperties maxDamageUnit = null;
            for (int i = 0; i < Turns.listUnitOur.Count; i++)
            {
                List<GameObject> effects = Turns.listUnitOur[i].idDebuff;
                if (!effects.Any(item => item.GetComponent<AbstractSpell>().id == 29) && Turns.listUnitOur[i].damage * Turns.listUnitOur[i].times > maxDamage)
                {
                    maxDamage = Turns.listUnitOur[i].damage * Turns.listUnitOur[i].times;
                    maxDamageUnit = Turns.listUnitOur[i];
                }
            }
            if (maxDamageUnit != null)
            {
                UseSpell(maxDamageUnit, 0);
                return false;
            }
            return true;
        }
        //God
        else if (Turns.turnUnit.pathParent.ID == 14)
        {
            for (int i = 0; i < Turns.listUnitOur.Count; i++)
            {
                if (Turns.listUnitOur[i].hpProsent <= 65 &&
                    !Turns.listUnitOur[i].idDebuff.Any(item => item.GetComponent<AbstractSpell>().id == 36))
                {
                    UseSpell(Turns.listUnitOur[i], 0);
                    return false;
                }
            }
            bool have = false;
            for (int i = 0; i < Turns.listUnitOur.Count; i++)
            {
                if (Turns.listUnitOur[i].pathParent.ID == 44)
                {
                    have = true;
                    break;
                }
            }
            if (have == true)
            {
                int maxDamage = 0;
                UnitProperties maxDamageUnit = null;
                for (int i = 0; i < Turns.listUnitOur.Count; i++)
                {
                    List<GameObject> effects = Turns.listUnitOur[i].idDebuff;
                    if (Turns.listUnitOur[i].pathParent.ID != 44 && !effects.Any(item => item.GetComponent<AbstractSpell>().id == 37) && Turns.listUnitOur[i].damage > maxDamage)
                    {
                        maxDamage = Turns.listUnitOur[i].damage;
                        maxDamageUnit = Turns.listUnitOur[i];
                    }
                }
                if (maxDamageUnit != null)
                {
                    UseSpell(maxDamageUnit, 1);
                    return false;
                }
            }
            for (int i = 0; i < Turns.listUnitOur.Count; i++)
            {
                if (Turns.listUnitOur[i].hpProsent <= 90 &&
                    !Turns.listUnitOur[i].idDebuff.Any(item => item.GetComponent<AbstractSpell>().id == 36))
                {
                    UseSpell(Turns.listUnitOur[i], 0);
                    return false;
                }
            }
            return true;
        }
        //Fermor
        else if (Turns.turnUnit.pathParent.ID == 10)
        {
            for (int i = 0; i < Turns.listUnitEnemy.Count; i++)
            {       //Âàìïèðèê
                if (((Turns.listUnitEnemy[i].hpProsent <= 70 && (Turns.listUnitEnemy[i].pathParent.ID == 35 || Turns.listUnitEnemy[i].pathParent.ID == 8)) ||
                    //Ëåäÿíîé õèëë
                    (!Turns.listUnitEnemy[i].idDebuff.Any(item => item.GetComponent<AbstractSpell>().id == 36) && Turns.listUnitEnemy[i].hpProsent <= 90) ||
                    //êðèòòåð
                    (Turns.listUnitEnemy[i].pathEnergy.energy >= 3 && Turns.listUnitEnemy[i].pathParent.ID == 37)) &&
                    !Turns.listUnitEnemy[i].idDebuff.Any(item => item.GetComponent<AbstractSpell>().id == 39))
                {
                    UseSpell(Turns.listUnitEnemy[i], 1);
                    return false;
                }
            }
            for (int i = 0; i < Turns.listUnitEnemy.Count; i++)
            {
                if (Turns.listUnitEnemy[i].pathParent.vulnerability == 5)
                {
                    UseSpell(Turns.listUnitEnemy[i], 0);
                    return false;
                }
            }
            UseSpell(Turns.listUnitEnemy[Random.Range(0, Turns.listUnitEnemy.Count)], 0);
            return false;

        }
        //Goblin
        else if (Turns.turnUnit.pathParent.ID == 19)
        {
            int maxDamage = 0;
            UnitProperties maxDamageUnit = null;
            for (int i = 0; i < Turns.listUnitEnemy.Count; i++)
            {
                if (Turns.listUnitEnemy[i].damage > maxDamage &&
                    !Turns.listUnitEnemy[i].idDebuff.Any(item => item.GetComponent<AbstractSpell>().id == 4))
                {
                    maxDamage = Turns.listUnitEnemy[i].damage;
                    maxDamageUnit = Turns.listUnitEnemy[i];
                }
            }
            if (maxDamageUnit != null)
            {
                UseSpell(maxDamageUnit, 0);
                return false;
            }
            return true;
        }
        //Soul Seeker
        else if (Turns.turnUnit.pathParent.ID == 16)
        {
            UseSpell(SeekMaxMinDamageHpUnit(Turns.listUnitEnemy, "max", "damage"), 0);
            StartCoroutine(Async(40));
            return false;
        }
        //Necromancer
        else if (Turns.turnUnit.pathParent.ID == 5)
        {
            for (int i = 0; i < Turns.listUnitEnemy.Count; i++)
            {
                if (Turns.listUnitEnemy[i].pathEnergy.energy >= Turns.listUnitEnemy[i].pathParent.EnergyÑonsumption &&
                   Turns.listUnitEnemy[i].pathSpells.SpellList.Any(item => item.GetComponent<AbstractSpell>().Type == "Debuff"))
                {
                    for (int i2 = 0; i2 < Turns.listUnitOur.Count; i2++)
                    {
                        if (!Turns.listUnitOur[i2].idDebuff.Any(item => item.GetComponent<AbstractSpell>().id == 31))
                        {
                            UseSpell(Turns.listUnitOur[i2], 1);
                            return false;
                        }
                    }

                }
            }
            for (int i = 0; i < Turns.listUnitOur.Count; i++)
            {
                if (!Turns.listUnitOur[i].idDebuff.Any(item => item.GetComponent<AbstractSpell>().id == 40))
                {
                    UseSpell(Turns.listUnitOur[Random.Range(0, Turns.listUnitOur.Count)], 0);
                    return false;
                }
            }
            return true;
        }
        //Falnce Shaman
        else if (Turns.turnUnit.pathParent.ID == 36)
        {
            for (int i = 0; i < Turns.listUnitOur.Count; i++)
            {
                if ((Turns.listUnitOur[i].pathParent.state == 0 || Turns.listUnitOur[i].pathParent.Type == 3) &&
                    !Turns.listUnitOur[i].idDebuff.Any(item => item.GetComponent<AbstractSpell>().id == 15))
                {
                    UseSpell(Turns.listUnitOur[i], 0);
                    return false;
                }
            }
            for (int i = 0; i < Turns.listUnitOur.Count; i++)
            {
                if (!Turns.listUnitOur[i].idDebuff.Any(item => item.GetComponent<AbstractSpell>().id == 15))
                {
                    UseSpell(Turns.listUnitOur[i], 0);
                    return false;
                }
            }
            return true;
        }
        //Twins
        else if (Turns.turnUnit.pathParent.ID == 32)
        {
            if (!Turns.turnUnit.idDebuff.Any(item => item.GetComponent<AbstractSpell>().id == 28))
            {
                int maxDamage = -1;
                UnitProperties maxDamageUnit = null;
                for (int i = 0; i < Turns.listUnitOur.Count; i++)
                {
                    if (Turns.listUnitOur[i].damage * Turns.listUnitOur[i].pathParent.times > maxDamage &&
                        Turns.listUnitOur[i] != Turns.turnUnit)
                    {
                        maxDamage = Turns.listUnitOur[i].damage * Turns.listUnitOur[i].pathParent.times;
                        maxDamageUnit = Turns.listUnitOur[i];
                    }
                }
                if (maxDamageUnit != null)
                {
                    UseSpell(maxDamageUnit, 0);
                    return false;
                }
            }
            int minDamage = 10000;
            UnitProperties minDamageUnit = null;
            for (int i = 0; i < Turns.listUnitOur.Count; i++)
            {
                if (Turns.listUnitOur[i].damage * Turns.listUnitOur[i].pathParent.times < minDamage &&
                    !Turns.listUnitOur[i].idDebuff.Any(item => item.GetComponent<AbstractSpell>().id == 35))
                {
                    minDamage = Turns.listUnitOur[i].damage * Turns.listUnitOur[i].pathParent.times;
                    minDamageUnit = Turns.listUnitOur[i];
                }
            }
            if (minDamageUnit != null)
            {
                UseSpell(minDamageUnit, 1);
                return false;
            }
            return true;
        }
        //Critter
        else if (Turns.turnUnit.pathParent.ID == 37)
        {
            if (Turns.turnUnit.hpProsent < 95 &&
                !Turns.turnUnit.idDebuff.Any(item => item.GetComponent<AbstractSpell>().id == 39))
            {
                UseSpell(Turns.listAllowHit[Random.Range(0, Turns.listAllowHit.Count)], 0);
                return false;
            }
            return true;
        }

        //Ïðî÷åå
        for (int i = 0; i < spells.Count; i++)
        {
            if (spells[i].GetComponent<AbstractSpell>().state == "Ball" || spells[i].GetComponent<AbstractSpell>().state == "Effect" ||
                spells[i].GetComponent<AbstractSpell>().state == "Melee" || spells[i].GetComponent<AbstractSpell>().state == "nonTarget")
            {
                magics.Add(i);
            }
        }
        int RandomMagic = Random.Range(0, magics.Count);
        if (magics.Count > 0)
        {
            magic = magics[RandomMagic];
            AbstractSpell currentSpell = spells[magics[RandomMagic]].GetComponent<AbstractSpell>();
            allow = new();
            allow.AddRange(Turns.listUnitAll);
            return SpellRegular(currentSpell);
        }
        else return true;
    }
    private bool SpellRegular(AbstractSpell currentSpell)
    {
        while (true)
        {
            int rand2 = Random.Range(0, allow.Count);

            if (allow[rand2] != null &&

                ((currentSpell.ToEnemy == true && allow[rand2].sideOnMap != Turns.turnUnit.sideOnMap) ||
                (currentSpell.ToEnemy == false && allow[rand2].sideOnMap == Turns.turnUnit.sideOnMap))
                &&
                (currentSpell.AllPlace == true ||
                (currentSpell.AllPlace == false && allow[rand2].allowHit == true))
                &&
                (currentSpell.NotMe == false ||
                (currentSpell.NotMe == true && allow[rand2] != Turns.turnUnit)))
            {
                if (currentSpell.state == "Effect")
                {
                    bool fail = allow[rand2].idDebuff.Any(item => item.GetComponent<AbstractSpell>().id == currentSpell.id);
                    if (fail == false)
                    {
                        UseSpell(allow[rand2], magic);
                        return false;
                    }
                }
                else
                {
                    UseSpell(allow[rand2], magic);
                    return false;
                }
            }
            allow.RemoveAt(rand2);
            if (allow.Count == 0) return true;
        }
    }
    private bool SeekUnitDamageType(List<UnitProperties> List, int type)
    {
        for (int i = 0; i < List.Count; i++)
        {
            if (type == List[i].inpDamageType)
            {
                return true;
            }
        }
        return false;
    }
    private UnitProperties SeekMaxMinDamageHpUnit(List<UnitProperties> List, string mode, string what)
    {
        int target;
        if (mode == "max")
            target = 0;
        else
            target = 10000;
        UnitProperties targetUnit = null;
        for (int i = 0; i < List.Count; i++)
        {
            if (what == "hp" && ((mode == "max" && List[i].hpProsent > target) ||
                (mode == "min" && List[i].hpProsent < target)))
            {
                target = Convert.ToInt32(List[i].hpProsent);
                targetUnit = List[i];
            }
            else if (what == "damage" && ((mode == "max" && List[i].damage > target) ||
                (mode == "min" && List[i].damage < target)))
            {
                target = List[i].damage;
                targetUnit = List[i];
            }
        }
        if (targetUnit != null)
            return targetUnit;
        else return null;
    }
    private void UseSpell(UnitProperties unitTarget, int spellIndex)
    {
        Turns.unitChoose = unitTarget;
        StartIni.battleNetwork.AttackQuery(spellIndex, Turns.turnUnit.pathSpells.modeIndex, BattleNetwork.ident, unitTarget.sideOnMap, unitTarget.placeOnMap);
    }
    private IEnumerator Async(int id)
    {
        if (id == 42)
        {
            int mode = 0;
            UnitProperties targetUnit = null;
            if (Turns.turnUnit.hpProsent <= 60)
            {
                for (int i = 0; i < Turns.listAllowHit.Count; i++)
                {
                    if (Turns.listAllowHit[i].pathParent.resist != 7)
                        targetUnit = Turns.listAllowHit[i];
                }
            }
            if (targetUnit == null)
            {
                mode = 1;
                for (int i = 0; i < Turns.listAllowHit.Count; i++)
                {
                    if (Turns.listAllowHit[i].pathParent.vulnerability != 7)
                        targetUnit = Turns.listAllowHit[i];
                }
                if (targetUnit == null)
                {
                    Turns.unitChoose = Turns.listAllowHit[Random.Range(0, Turns.listAllowHit.Count)];
                }
                else Turns.unitChoose = targetUnit;
            }
            else
            {
                mode = 2;
                Turns.unitChoose = targetUnit;
            }

            if (mode == 2)
            {
                if (Turns.turnUnit.pathSpells.modeIndex != 1)
                {
                    Turns.turnUnit.pathSpells.SetState(1);
                    yield return new WaitForSeconds(0.6f);
                }
            }
            else
            {
                if (Turns.turnUnit.pathSpells.modeIndex != 0)
                {
                    Turns.turnUnit.pathSpells.SetState(0);
                    yield return new WaitForSeconds(0.6f);
                }
            }
            if (Turns.turnUnit.pathEnergy.energy >= 3)
            {
                //StartCoroutine(Turns.turnUnit.PathSpells.UseActiveAsync(2));
                //Turns.turnUnit.PathEnergy.SetEnergy(3, '-');
            }
            else
            {
                //Turns.turnUnit.AttackedUnit(Turns.UnitChoose);
            }

        }
        else if (id == 40)
        {
            if (Turns.turnUnit.pathEnergy.energy >= 3)
            {
                Turns.unitChoose = SeekMaxMinDamageHpUnit(Turns.listUnitEnemy, "max", "damage");
                //StartCoroutine(Turns.turnUnit.PathSpells.UseActiveAsync(2));
                //Turns.turnUnit.PathEnergy.SetEnergy(3, '-');
            }
            else
            {
                bool have = false;
                for (int i = 0; i < Turns.listUnitEnemy.Count; i++)
                {
                    if (Turns.listUnitEnemy[i].pathParent.vulnerability != 7 &&
                        SeekUnitDamageType(Turns.listUnitEnemy, Turns.listUnitEnemy[i].pathParent.vulnerability) == true)
                    {
                        Turns.unitChoose = Turns.listUnitEnemy[i];
                        have = true;
                        break;
                    }
                }
                if (have == true)
                {
                    if (Turns.turnUnit.pathSpells.modeIndex != 0)
                    {
                        Turns.turnUnit.pathSpells.SetState(0);
                        yield return new WaitForSeconds(0.6f);
                    }
                    //Turns.turnUnit.AttackedUnit(Turns.UnitChoose);
                }
                else
                {
                    for (int i = 0; i < Turns.listUnitEnemy.Count; i++)
                    {
                        if (Turns.listUnitEnemy[i].pathParent.resist != 7 &&
                            SeekUnitDamageType(Turns.listUnitEnemy, Turns.listUnitEnemy[i].pathParent.resist) == true)
                        {
                            Turns.unitChoose = Turns.listUnitEnemy[i];
                            have = true;
                            break;
                        }
                    }
                    if (have == true)
                    {
                        if (Turns.turnUnit.pathSpells.modeIndex != 1)
                        {
                            Turns.turnUnit.pathSpells.SetState(1);
                            yield return new WaitForSeconds(0.6f);
                        }
                        //Turns.turnUnit.AttackedUnit(Turns.UnitChoose);
                    }
                    else
                    {
                        Turns.unitChoose = Turns.listUnitEnemy[Random.Range(0, Turns.listUnitEnemy.Count)];
                        //Turns.turnUnit.AttackedUnit(Turns.UnitChoose);
                    }
                }
            }
        }
    }
}
