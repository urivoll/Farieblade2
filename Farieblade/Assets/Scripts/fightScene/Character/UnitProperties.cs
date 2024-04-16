using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetSelector))]
[RequireComponent(typeof(CharacterInfoPanelOpener))]
public class UnitProperties : MonoBehaviour
{
    // ŒŒ–ƒ»Õ¿“€ Õ¿  ¿–“≈
    [HideInInspector] public int Side => _side;
    [HideInInspector] public int Place => _place;

    private int _side;
    private int _place;

    //œ¿–¿Ã≈“–€ œ≈–—ŒÕ¿∆¿
    [HideInInspector] public int initiative;
    [HideInInspector] public int hp;
    [HideInInspector] public int hpBase;
    [HideInInspector] public int damage;
    [HideInInspector] public int accuracy;
    [HideInInspector] public int state;
    public int times;
    [HideInInspector] public string race;
    public float behiendTimes;
    //œ”“»
    [HideInInspector] public Unit pathParent;
    [HideInInspector] public EnergyUnit pathEnergy;
    [HideInInspector] public Spells pathSpells;
    [HideInInspector] public CircleProperties pathCircle;
    [HideInInspector] public Transform pathDebuffs;
    [HideInInspector] public Transform pathBulletTarget;
    [HideInInspector] public UnitAnimation pathAnimation;
    private UnitCanvas pathCanvas;
    //—Œ—“ŒﬂÕ»≈
    [HideInInspector] public float resistance = 1;
    [HideInInspector] public float hpProsent = 100;
    [HideInInspector] public bool paralize = false;
    [HideInInspector] public bool went = false;
    [HideInInspector] public bool allowHit = false;
    [HideInInspector] public bool resurect = false;
    public bool silence = false;
    [HideInInspector] public bool heal = true;
    //¬’ŒƒﬂŸ»≈ «Õ¿◊≈Õ»ﬂ
    public float vul = 0.5f;
    public float res = 0.5f;
    [HideInInspector] public float inpDamage;
    [HideInInspector] public int inpDamageType;
    //Œ—“¿À‹ÕŒ≈
    public AudioClip[] soundVoiceStrike;
    [HideInInspector] public List<GameObject> idDebuff = new();
    private EffectManager _effectManager;
    private Weapon _weapon;
    private GameObject slideDamage2;
    private Animator animatorCanvas;
    public Aura aura;
    public int indexVoice;

    public void Init(int side, int place)
    {
        _side = side;
        _place = place;
    }
    public void Awake()
    {
        //»Õ»÷»¿À»«¿÷»ﬂ œ”“≈…
        pathBulletTarget = transform.Find("BulletTarget");
        pathDebuffs = transform.parent.Find("Canvas/Debuffs");
        pathSpells = GetComponent<Spells>();
        pathParent = GetComponentInParent<Unit>();
        pathEnergy = transform.parent.GetComponentInChildren<EnergyUnit>();
        pathCanvas = transform.parent.transform.Find("Canvas").gameObject.GetComponent<UnitCanvas>();
        //ÓÒÚ‡Î¸ÌÓÂ
    }
    public void Instantiate()
    {
        animatorCanvas = transform.parent.gameObject.transform.Find("Canvas").gameObject.GetComponent<Animator>();
        _effectManager = Camera.main.GetComponent<EffectManager>();
        //— »Õ€
        pathAnimation = GetComponent<UnitAnimation>();
        //»Õ»÷»¿À»«¿÷»ﬂ œ¿–¿Ã≈“–Œ¬
        initiative = pathParent.initiative;
        hpBase = pathParent.hpBase;
        hp = hpBase;
        accuracy = pathParent.accuracy;
        damage = pathParent.damage;
        state = pathParent.state;
        times = pathParent.times;
        _weapon = GetComponent<Weapon>();
        HpDamage("none");
    }

