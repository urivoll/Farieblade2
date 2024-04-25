using UnityEngine;

[CreateAssetMenu(menuName = "SO/Character Prefub Reserences")]
public class CharacterPrefabReferences : ScriptableObject
{
    public GameObject CombatCharacterPrefab => _�ombatCharacterPrefab;
    public GameObject CardPrefub => _cardPrefub;
    public GameObject Model => _model;

    [SerializeField] private GameObject _�ombatCharacterPrefab;
    [SerializeField] private GameObject _cardPrefub;
    [SerializeField] private GameObject _model;
}
