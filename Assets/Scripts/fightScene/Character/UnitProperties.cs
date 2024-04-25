using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetSelector))]
[RequireComponent(typeof(HpCharacter))]
[RequireComponent(typeof(UnitAnimation))]
[RequireComponent(typeof(CharacterInfoPanelOpener))]
[RequireComponent(typeof(Spells))]
public class UnitProperties : MonoBehaviour
{
    [HideInInspector] public int Id;
    [HideInInspector] public int initiative;
    [HideInInspector] public int Level;
    [HideInInspector] public int Grade;
    [HideInInspector] public int State;
    [HideInInspector] public int indexVoice;
    [HideInInspector] public HpCharacter HpCharacter;
    [HideInInspector] public EnergyUnit Energy;
    [HideInInspector] public Spells Spells;
    [HideInInspector] public CircleProperties ParentCircle;
    [HideInInspector] public UnitAnimation Animation;
    [HideInInspector] public Weapon Weapon;
    [HideInInspector] public CharacterState CharacterState;
    [HideInInspector] public Aura Aura;
    [HideInInspector] public List<GameObject> DebuffList = new();
    [HideInInspector] public Transform PathDebuffs;
    [HideInInspector] public Transform PathBulletTarget;

    [SerializeField] private IDCaracter _idCharacter;
    private GameObject _prefub;

    public void Init(int id, int side, int sortingOrder)
    {
        Id = id;
        Spells = GetComponent<Spells>();
        Weapon = GetComponent<Weapon>();
        Energy = GetComponentInChildren<EnergyUnit>();
        Animation = GetComponent<UnitAnimation>();
        CharacterState = new(this);
        CharacterData characterData = _idCharacter.CharacterData[id];
        HpCharacter = new(this, characterData.Attributes);

        initiative = characterData.Attributes.Initiative;
        State = characterData.Attributes.State;
        _prefub = Instantiate(
            characterData.Prefubs.CombatCharacterPrefab, 
            ParentCircle.transform.position, 
            Quaternion.identity,
            transform);
        _prefub.GetComponent<VisualCharacter>().Init(side, sortingOrder);
        PathBulletTarget = transform.Find("BulletTarget");
        PathDebuffs = transform.parent.Find("UI/Debuffs");
        Spells.SpellList.AddRange(characterData.Spells);
        //SetValues();
    }

    /*    public void SetValues()
        {
            int baseRang = 0;
            float tempHp = hpBaseDefault;
            float tempDamage = damageDefault;
            tempHp += hpBaseDefault * (0.2f * level);
            tempDamage += damageDefault * (0.1f * level);
            hpBase = Convert.ToInt32(tempHp + (tempHp * (0.4f * grade)));
            damage = Convert.ToInt32(tempDamage + (tempDamage * (0.4f * grade)));
            accuracy = accuracyDefault + grade;
            CountExp();
            if (rang == 0) baseRang = 75;
            else if (rang == 1) baseRang = 100;
            else if (rang == 2) baseRang = 125;
            else baseRang = 150;

            Power = baseRang * ((level - 1 / 2 + 1) * (1 + ((grade + 1) * 0.5f)));
            if (Type == 1) Power *= 2f;
        }*/

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    public IEnumerator SoundDie()
    {
        yield return new WaitForSeconds(0.6f);
        BattleSound.sound.PlayOneShot(BattleSound.soundClip[2]);
    }
}