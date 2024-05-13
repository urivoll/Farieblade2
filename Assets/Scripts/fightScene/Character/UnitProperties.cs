using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TargetSelector))]
[RequireComponent(typeof(HpCharacter))]
[RequireComponent(typeof(UnitAnimation))]
[RequireComponent(typeof(CharacterInfoPanelOpener))]
[RequireComponent(typeof(Spells))]
[RequireComponent(typeof(CharacterState))]

public class UnitProperties : MonoBehaviour
{
    public int Id;
    public int initiative;
    public int Level;
    public int Grade;
    public int Type;
    public int indexVoice;
    public int Energy—onsumption;
    public HpCharacter HpCharacter;
    public EnergyUnit Energy;
    public Spells Spells;
    public CircleProperties ParentCircle;
    public UnitAnimation Animation;
    public UnitCanvas UI;
    public Weapon Weapon;
    public CharacterState CharacterState;
    public Aura Aura;
    public List<GameObject> DebuffList = new();
    public Transform PathDebuffs;
    public Transform PathBulletTarget;

    [SerializeField] private IDCaracter _idCharacter;
    private GameObject _prefub;
    [Inject] private DiContainer _diContainer;

    public void Init(int id, int side, int sortingOrder)
    {
        Id = id;

        UI = transform.Find("UI").GetComponent<UnitCanvas>();
        Energy = GetComponentInChildren<EnergyUnit>();
        
        CharacterData characterData = _idCharacter.CharacterData[id];

        CharacterState = GetComponent<CharacterState>();
        CharacterState.Init(this);
        Spells = GetComponent<Spells>();
        Spells.Init(characterData);

        GameObject passive = _diContainer.InstantiatePrefab(characterData.StartDebuff, PathDebuffs);
        DebuffList.Add(passive);

        Weapon = (characterData.Attributes.Type.Id == 0) ? _diContainer.InstantiateComponent<Melee>(gameObject) : _diContainer.InstantiateComponent<Shooter>(gameObject);
        Weapon.Init(this, characterData.Attributes);
        HpCharacter = GetComponent<HpCharacter>();
        HpCharacter.Init(this, characterData.Attributes);

        initiative = characterData.Attributes.Initiative;
        Type = characterData.Attributes.Type.Id;
        _prefub = Instantiate(
            characterData.Prefub, 
            ParentCircle.transform.position, 
            Quaternion.identity,
            transform);
        _prefub.GetComponent<VisualCharacter>().Init(side, sortingOrder);
        Animation = GetComponent<UnitAnimation>();
        Animation.Init(characterData.AnimationAssets, _prefub);

        PathBulletTarget = _prefub.transform.Find("BulletTarget");
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