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
    [HideInInspector] public int Type;
    [HideInInspector] public int indexVoice;
    [HideInInspector] public int Energy—onsumption;
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
        CharacterState = new(this);
        CharacterData characterData = _idCharacter.CharacterData[id];
        HpCharacter = new(this, characterData.Attributes);

        initiative = characterData.Attributes.Initiative;
        Type = characterData.Attributes.Type.Id;
        _prefub = Instantiate(
            characterData.Prefub, 
            ParentCircle.transform.position, 
            Quaternion.identity,
            transform);
        _prefub.GetComponent<VisualCharacter>().Init(side, sortingOrder);
        Animation = _prefub.GetComponent<UnitAnimation>();
        PathBulletTarget = transform.Find("BulletTarget");
        PathDebuffs = transform.parent.Find("UI/Debuffs");
        Spells.SpellList.AddRange(characterData.Spells);
        gameObject.name = characterData.Name[PlayerData.language];
    }

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