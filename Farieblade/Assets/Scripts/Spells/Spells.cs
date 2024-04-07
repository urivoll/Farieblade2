using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class Spells : MonoBehaviour
{
    public List<GameObject> SpellList;
    public List<GameObject> modeList;
    [HideInInspector] public int modeIndex = -1;
    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject effectBefore;
    [SerializeField] private GameObject useSpell;
    private GameObject skill;
    private Image skillPic;
    private UnitProperties parentObject;
    public void Start()
    {
        parentObject = GetComponent<UnitProperties>();
        skill = transform.parent.transform.Find("Canvas/Skill").gameObject;
        skillPic = skill.transform.Find("Mask/Pic").GetComponent<Image>();
        if (modeList.Count > 0) modeIndex = 0;
        else modeIndex = -1;
    }
    public void UseActive(int i, List<MakeMove> inpData) => StartCoroutine(UseActiveAsync(i, inpData));
    public IEnumerator UseActiveAsync(int i, List<MakeMove> inpData)
    {
        //Общее
        AbstractSpell currentSpell = SpellList[i].GetComponent<AbstractSpell>();
        if (effect != null) effect.SetActive(true);
        if (currentSpell.voiceBefore.Length > 0)
        {
            int rand = Random.Range(0, 3);
            if(rand != 2) BattleVoice.voiceSource.PlayOneShot(currentSpell.voiceBefore[rand]); 
        }
        skill.SetActive(true);
        skillPic.sprite = SpellList[i].transform.Find("Mask/Pic").gameObject.GetComponent<Image>().sprite;
        if (currentSpell.soundBefore != null) BattleSound.sound.PlayOneShot(currentSpell.soundBefore);
        //Эффект
        if (currentSpell.state == "Effect")
        {
            parentObject.pathAnimation.SetCaracterState(currentSpell.Animation);
            yield return new WaitForSeconds(currentSpell.timeBeforeShoot);
            if(currentSpell.soundMid != null) BattleSound.sound.PlayOneShot(currentSpell.soundMid);
            yield return new WaitForSeconds(0.1f);
            if (useSpell != null) useSpell.SetActive(true);
            if (currentSpell.soundAfter != null) BattleSound.sound.PlayOneShot(currentSpell.soundAfter);
            GameObject newObject = Instantiate(SpellList[i], Turns.circlesMap[inpData[0].attackSend["side"], inpData[0].attackSend["place"]].newObject.pathDebuffs);
            newObject.GetComponent<AbstractSpell>().fromUnit = parentObject.GetComponentInParent<Unit>();
            yield return new WaitForSeconds(0.4f);
            Turns.hitDone = true;
        }

        //Снаряд
        else if (currentSpell.state == "Ball")
        {
            parentObject.pathAnimation.SetCaracterState(currentSpell.Animation);
            yield return new WaitForSeconds(currentSpell.timeBeforeShoot);

            int count = 0;
            while (count < currentSpell.times)
            {
                GameObject bulletTarget;
                if (Turns.circlesMap[inpData[count].attackSend["side"], inpData[count].attackSend["place"]].newObject != null)
                    bulletTarget = Turns.circlesMap[inpData[count].attackSend["side"], inpData[count].attackSend["place"]].newObject.pathBulletTarget.gameObject;
                else bulletTarget = Turns.circlesMap[inpData[count].attackSend["side"], inpData[count].attackSend["place"]].gameObject;
                //if (GetComponent<UnitProperties>().soundVoiceStrike.Length > 0) _soundManagerUnit.SoundStrike(GetComponent<UnitProperties>().soundVoiceStrike[Random.Range(0, 3)]);

                if (currentSpell.soundMid != null) BattleSound.sound.PlayOneShot(currentSpell.soundMid);
                if (currentSpell.soundAfter != null) BattleSound.sound.PlayOneShot(currentSpell.soundAfter);
                if (inpData[count].hitEffectSend.Count > 0) StartCoroutine(currentSpell.HitEffect(inpData[count].hitEffectSend));

                if (useSpell != null) useSpell.SetActive(true);
                GameObject newBullet = Instantiate(currentSpell.Effect, transform.Find("bullet").gameObject.transform.position, Quaternion.identity);
                Bullet bullet = newBullet.GetComponent<Bullet>();
                if (inpData[count].attackSend.ContainsKey("damage")) bullet.damage = inpData[count].attackSend["damage"];
                if (inpData[count].attackSend.ContainsKey("element")) bullet.element = inpData[count].attackSend["element"];
                bullet.unitFrom = parentObject;
                bullet.unitTarget = Turns.circlesMap[inpData[count].attackSend["side"], inpData[count].attackSend["place"]].newObject;
                    
                var direction = bulletTarget.transform.position - newBullet.transform.position;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                if (parentObject.times > 0) newBullet.transform.rotation = Quaternion.Euler(0, 0, angle + Random.Range(0, -6));
                else newBullet.transform.rotation = Quaternion.Euler(0, 0, angle);

                count++;
                yield return new WaitForSeconds(currentSpell.between);
            }
            PlayDoneSound(currentSpell);
            yield return new WaitForSeconds(1f);
            Turns.hitDone = true;
        }

        //Ближний
        else if (currentSpell.state == "Melee")
        {
            if (effectBefore != null) effectBefore.SetActive(true);

            parentObject.pathAnimation.SetCaracterState(currentSpell.Animation);
            yield return new WaitForSeconds(currentSpell.timeBeforeShoot);
            currentSpell.BeforeHit();
            int count = 0;
            while (count != currentSpell.times)
            {
                StartIni.soundVoice.StrikeVoices(parentObject.indexVoice);
                if (currentSpell.soundMid != null) BattleSound.sound.PlayOneShot(currentSpell.soundMid);
                currentSpell.SwishMethod(count);
                yield return new WaitForSeconds(0.1f);
                if(GetComponent<UnitProperties>().pathParent.Type == 3) StartIni.animatorShakeStatic.SetTrigger("shake");
                if (useSpell != null) useSpell.SetActive(true);
                if (inpData[count].hitEffectSend.Count > 0) StartCoroutine(currentSpell.HitEffect(inpData[count].hitEffectSend));
                if (currentSpell.soundAfter != null) BattleSound.sound.PlayOneShot(currentSpell.soundAfter);
                if (inpData[count].attackSend.ContainsKey("damage"))
                    Turns.circlesMap[inpData[count].attackSend["side"], inpData[count].attackSend["place"]].newObject.SpellDamage(inpData[count].attackSend["damage"], inpData[count].attackSend["element"]);
                count++;
                yield return new WaitForSeconds(currentSpell.between);
            }
            PlayDoneSound(currentSpell);
            Turns.hitDone = true;
        }
        //Без цели
        else if (currentSpell.state == "nonTarget")
        {
            parentObject.pathAnimation.SetCaracterState(currentSpell.Animation);
            StartCoroutine(currentSpell.HitEffect(inpData[0].hitEffectSend));
            while (!Turns.hitDone) yield return null;
            PlayDoneSound(currentSpell);
        }
    }
    public void SetState(int i)
    {
        modeIndex = i;
        if (parentObject.pathAnimation._mode != null) parentObject.pathAnimation.SetCaracterState("mode");
        if (modeList[i].GetComponent<AbstractSpell>().soundMid != null) BattleSound.sound.PlayOneShot(modeList[i].GetComponent<AbstractSpell>().soundMid);
        Instantiate(modeList[i], parentObject.pathDebuffs);
    }
    private void PlayDoneSound(AbstractSpell currentSpell)
    {
        if (currentSpell.voiceDone.Length > 0)
        {
            int rand = Random.Range(0, 3);
            if (rand != 2) BattleVoice.voiceSource.PlayOneShot(currentSpell.voiceDone[rand]);
        }
    }
}