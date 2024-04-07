using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
public class UnitProperties : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    // ŒŒ–ƒ»Õ¿“€ Õ¿  ¿–“≈
    [HideInInspector] public int sideOnMap;
    [HideInInspector] public int placeOnMap;
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
    private Shooter _shooter;
    private Melee _melee;
    private GameObject slideDamagePrefub;
    private GameObject slideDamage2;
    private Animator animatorCanvas;
    public Aura aura;
    private bool workProperties = false;
    private Coroutine workCoroutine;
    public int indexVoice;
    public bool moved = false;
    public UnitProperties pushUnit;
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
        slideDamagePrefub = Camera.main.GetComponent<Turns>().slideDamagePrefub;
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

        if (state == 1) _shooter = GetComponent<Shooter>();
        else if (state == 0 || state == 2) _melee = GetComponent<Melee>();
        HpDamage("none");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (pathCircle.newObject == null ||
            !eventData.pointerClick.GetComponent<UnitProperties>().allowHit ||
            SideUnitUi.modeBlock == true || 
            SideUnitUi.spell == -555) return;
        StartIni.battleNetwork.AttackQuery(SideUnitUi.spell, Turns.turnUnit.pathSpells.modeIndex, BattleNetwork.ident, sideOnMap, placeOnMap);
        SideUnitUi.spell = -555;
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

        if (inpDamage > 0)
            slideDamage2 = Instantiate(slideDamagePrefub, new Vector2(gameObject.transform.position[0], gameObject.transform.position[1] + 3), Quaternion.identity);
        if (slideDamage2 != null) slideDamage2.GetComponent<SlideDamage>().UpdateSlideDamage(inpDamage, inpDamageType);
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
        if (inpDamage > 0)
            slideDamage2 = Instantiate(slideDamagePrefub, new Vector2(gameObject.transform.position[0], gameObject.transform.position[1] + 3), Quaternion.identity);
        if (slideDamage2 != null) slideDamage2.GetComponent<SlideDamage>().UpdateSlideDamage(inpDamage, inpDamageType);
        TryToDeath();
    }

    public void HpDamage(string hpDmg)
    {
        if(hpDmg == "hp" || hpDmg == "hpdmg") hpProsent = hp * 100 / hpBase;
        pathCanvas.UnitPropTextRenderer(hp, damage, hpProsent, state, this, hpDmg);
    }

    //œÓ‚ÂÍ‡ Ì‡ ÒÎ‡·ÓÒÚË Ë ÂÁËÒÚ
    private void ResVul()
    {
        if (inpDamageType == pathParent.vulnerability && inpDamage > 0)
        {
            BattleSound.sound.PlayOneShot(BattleSound.soundClip[0]);
            StartCoroutine(_effectManager.ShowEffect("vul", gameObject));
        }
        else if (inpDamageType == pathParent.resist && inpDamage > 0)
        {
            BattleSound.sound.PlayOneShot(BattleSound.soundClip[1]);
            StartCoroutine(_effectManager.ShowEffect("resist", gameObject));
        }
    }
    //¿Ú‡Í‡
    public void AttackedUnit(UnitProperties unitTarget, List<MakeMove> attack)
    {
        pathAnimation.SetCaracterState("attack");
        if (state == 1) StartCoroutine(_shooter.Shoot(unitTarget, this, attack));
        else
        {
            if(Turns.turnUnit.placeOnMap != unitTarget.placeOnMap)
            {
                moved = true;
                Transform newPosition;
                if(unitTarget.placeOnMap % 2 != 0) newPosition = Turns.circlesMap[Turns.enemySide, unitTarget.placeOnMap - 1].transform;
                else
                {
                    if(Turns.circlesMap[Turns.turnUnit.sideOnMap, unitTarget.placeOnMap].newObject != null)
                    {
                        pushUnit = Turns.circlesMap[Turns.turnUnit.sideOnMap, unitTarget.placeOnMap].newObject;
                        if (pushUnit.sideOnMap == 1) pushUnit.transform.localPosition += new Vector3(2f, 0, 0);
                        else pushUnit.transform.localPosition -= new Vector3(2f, 0, 0);
                        //pushUnit.Skins[pushUnit.currentSkin].GetComponent<SkeletonPartsRenderer>().MeshRenderer.sortingLayerName = "Default";
                        newPosition = pushUnit.pathCircle.transform;
                        pathParent.transform.SetParent(pushUnit.GetComponent<UnitProperties>().pathCircle.transform);
                        pathParent.transform.localScale = new Vector2(1, 1);
                        //transform.localScale = new Vector2(0.55f * pathCircle.intState, 0.55f);
                    }
                    else newPosition = Turns.circlesMap[Turns.turnUnit.sideOnMap, unitTarget.placeOnMap].transform;
                }
                gameObject.transform.position = newPosition.position;
            }
            StartCoroutine(_melee.Punch(unitTarget, this, attack));
        }
    }
    public void Miss()
    {
        GameObject slideDamage2 = Instantiate(slideDamagePrefub, new Vector2(gameObject.transform.position[0], gameObject.transform.position[1] + 3), Quaternion.identity);
        slideDamage2.GetComponent<SlideDamage>().UpdateSlideDamage(-1, -2);
    }

    //—ÏÂÚ¸
    private void TryToDeath()
    {
        if (hp > 1)
        {
            if (inpDamage > 0)
            {
                pathAnimation.SetCaracterState("hit");
                StartIni.soundVoice.HitVoices(indexVoice, true);
            }
            return;
        }
        StartIni.soundVoice.HitVoices(indexVoice, false);
        if (Turns.currentTryDeath.Count > 0)
        {
            for (int i = 0; i < Turns.currentTryDeath.Count; i++)
            {
                if (sideOnMap != Turns.currentTryDeath[i]["side"] ||
                   placeOnMap != Turns.currentTryDeath[i]["place"]) continue;
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
        pathAnimation.SetCaracterState("death");
        if (resurect) return;
        if (this == Turns.turnUnit) Turns.turnUnit = null;
        if (this == Turns.unitChoose) Turns.unitChoose = null;
        Turns.listUnitAll.Remove(this);
        if (sideOnMap == 0) Turns.listUnitLeft.Remove(this);
        else if (sideOnMap == 1) Turns.listUnitRight.Remove(this);
        pathCircle.newObject = null;
        Destroy(pathParent.transform.Find("Fight/Canvas").gameObject, 0.8f);
        Destroy(pathParent.gameObject, 1.5f);
    }
    private void BodyFallSound()
    {
        BattleSound.sound.PlayOneShot(BattleSound.soundClip[2]);
        if (pathParent.Type == 3) StartIni.animatorShakeStatic.SetTrigger("shakeShort");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Turns.unitChoose = this;
        workCoroutine = StartCoroutine(PointDown());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (workCoroutine == null) return;
        StopCoroutine(workCoroutine);
        StartIni.circleProperties.gameObject.SetActive(false);
        workProperties = false;
    }
    private IEnumerator PointDown()
    {
        yield return new WaitForSeconds(0.2f);
        workProperties = true;
        StartIni.circleProperties.gameObject.SetActive(true);
        StartIni.circleProperties.transform.position = pathBulletTarget.position;
        StartIni.circleProperties.fillAmount = 0;
        yield return new WaitForSeconds(0.3f);
        workProperties = false;
        StartIni.circleProperties.gameObject.SetActive(false);
        workCoroutine = null;
        StartIni.unitProperties.SetActive(true);
        pathParent.ShowUnitProperties();
    }
    private void Update()
    {
        if (workProperties == true) StartIni.circleProperties.fillAmount = Mathf.Lerp(StartIni.circleProperties.fillAmount, 1, 8f * Time.deltaTime);
    }
}