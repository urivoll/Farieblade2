using UnityEngine;

[CreateAssetMenu(menuName = "SO/Character Prefub Reserences")]
public class CharacterPrefabReferences : ScriptableObject
{
    public GameObject CombatCharacterPrefab => _ñombatCharacterPrefab;
    public GameObject CardPrefub => _cardPrefub;
    public GameObject Model => _model;

    [SerializeField] private GameObject _ñombatCharacterPrefab;
    [SerializeField] private GameObject _cardPrefub;
    [SerializeField] private GameObject _model;
}
