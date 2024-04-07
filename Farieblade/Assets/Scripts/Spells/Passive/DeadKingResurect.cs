using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeadKingResurect : AbstractSpell
{
    public float Value;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip swish;
    [SerializeField] private AudioClip soulSound;
    [SerializeField] private GameObject hpEffect;
    [SerializeField] private GameObject dmgEffect;
    [SerializeField] private GameObject soul;
    [SerializeField] private GameObject heal;
    private void Start()
    {
        Value = 0.3f + fromUnit.grade * 0.01f;
        if (PlayerData.language == 0)
        {
            nameText = "Gift of the night";
            SType = "Passive";
            description = $"If an ally is resurrected in battle, the Dead King strengthens the allies. If an ally has more than 50% health, it gives damage, if less, it gives health.\nDamage/Health: +{Convert.ToInt32(Value * 100)}%";
        }
        else
        {
            nameText = "Дар ночи";
            SType = "Пассивная";
            description = $"Если в битве воскрес союзник, Мертвый король усиляет союзников. Если у союзника больше 50% здоровья, дает урон, если меньше, то дает здоровье.\r\nУрон/Здоровье: +{Convert.ToInt32(Value * 100)}%";
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        int rand = Random.Range(0, 3);
        if (rand != 2) BattleSound.sound.PlayOneShot(voiceAfter[rand]);
        parentUnit.pathAnimation.SetCaracterState("passive");
        BattleSound.sound.PlayOneShot(soulSound);
        BattleSound.sound.PlayOneShot(swish);
        yield return new WaitForSeconds(0.3f);
        List<UnitProperties> list = new();
        for (int i = 0; i < inpData["count"]; i++)
        {
            list.Add(Turns.circlesMap[inpData["side"], inpData[$"placeOur{i}"]].newObject);
            GameObject newObject = Instantiate(soul, Turns.circlesMap[inpData["side"], inpData["victim"]].newObject.transform.position, Quaternion.identity);
            newObject.transform.Find("DeadKingSoulTarget").gameObject.transform.position = Turns.circlesMap[inpData["side"], inpData[$"placeOur{i}"]].newObject.pathBulletTarget.position;
        }
        yield return new WaitForSeconds(0.9f);
        BattleSound.sound.PlayOneShot(swish);
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < inpData["count"]; i++)
        {
            UnitProperties unit = list[i];
            if (inpData[$"mode{i}"] == 1)
            {
                unit.damage = inpData[$"damage{i}"];
                unit.HpDamage("dmg");
            }
            else
            {
                if (inpData[$"catch{i}"] == 1)
                {
                    unit.hp = inpData[$"hp{i}"];
                    unit.HpDamage("hp");
                    Instantiate(heal, unit.pathBulletTarget.position, Quaternion.identity);
                }
            }
            Instantiate(hpEffect, list[i].pathBulletTarget.position, Quaternion.identity);
        }
        parentUnit.transform.Find("UseSpell").gameObject.SetActive(true);
        BattleSound.sound.PlayOneShot(clip);
        yield return new WaitForSeconds(0.5f);
        Turns.finishEndEvent = true;
    }
}
