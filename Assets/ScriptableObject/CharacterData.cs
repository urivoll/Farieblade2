using UnityEngine;

[CreateAssetMenu(menuName = "SO/Character Data")]
public class CharacterData : ScriptableObject
{
    public CharacterPrefabReferences Prefubs => _prefubs;
    public CharacterAttributes Attributes => _attributes;
    public CharacterTransforms Transforms => Transforms;
    public AnimationAssets AnimationAssets => _animationAssets;
    public GameObject[] Spells => _spells;

    [SerializeField] private CharacterPrefabReferences _prefubs;
    [SerializeField] private CharacterAttributes _attributes;
    [SerializeField] private CharacterTransforms _transforms;
    [SerializeField] private AnimationAssets _animationAssets;
    [SerializeField] private GameObject[] _spells;
}
