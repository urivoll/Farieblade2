using System;
using Zenject;

public class HpCharacter 
{
    public int Hp => _hp;
    public int HpBase => _hpBase;
    public float HpProsent => _hpProsent;
    public float InpDamage => _inpDamage;
    public int InpDamageType => _inpDamageType;

    public bool heal = true;
    public bool resurect = false;

    private float _inpDamage;
    private int _inpDamageType;
    private int _hp;
    private int _hpBase;
    private float _hpProsent = 100;
    private UnitProperties _unitProperties;
    [Inject] private CharacterPlacement _characterPlacement;

    public HpCharacter(UnitProperties unitProperties, CharacterAttributes attributes)
    {
        _unitProperties = unitProperties;
        _hpBase = attributes.Hp;
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
        //pathCanvas.UnitPropTextRenderer(hp, damage, hpProsent, state, this, hpDmg);
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
        if (_unitProperties.pathParent.ID != 4 &&
            _unitProperties.pathParent.ID != 13 &&
            _unitProperties.pathParent.ID != 44 &&
            _unitProperties.pathParent.fraction != 9)
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