    //œÓÎÛ˜ÂÌËÂ ÛÓÌ‡!
    public void SpellDamage(float inpDamage, int inpdamageType)
    {
        this.inpDamageType = inpdamageType;
        this.inpDamage = inpDamage;
        ResVul();
        Turns.takeDamage?.Invoke(this);
        hp -= Convert.ToInt32(inpDamage);
        HpDamage("hp");
        TryToDeath();
    }
    public void TakeDamage(UnitProperties who, MakeMove inpData)
    {
        inpDamageType = inpData.attackSend["element"];
        Turns.beforePunch?.Invoke(this, who);
        inpDamage = inpData.attackSend["damage"];
        ResVul();
        if (inpData.punchSend.Count > 0) Turns.punch?.Invoke(this, who, inpData.punchSend);
        Turns.takeDamage?.Invoke(this);
        hp = Convert.ToInt32(inpData.attackSend["hp"]);
        HpDamage("hp");
        TryToDeath();
    }
    public void AttackedUnit(List<MakeMove> attack)
    {
        pathAnimation.TryGetAnimation("attack");
        StartCoroutine(_weapon.Attack(this, attack));
    }
    public void HpDamage(string hpDmg)
    {
        if(hpDmg == "hp" || hpDmg == "hpdmg") 
            hpProsent = hp * 100 / hpBase;
        pathCanvas.UnitPropTextRenderer(hp, damage, hpProsent, state, this, hpDmg);
    }

    //œÓ‚ÂÍ‡ Ì‡ ÒÎ‡·ÓÒÚË Ë ÂÁËÒÚ
    private void ResVul()
    {
        if (inpDamage <= 0) return;
        if (inpDamageType == pathParent.vulnerability)
        {
            BattleSound.sound.PlayOneShot(BattleSound.soundClip[0]);
            StartCoroutine(_effectManager.ShowEffect("vul", gameObject));
        }
        else if (inpDamageType == pathParent.resist)
        {
            BattleSound.sound.PlayOneShot(BattleSound.soundClip[1]);
            StartCoroutine(_effectManager.ShowEffect("resist", gameObject));
        }
    }

    public void Miss()
    {
        //GameObject slideDamage2 = Instantiate(slideDamagePrefub, new Vector2(gameObject.transform.position[0], gameObject.transform.position[1] + 3), Quaternion.identity);
        slideDamage2.GetComponent<SlideDamage>().UpdateSlideDamage(-1, -2);
    }

    //—ÏÂÚ¸
    private void TryToDeath()
    {
        if (hp > 1)
        {
            if (inpDamage > 0)
            {
                pathAnimation.TryGetAnimation("hit");
                StartIni.soundVoice.HitVoices(indexVoice, true);
            }
            return;
        }
        StartIni.soundVoice.HitVoices(indexVoice, false);
        if (Turns.currentTryDeath.Count > 0)
        {
            for (int i = 0; i < Turns.currentTryDeath.Count; i++)
            {
                if (Side != Turns.currentTryDeath[i]["side"] ||
                   Place != Turns.currentTryDeath[i]["place"]) 
                    continue;
                Turns.tryDeath?.Invoke(this, Turns.currentTryDeath[i]);
            }
        }
        animatorCanvas.SetTrigger("end");
        hp = 0;
        if (pathParent.ID != 4 &&
            pathParent.ID != 13 &&
            pathParent.ID != 44 &&
            pathParent.fraction != 9)
            Invoke("BodyFallSound", 0.6f);
        pathAnimation.TryGetAnimation("death");
        if (resurect) return;
        if (this == Turns.turnUnit) Turns.turnUnit = null;
        if (this == Turns.unitChoose) Turns.unitChoose = null;
/*        Turns.listUnitAll.Remove(this);
        if (Side == 0) Turns.listUnitLeft.Remove(this);
        else if (Side == 1) Turns.listUnitRight.Remove(this);*/
        pathCircle.newObject = null;
        Destroy(pathParent.transform.Find("Fight/Canvas").gameObject, 0.8f);
        Destroy(pathParent.gameObject, 1.5f);
    }
    private void BodyFallSound()
    {
        BattleSound.sound.PlayOneShot(BattleSound.soundClip[2]);
        if (pathParent.Type == 3) StartIni.animatorShakeStatic.SetTrigger("shakeShort");
    }
}