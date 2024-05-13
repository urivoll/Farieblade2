using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
public class BattleAI : MonoBehaviour
{
    [Inject] private CharacterPlacement _characterPlacement;
    private int magic;
    private List<UnitProperties> allow = new();

    public void AI0()
    {
        if (!AI()) return;
        if (Turns.listAllowHit.Count == 0 || Turns.turnUnit.Type == 3 || Turns.turnUnit.Type == 4)
        {
            //_defendReference.Defend();
            return;
        }
        //Ïîèñê ïî âàíøîòó
        bool have = false;
        for (int i = 0; i < Turns.listAllowHit.Count; i++)
        {
            if (Turns.listAllowHit[i].HpCharacter.Hp > Turns.turnUnit.Weapon.Damage * Turns.turnUnit.Weapon.Times) continue;
            have = true;
            Turns.unitChoose = Turns.listAllowHit[i];
            break;
        }
        if (!have)
        {
            //Ïîèñê ïî Óÿçâèìîìó ìåñòó
            for (int i = 0; i < Turns.listAllowHit.Count; i++)
            {
                if (Turns.turnUnit.Weapon.DamageType != Turns.listAllowHit[i].HpCharacter.Vulnerability) continue;
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
                if (Turns.turnUnit.Weapon.DamageType == Turns.listAllowHit[i].HpCharacter.Resist) continue;
                have = true;
                Turns.unitChoose = Turns.listAllowHit[i];
                break;
            }
        }
        if (!have) Turns.unitChoose = SeekMaxMinDamageHpUnit(Turns.listAllowHit, "max", "damage");
        //StartIni.battleNetwork.AttackQuery(-666, Turns.turnUnit.pathSpells.modeIndex, BattleNetwork.ident, Turns.unitChoose.Side, Turns.unitChoose.Place);
    }

