using System;
using UnityEngine;
using Zenject;

public class HpCharacter : MonoBehaviour
{
    public int Hp => _hp;
    public int HpBase => _hpBase;
    public float HpProsent => _hpProsent;
    public float InpDamage => _inpDamage;
    public int InpDamageType => _inpDamageType;
    public int Vulnerability => _vulnerability;
    public int Resist => _resist;

    public bool heal = true;
    public bool resurect = false;

    private float _inpDamage;
    private int _inpDamageType;
    private int _hp;
    private int _hpBase;
    private float _hpProsent = 100;
    private UnitProperties _unitProperties;
    private int _vulnerability;
    private int _resist;
    [Inject] private CharacterPlacement _characterPlacement;

    public void Init(UnitProperties unitProperties, CharacterAttributes attributes)
    {
        _unitProperties = unitProperties;
        _hpBase = (int)attributes.Hp;
        _hp = HpBase;
        HpDamage("none");
    }

    public void SpellDamage(float inpDamage, int inpdamageType)
    {
        _inpDamageType = inpdamageType;
        _inpDamage = inpDamage;
        //ResVul();
        Turns.takeDamage?.Invoke(_unitProperties);
        _hp -= Convert.ToInt32(inpDamage);
        HpDamage("hp");
        TryToDeath();
    }

/*    private void ResVul()
    {
        if (_inpDamage <= 0) return;
        if (_inpDamageType == _unitProperties.pathParent.vulnerability)
        {
            BattleSound.sound.PlayOneShot(BattleSound.soundClip[0]);
            StartCoroutine(_effectManager.ShowEffect("vul", gameObject));
        }
        else if (_inpDamageType == _unitProperties.pathParent.resist)
        {
            BattleSound.sound.PlayOneShot(BattleSound.soundClip[1]);
            StartCoroutine(_effectManager.ShowEffect("resist", gameObject));
        }
    }*/

    public void TakeDamage(UnitProperties who, MakeMove inpData)
    {
        _inpDamageType = inpData.attackSend["element"];
        Turns.beforePunch?.Invoke(_unitProperties, who);
        _inpDamage = inpData.attackSend["damage"];
        //ResVul();
        if (inpData.punchSend.Count > 0) Turns.punch?.Invoke(_unitProperties, who, inpData.punchSend);
        Turns.takeDamage?.Invoke(_unitProperties);
        _hp = Convert.ToInt32(inpData.attackSend["hp"]);
        HpDamage("hp");
        TryToDeath();
    }

    public void HpDamage(string hpDmg)
    {
        if (hpDmg == "hp" || hpDmg == "hpdmg")
            _hpProsent = Hp * 100 / _hpBase;
        _unitProperties.UI.UnitPropTextRenderer(_hp, _unitProperties.Weapon.Damage, _hpProsent, hpDmg);
    }

    private void TryToDeath()
    {
        if (_hp > 1)
        {
            if (InpDamage > 0)
            {
                _unitProperties.Animation.TryGetAnimation("hit");
                StartIni.soundVoice.HitVoices(_unitProperties.indexVoice, true);
            }
            return;
        }
        StartIni.soundVoice.HitVoices(_unitProperties.indexVoice, false);
        if (Turns.currentTryDeath.Count > 0)
        {
            for (int i = 0; i < Turns.currentTryDeath.Count; i++)
            {
                if (_unitProperties.ParentCircle.Side != Turns.currentTryDeath[i]["side"] ||
                   _unitProperties.ParentCircle.Place != Turns.currentTryDeath[i]["place"])
                    continue;
                Turns.tryDeath?.Invoke(_unitProperties, Turns.currentTryDeath[i]);
            }
        }
        _hp = 0;
        if (_unitProperties.Id != 4 &&
            _unitProperties.Id != 13 &&
            _unitProperties.Id != 44 &&
            _unitProperties.Id != 9)
            _unitProperties.SoundDie();
        _unitProperties.Animation.TryGetAnimation("death");
        if (resurect) return;
        if (_unitProperties == Turns.turnUnit) Turns.turnUnit = null;
        if (_unitProperties == Turns.unitChoose) Turns.unitChoose = null;
        _characterPlacement.DeleteCharacter(_unitProperties, _unitProperties.ParentCircle.Side);
        _unitProperties.ParentCircle.TryDeleteChild();
        _unitProperties.Die();
    }
}
