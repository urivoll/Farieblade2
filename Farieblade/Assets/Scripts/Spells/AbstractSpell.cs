using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AbstractSpell : MonoBehaviour
{
    [HideInInspector] public int duration;
    [HideInInspector] public UnitProperties parentUnit;
    [HideInInspector] public Unit fromUnit;
    [HideInInspector] public int startNumberTurn;
    public int id;
    public bool PeriodicBool;
    public GameObject Effect;
    public string state;
    public bool ToEnemy;
    public bool AllPlace;
    public bool NotMe;
    public AudioClip[] voiceBefore;
    public AudioClip soundBefore;
    public AudioClip soundMid;
    public AudioClip[] voiceAfter;
    public AudioClip[] voiceDone;
    public AudioClip soundAfter;
    [HideInInspector] public string nameText;
    [HideInInspector] public string description;
    [HideInInspector] public string SType;
    public string Animation;
    public float timeBeforeShoot;
    public float prosentDamage;
    public string Type;
    public int times;
    public float between;
    private GameObject CurrentEffect;
    public bool onGame = false;
    private void Awake()
    {
        if (transform.parent.gameObject.name == "Spells") fromUnit = transform.parent.transform.parent.gameObject.GetComponent<Unit>();
        else SetEffect();
    }
    public virtual IEnumerator HitEffect(Dictionary<string, int> inpData) {yield return null;}
    public virtual void BeforeHit() {}
    public virtual void SwishMethod(int count) { }
    public virtual void PeriodicMethod(Dictionary<string, int> inpData) { }
    public virtual void StuckMethod() { }
    public virtual IEnumerator AfterStep(Dictionary<string, int> inpData) { yield return null; }
    public virtual void EndDebuff() { }
    private void OnDestroy()
    {
        if (CurrentEffect != null) CurrentEffect.SetActive(false);
        if (transform.parent.gameObject.name == "Debuffs")
        {
            EndDebuff();
            parentUnit.idDebuff.Remove(gameObject);
        }
    }
    private void SetEffect()
    {
        parentUnit = transform.parent.parent.parent.Find("Model").GetComponent<UnitProperties>();
        if (state == "Passive") fromUnit = transform.parent.parent.parent.parent.GetComponent<Unit>();
        else if (state != "Mode") Turns.getDebuff?.Invoke(gameObject, parentUnit);
        int mode = -666;
        for (int i = 0; i < parentUnit.idDebuff.Count; i++)
        {
            AbstractSpell parentSpell = parentUnit.idDebuff[i].GetComponent<AbstractSpell>();
            if (parentSpell.id != id || id == 0) continue;
            //Œƒ»Õ¿ Œ¬€… ƒ≈¡¿‘‘
            if (state != "Mode")
            {
                if (Effect != null) Instantiate(Effect, parentUnit.pathBulletTarget.position, Quaternion.identity);
                parentSpell.StuckMethod();
                parentSpell.startNumberTurn = Turns.numberTurn + duration;
                Destroy(gameObject);
            }
            //»«Ã≈Õ≈Õ»≈ –≈∆»Ã¿
            else
            {
                Destroy(parentUnit.idDebuff[i]);
                mode = i;
            }
            break;
        }
        if (mode == -666) parentUnit.idDebuff.Add(gameObject);
        else parentUnit.idDebuff.Insert(mode, gameObject);
        if (Effect != null) CurrentEffect = Instantiate(Effect, parentUnit.pathBulletTarget.position, Quaternion.identity);
    }
}