    public bool AI()
    {
        int energy = Turns.turnUnit.Energy.energy;
        if (!Turns.turnUnit.Spells || energy < Turns.turnUnit.EnergyÑonsumption ||
            Turns.turnUnit.CharacterState.Silence == true) return true;

        List<int> magics = new();
        List<GameObject> spells = new();
        spells.AddRange(Turns.turnUnit.Spells.SpellList);

        //Óæàñíûé Ôëåíö!
        if (Turns.turnUnit.Id == 35)
        {
            for (int i = 0; i < Turns.listAllowHit.Count; i++)
            {
                if (Turns.listAllowHit[i].DebuffList.Any(item => item.GetComponent<AbstractSpell>().id == 38))
                {
                    UseSpell(Turns.listAllowHit[i], 1);
                    return false;
                }
            }
            UseSpell(Turns.listAllowHit[Random.Range(0, Turns.listAllowHit.Count)], 0);
            return false;
        }
        //Poison Flance
        else if (Turns.turnUnit.Id == 33)
        {
            for (int i = 0; i < _characterPlacement.UnitEnemy.Count; i++)
            {
                List<GameObject> effects = _characterPlacement.UnitEnemy[i].DebuffList;
                for (int i2 = 0; i2 < effects.Count; i2++)
                {
                    AbstractSpell Buff = effects[i2].GetComponent<AbstractSpell>();
                    if (Buff.id == 31 || 
                        Buff.id == 16 ||
                        spells[2].GetComponent<VenomFlancePassive>().Value * 2 >= _characterPlacement.UnitEnemy[i].HpCharacter.Hp)
                    {
                        UseSpell(_characterPlacement.UnitEnemy[0], 1);
                        return false;
                    }
                }
            }
            int maxCount = 0;
            UnitProperties maxCountUnit = null;
            for (int i = 0; i < Turns.listAllowHit.Count; i++)
            {
                List<GameObject> effects = Turns.listAllowHit[i].DebuffList;
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
            UseSpell(_characterPlacement.UnitEnemy[0], 1);
            return false;
        }
        //Áîëîòíûé ôëåíö
        else if (Turns.turnUnit.Id == 34)
        {
            bool need = false;
            for (int i = 0; i < _characterPlacement.UnitEnemy.Count; i++)
            {
                if (_characterPlacement.UnitEnemy[i].Weapon.DamageType == 0 ||
                    _characterPlacement.UnitEnemy[i].Weapon.DamageType == 6 ||
                    _characterPlacement.UnitEnemy[i].Weapon.DamageType == 1 ||
                    _characterPlacement.UnitEnemy[i].Weapon.DamageType == 3)
                {
                    need = true;
                    break;
                }
            }
            if (need)
            {
                for (int i = 0; i < _characterPlacement.UnitOur.Count; i++)
                {
                    if (!_characterPlacement.UnitOur[i].DebuffList.Any(item => item.GetComponent<AbstractSpell>().id == 22))
                    {
                        UseSpell(_characterPlacement.UnitOur[i], 0);
                        return false;
                    }
                }
            }
            return true;
        }
        //Dwarf!
        else if (Turns.turnUnit.Id == 20)
        {
            for (int i = 0; i < _characterPlacement.UnitOur.Count; i++)
            {
                List<GameObject> Effects = _characterPlacement.UnitOur[i].DebuffList;
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
                            UseSpell(_characterPlacement.UnitOur[i], 2);
                            return false;
                        }
                    }
                }
            }
            int rand = Random.Range(0, 101);
            if (rand >= 80)
            {
                for (int i = 0; i < _characterPlacement.UnitOur.Count; i++)
                {
                    if (_characterPlacement.UnitOur[i] != Turns.turnUnit)
                    {
                        UseSpell(_characterPlacement.UnitOur[i], 1);
                        return false;
                    }
                }
            }
            else
            {
                int maxDamage = 0;
                UnitProperties maxDamageUnit = null;
                for (int i = 0; i < _characterPlacement.UnitOur.Count; i++)
                {
                    if (_characterPlacement.UnitOur[i].Weapon.Damage > maxDamage)
                    {
                        maxDamage = _characterPlacement.UnitOur[i].Weapon.Damage * _characterPlacement.UnitOur[i].Weapon.Times;
                        maxDamageUnit = _characterPlacement.UnitOur[i];
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
        else if (Turns.turnUnit.Id == 22)
        {
            int rand = Random.Range(0, 101);
            if (rand >= 40)
            {
                for (int i = 0; i < _characterPlacement.UnitEnemy.Count; i++)
                {
                    List<GameObject> Effects = _characterPlacement.UnitEnemy[i].DebuffList;
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
                                UseSpell(_characterPlacement.UnitEnemy[i], 1);
                                return false;
                            }
                        }
                    }
                }
            }
            bool have = false;
            for (int i = 0; i < _characterPlacement.UnitEnemy.Count; i++)
            {
                if (_characterPlacement.UnitEnemy[i].Type == 2 ||
                    _characterPlacement.UnitEnemy[i].Type == 1)
                {
                    have = true;
                    break;
                }
            }
            if (have == false) return true;

            int minDamage = 10000;
            UnitProperties minDamageUnit = null;
            for (int i = 0; i < _characterPlacement.UnitOur.Count; i++)
            {
                if (!_characterPlacement.UnitOur[i].DebuffList.Any(item => item.GetComponent<AbstractSpell>().id == 34) &&
                    _characterPlacement.UnitOur[i].Weapon.Damage < minDamage)
                {
                    minDamage = _characterPlacement.UnitOur[i].Weapon.Damage;
                    minDamageUnit = _characterPlacement.UnitOur[i];
                }
            }
            for (int i = 0; i < _characterPlacement.UnitEnemy.Count; i++)
            {
                if ((_characterPlacement.UnitEnemy[i].Type == 1 ||
                    _characterPlacement.UnitEnemy[i].Type == 2) &&
                    _characterPlacement.UnitEnemy[i].Weapon.Damage > minDamageUnit.Weapon.Damage)
                {
                    UseSpell(minDamageUnit, 0);
                    return false;
                }
            }
            for (int i = 0; i < _characterPlacement.UnitEnemy.Count; i++)
            {
                List<GameObject> effects = _characterPlacement.UnitEnemy[i].DebuffList;
                for (int i2 = 0; i2 < effects.Count; i2++)
                {
                    AbstractSpell debuff = effects[i2].GetComponent<AbstractSpell>();
                    if (debuff.Type == "Buff")
                    {
                        {
                            UseSpell(_characterPlacement.UnitEnemy[i], 1);
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        //Duelist
        else if (Turns.turnUnit.Id == 30)
        {
            for (int i = 0; i < _characterPlacement.UnitEnemy.Count; i++)
            {
                if (_characterPlacement.UnitEnemy[i].Energy.energy >= _characterPlacement.UnitEnemy[i].EnergyÑonsumption)
                {
                    if (_characterPlacement.UnitEnemy[i].Type == 1 ||
                        _characterPlacement.UnitEnemy[i].Type == 2)
                    {
                        UseSpell(_characterPlacement.UnitEnemy[i], 1);
                        return false;
                    }
                }
            }
            for (int i = 0; i < Turns.listAllowHit.Count; i++)
            {
                if (Turns.listAllowHit[i].Energy.energy >= Turns.listAllowHit[i].EnergyÑonsumption &&
                    !Turns.listAllowHit[i].DebuffList.Any(item => item.GetComponent<AbstractSpell>().id == 27))
                {
                    UseSpell(Turns.listAllowHit[i], 0);
                    return false;
                }
            }
            return true;
        }
        //FatMan
        else if (Turns.turnUnit.Id == 15)
        {
            int maxDamage = 0;
            UnitProperties maxDamageUnit = null;
            bool have = false;
            for (int i = 0; i < _characterPlacement.UnitEnemy.Count; i++)
            {
                List<GameObject> effects = _characterPlacement.UnitEnemy[i].DebuffList;
                if (_characterPlacement.UnitEnemy[i].Weapon.Damage > maxDamage &&
                    !effects.Any(item => item.GetComponent<AbstractSpell>().id == 18))
                {
                    maxDamage = _characterPlacement.UnitEnemy[i].Weapon.Damage * _characterPlacement.UnitEnemy[i].Weapon.Times;
                    maxDamageUnit = _characterPlacement.UnitEnemy[i];
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
                UseSpell(_characterPlacement.UnitEnemy[0], 0);
                return false;
            }
            UseSpell(maxDamageUnit, 1);
            return false;
        }
        //LizardWarrior
        else if (Turns.turnUnit.Id == 17)
        {
            for (int i = 0; i < _characterPlacement.UnitEnemy.Count; i++)
            {
                if (_characterPlacement.UnitEnemy[i].Type == 1 ||
                    _characterPlacement.UnitEnemy[i].Type == 2 ||
                    _characterPlacement.UnitEnemy[i].Type == 3 ||
                    _characterPlacement.UnitEnemy[i].Type == 4)
                {
                    UseSpell(_characterPlacement.UnitEnemy[i], 0);
                    return false;
                }
            }
            return true;
        }
        //MoonMage
        else if (Turns.turnUnit.Id == 18)
        {
            int maxDamage = 0;
            UnitProperties maxDamageUnit = null;
            for (int i = 0; i < _characterPlacement.UnitOur.Count; i++)
            {
                List<GameObject> effects = _characterPlacement.UnitOur[i].DebuffList;
                if (!effects.Any(item => item.GetComponent<AbstractSpell>().id == 29) && _characterPlacement.UnitOur[i].Weapon.Damage * _characterPlacement.UnitOur[i].Weapon.Times > maxDamage)
                {
                    maxDamage = _characterPlacement.UnitOur[i].Weapon.Damage * _characterPlacement.UnitOur[i].Weapon.Times;
                    maxDamageUnit = _characterPlacement.UnitOur[i];
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
        else if (Turns.turnUnit.Id == 14)
        {
            for (int i = 0; i < _characterPlacement.UnitOur.Count; i++)
            {
                if (_characterPlacement.UnitOur[i].HpCharacter.HpProsent <= 65 &&
                    !_characterPlacement.UnitOur[i].DebuffList.Any(item => item.GetComponent<AbstractSpell>().id == 36))
                {
                    UseSpell(_characterPlacement.UnitOur[i], 0);
                    return false;
                }
            }
            bool have = false;
            for (int i = 0; i < _characterPlacement.UnitOur.Count; i++)
            {
                if (_characterPlacement.UnitOur[i].Id == 44)
                {
                    have = true;
                    break;
                }
            }
            if (have == true)
            {
                int maxDamage = 0;
                UnitProperties maxDamageUnit = null;
                for (int i = 0; i < _characterPlacement.UnitOur.Count; i++)
                {
                    List<GameObject> effects = _characterPlacement.UnitOur[i].DebuffList;
                    if (_characterPlacement.UnitOur[i].Id != 44 && !effects.Any(item => item.GetComponent<AbstractSpell>().id == 37) && _characterPlacement.UnitOur[i].Weapon.Damage > maxDamage)
                    {
                        maxDamage = _characterPlacement.UnitOur[i].Weapon.Damage;
                        maxDamageUnit = _characterPlacement.UnitOur[i];
                    }
                }
                if (maxDamageUnit != null)
                {
                    UseSpell(maxDamageUnit, 1);
                    return false;
                }
            }
            for (int i = 0; i < _characterPlacement.UnitOur.Count; i++)
            {
                if (_characterPlacement.UnitOur[i].HpCharacter.HpProsent <= 90 &&
                    !_characterPlacement.UnitOur[i].DebuffList.Any(item => item.GetComponent<AbstractSpell>().id == 36))
                {
                    UseSpell(_characterPlacement.UnitOur[i], 0);
                    return false;
                }
            }
            return true;
        }
        //Fermor
        else if (Turns.turnUnit.Id == 10)
        {
            for (int i = 0; i < _characterPlacement.UnitEnemy.Count; i++)
            {       //Âàìïèðèê
                if (((_characterPlacement.UnitEnemy[i].HpCharacter.HpProsent <= 70 && (_characterPlacement.UnitEnemy[i].Id == 35 || _characterPlacement.UnitEnemy[i].Id == 8)) ||
                    //Ëåäÿíîé õèëë
                    (!_characterPlacement.UnitEnemy[i].DebuffList.Any(item => item.GetComponent<AbstractSpell>().id == 36) && _characterPlacement.UnitEnemy[i].HpCharacter.HpProsent <= 90) ||
                    //êðèòòåð
                    (_characterPlacement.UnitEnemy[i].Energy.energy >= 3 && _characterPlacement.UnitEnemy[i].Id == 37)) &&
                    !_characterPlacement.UnitEnemy[i].DebuffList.Any(item => item.GetComponent<AbstractSpell>().id == 39))
                {
                    UseSpell(_characterPlacement.UnitEnemy[i], 1);
                    return false;
                }
            }
            for (int i = 0; i < _characterPlacement.UnitEnemy.Count; i++)
            {
                if (_characterPlacement.UnitEnemy[i].HpCharacter.Vulnerability == 5)
                {
                    UseSpell(_characterPlacement.UnitEnemy[i], 0);
                    return false;
                }
            }
            UseSpell(_characterPlacement.UnitEnemy[Random.Range(0, _characterPlacement.UnitEnemy.Count)], 0);
            return false;

        }
        //Goblin
        else if (Turns.turnUnit.Id == 19)
        {
            int maxDamage = 0;
            UnitProperties maxDamageUnit = null;
            for (int i = 0; i < _characterPlacement.UnitEnemy.Count; i++)
            {
                if (_characterPlacement.UnitEnemy[i].Weapon.Damage > maxDamage &&
                    !_characterPlacement.UnitEnemy[i].DebuffList.Any(item => item.GetComponent<AbstractSpell>().id == 4))
                {
                    maxDamage = _characterPlacement.UnitEnemy[i].Weapon.Damage;
                    maxDamageUnit = _characterPlacement.UnitEnemy[i];
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
        else if (Turns.turnUnit.Id == 16)
        {
            UseSpell(SeekMaxMinDamageHpUnit(_characterPlacement.UnitEnemy, "max", "damage"), 0);
            StartCoroutine(Async(40));
            return false;
        }
        //Necromancer
        else if (Turns.turnUnit.Id == 5)
        {
            for (int i = 0; i < _characterPlacement.UnitEnemy.Count; i++)
            {
                if (_characterPlacement.UnitEnemy[i].Energy.energy >= _characterPlacement.UnitEnemy[i].EnergyÑonsumption &&
                   _characterPlacement.UnitEnemy[i].Spells.SpellList.Any(item => item.GetComponent<AbstractSpell>().Type == "Debuff"))
                {
                    for (int i2 = 0; i2 < _characterPlacement.UnitOur.Count; i2++)
                    {
                        if (!_characterPlacement.UnitOur[i2].DebuffList.Any(item => item.GetComponent<AbstractSpell>().id == 31))
                        {
                            UseSpell(_characterPlacement.UnitOur[i2], 1);
                            return false;
                        }
                    }

                }
            }
            for (int i = 0; i < _characterPlacement.UnitOur.Count; i++)
            {
                if (!_characterPlacement.UnitOur[i].DebuffList.Any(item => item.GetComponent<AbstractSpell>().id == 40))
                {
                    UseSpell(_characterPlacement.UnitOur[Random.Range(0, _characterPlacement.UnitOur.Count)], 0);
                    return false;
                }
            }
            return true;
        }
        //Falnce Shaman
        else if (Turns.turnUnit.Id == 36)
        {
            for (int i = 0; i < _characterPlacement.UnitOur.Count; i++)
            {
                if ((_characterPlacement.UnitOur[i].Type == 0) &&
                    !_characterPlacement.UnitOur[i].DebuffList.Any(item => item.GetComponent<AbstractSpell>().id == 15))
                {
                    UseSpell(_characterPlacement.UnitOur[i], 0);
                    return false;
                }
            }
            for (int i = 0; i < _characterPlacement.UnitOur.Count; i++)
            {
                if (!_characterPlacement.UnitOur[i].DebuffList.Any(item => item.GetComponent<AbstractSpell>().id == 15))
                {
                    UseSpell(_characterPlacement.UnitOur[i], 0);
                    return false;
                }
            }
            return true;
        }
        //Twins
        else if (Turns.turnUnit.Id == 32)
        {
            if (!Turns.turnUnit.DebuffList.Any(item => item.GetComponent<AbstractSpell>().id == 28))
            {
                int maxDamage = -1;
                UnitProperties maxDamageUnit = null;
                for (int i = 0; i < _characterPlacement.UnitOur.Count; i++)
                {
                    if (_characterPlacement.UnitOur[i].Weapon.Damage * _characterPlacement.UnitOur[i].Weapon.Times > maxDamage &&
                        _characterPlacement.UnitOur[i] != Turns.turnUnit)
                    {
                        maxDamage = _characterPlacement.UnitOur[i].Weapon.Damage * _characterPlacement.UnitOur[i].Weapon.Times;
                        maxDamageUnit = _characterPlacement.UnitOur[i];
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
            for (int i = 0; i < _characterPlacement.UnitOur.Count; i++)
            {
                if (_characterPlacement.UnitOur[i].Weapon.Damage * _characterPlacement.UnitOur[i].Weapon.Times < minDamage &&
                    !_characterPlacement.UnitOur[i].DebuffList.Any(item => item.GetComponent<AbstractSpell>().id == 35))
                {
                    minDamage = _characterPlacement.UnitOur[i].Weapon.Damage * _characterPlacement.UnitOur[i].Weapon.Times;
                    minDamageUnit = _characterPlacement.UnitOur[i];
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
        else if (Turns.turnUnit.Id == 37)
        {
            if (Turns.turnUnit.HpCharacter.HpProsent < 95 &&
                !Turns.turnUnit.DebuffList.Any(item => item.GetComponent<AbstractSpell>().id == 39))
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
            allow.AddRange(_characterPlacement.UnitAll);
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

                ((currentSpell.ToEnemy == true && allow[rand2].ParentCircle.Side != Turns.turnUnit.ParentCircle.Side) ||
                (currentSpell.ToEnemy == false && allow[rand2].ParentCircle.Side == Turns.turnUnit.ParentCircle.Side))
                &&
                (currentSpell.AllPlace == true ||
                (currentSpell.AllPlace == false && allow[rand2].CharacterState.AllowHit == true))
                &&
                (currentSpell.NotMe == false ||
                (currentSpell.NotMe == true && allow[rand2] != Turns.turnUnit)))
            {
                if (currentSpell.state == "Effect")
                {
                    bool fail = allow[rand2].DebuffList.Any(item => item.GetComponent<AbstractSpell>().id == currentSpell.id);
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
            if (type == List[i].HpCharacter.InpDamageType)
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
            if (what == "hp" && ((mode == "max" && List[i].HpCharacter.HpProsent > target) ||
                (mode == "min" && List[i].HpCharacter.HpProsent < target)))
            {
                target = Convert.ToInt32(List[i].HpCharacter.HpProsent);
                targetUnit = List[i];
            }
            else if (what == "damage" && ((mode == "max" && List[i].Weapon.Damage > target) ||
                (mode == "min" && List[i].Weapon.Damage < target)))
            {
                target = List[i].Weapon.Damage;
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
        //StartIni.battleNetwork.AttackQuery(spellIndex, Turns.turnUnit.pathSpells.modeIndex, BattleNetwork.ident, unitTarget.Side, unitTarget.Place);
    }
    private IEnumerator Async(int id)
    {
        if (id == 42)
        {
            int mode = 0;
            UnitProperties targetUnit = null;
            if (Turns.turnUnit.HpCharacter.HpProsent <= 60)
            {
                for (int i = 0; i < Turns.listAllowHit.Count; i++)
                {
                    if (Turns.listAllowHit[i].HpCharacter.Resist != 7)
                        targetUnit = Turns.listAllowHit[i];
                }
            }
            if (targetUnit == null)
            {
                mode = 1;
                for (int i = 0; i < Turns.listAllowHit.Count; i++)
                {
                    if (Turns.listAllowHit[i].HpCharacter.Vulnerability != 7)
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
                if (Turns.turnUnit.Spells.modeIndex != 1)
                {
                    Turns.turnUnit.Spells.SetState(1);
                    yield return new WaitForSeconds(0.6f);
                }
            }
            else
            {
                if (Turns.turnUnit.Spells.modeIndex != 0)
                {
                    Turns.turnUnit.Spells.SetState(0);
                    yield return new WaitForSeconds(0.6f);
                }
            }
            if (Turns.turnUnit.Energy.energy >= 3)
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
            if (Turns.turnUnit.Energy.energy >= 3)
            {
                Turns.unitChoose = SeekMaxMinDamageHpUnit(_characterPlacement.UnitEnemy, "max", "damage");
                //StartCoroutine(Turns.turnUnit.PathSpells.UseActiveAsync(2));
                //Turns.turnUnit.PathEnergy.SetEnergy(3, '-');
            }
            else
            {
                bool have = false;
                for (int i = 0; i < _characterPlacement.UnitEnemy.Count; i++)
                {
                    if (_characterPlacement.UnitEnemy[i].HpCharacter.Vulnerability != 7 &&
                        SeekUnitDamageType(_characterPlacement.UnitEnemy, _characterPlacement.UnitEnemy[i].HpCharacter.Vulnerability) == true)
                    {
                        Turns.unitChoose = _characterPlacement.UnitEnemy[i];
                        have = true;
                        break;
                    }
                }
                if (have == true)
                {
                    if (Turns.turnUnit.Spells.modeIndex != 0)
                    {
                        Turns.turnUnit.Spells.SetState(0);
                        yield return new WaitForSeconds(0.6f);
                    }
                    //Turns.turnUnit.AttackedUnit(Turns.UnitChoose);
                }
                else
                {
                    for (int i = 0; i < _characterPlacement.UnitEnemy.Count; i++)
                    {
                        if (_characterPlacement.UnitEnemy[i].HpCharacter.Resist != 7 &&
                            SeekUnitDamageType(_characterPlacement.UnitEnemy, _characterPlacement.UnitEnemy[i].HpCharacter.Resist) == true)
                        {
                            Turns.unitChoose = _characterPlacement.UnitEnemy[i];
                            have = true;
                            break;
                        }
                    }
                    if (have == true)
                    {
                        if (Turns.turnUnit.Spells.modeIndex != 1)
                        {
                            Turns.turnUnit.Spells.SetState(1);
                            yield return new WaitForSeconds(0.6f);
                        }
                        //Turns.turnUnit.AttackedUnit(Turns.UnitChoose);
                    }
                    else
                    {
                        Turns.unitChoose = _characterPlacement.UnitEnemy[Random.Range(0, _characterPlacement.UnitEnemy.Count)];
                        //Turns.turnUnit.AttackedUnit(Turns.UnitChoose);
                    }
                }
            }
        }
    }
}
