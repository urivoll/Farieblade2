using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
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
    [Inject] private CharacterPlacement _characterPlacement;

    public void Init(CharacterData characterData)
    {
        SpellList.AddRange(characterData.Spells);
        parentObject = GetComponent<UnitProperties>();
        skill = transform.Find("UI/Skill").gameObject;
        skillPic = skill.transform.Find("Mask/Pic").GetComponent<Image>();
        modeIndex = (modeList.Count > 0) ? modeIndex = 0 : -1;
    }

    public void UseActive(int i, List<MakeMove> inpData) => StartCoroutine(UseActiveAsync(i, inpData));

    public IEnumerator UseActiveAsync(int i, List<MakeMove> inpData)
    {
        //�����
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
        //������
        if (currentSpell.state == "Effect")
        {
            parentObject.Animation.TryGetAnimation(currentSpell.Animation);
            yield return new WaitForSeconds(currentSpell.timeBeforeShoot);
            if(currentSpell.soundMid != null) BattleSound.sound.PlayOneShot(currentSpell.soundMid);
            yield return new WaitForSeconds(0.1f);
            if (useSpell != null) useSpell.SetActive(true);
            if (currentSpell.soundAfter != null) BattleSound.sound.PlayOneShot(currentSpell.soundAfter);
            GameObject newObject = Instantiate(SpellList[i], _characterPlacement.CirclesMap[inpData[0].attackSend["side"], inpData[0].attackSend["place"]].ChildCharacter.PathDebuffs);
            newObject.GetComponent<AbstractSpell>().fromUnit = parentObject.GetComponentInParent<Unit>();
            yield return new WaitForSeconds(0.4f);
            Turns.hitDone = true;
        }

        //������
        else if (currentSpell.state == "Ball")
        {
            parentObject.Animation.TryGetAnimation(currentSpell.Animation);
            yield return new WaitForSeconds(currentSpell.timeBeforeShoot);

            int count = 0;
            while (count < currentSpell.times)
            {
                GameObject bulletTarget;
                if (_characterPlacement.CirclesMap[inpData[count].attackSend["side"], inpData[count].attackSend["place"]].ChildCharacter != null)
                    bulletTarget = _characterPlacement.CirclesMap[inpData[count].attackSend["side"], inpData[count].attackSend["place"]].ChildCharacter.PathBulletTarget.gameObject;
                else bulletTarget = _characterPlacement.CirclesMap[inpData[count].attackSend["side"], inpData[count].attackSend["place"]].gameObject;
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
                bullet.unitTarget = _characterPlacement.CirclesMap[inpData[count].attackSend["side"], inpData[count].attackSend["place"]].ChildCharacter;
                    
                var direction = bulletTarget.transform.position - newBullet.transform.position;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                if (parentObject.Weapon.Times > 0) newBullet.transform.rotation = Quaternion.Euler(0, 0, angle + Random.Range(0, -6));
                else newBullet.transform.rotation = Quaternion.Euler(0, 0, angle);

                count++;
                yield return new WaitForSeconds(currentSpell.between);
            }
            PlayDoneSound(currentSpell);
            yield return new WaitForSeconds(1f);
            Turns.hitDone = true;
        }

        //�������
        else if (currentSpell.state == "Melee")
        {
            if (effectBefore != null) effectBefore.SetActive(true);

            parentObject.Animation.TryGetAnimation(currentSpell.Animation);
            yield return new WaitForSeconds(currentSpell.timeBeforeShoot);
            currentSpell.BeforeHit();
            int count = 0;
            while (count != currentSpell.times)
            {
                StartIni.soundVoice.StrikeVoices(parentObject.indexVoice);
                if (currentSpell.soundMid != null) BattleSound.sound.PlayOneShot(currentSpell.soundMid);
                currentSpell.SwishMethod(count);
                yield return new WaitForSeconds(0.1f);
                if (useSpell != null) useSpell.SetActive(true);
                if (inpData[count].hitEffectSend.Count > 0) StartCoroutine(currentSpell.HitEffect(inpData[count].hitEffectSend));
                if (currentSpell.soundAfter != null) BattleSound.sound.PlayOneShot(currentSpell.soundAfter);
                if (inpData[count].attackSend.ContainsKey("damage"))
                    _characterPlacement.CirclesMap[inpData[count].attackSend["side"], inpData[count].attackSend["place"]].ChildCharacter.HpCharacter.SpellDamage(inpData[count].attackSend["damage"], inpData[count].attackSend["element"]);
                count++;
                yield return new WaitForSeconds(currentSpell.between);
            }
            PlayDoneSound(currentSpell);
            Turns.hitDone = true;
        }
        //��� ����
        else if (currentSpell.state == "nonTarget")
        {
            parentObject.Animation.TryGetAnimation(currentSpell.Animation);
            StartCoroutine(currentSpell.HitEffect(inpData[0].hitEffectSend));
            while (!Turns.hitDone) yield return null;
            PlayDoneSound(currentSpell);
        }
    }
    public void SetState(int i)
    {
        modeIndex = i;
        parentObject.Animation.TryGetAnimation("mode");
        if (modeList[i].GetComponent<AbstractSpell>().soundMid != null) BattleSound.sound.PlayOneShot(modeList[i].GetComponent<AbstractSpell>().soundMid);
        Instantiate(modeList[i], parentObject.PathDebuffs);
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