using UnityEngine;

[CreateAssetMenu(menuName = "SO/Character Data")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private CharacterPrefabReferences _prefubs;
    [SerializeField] private CharacterAttributes _attributes;
    [SerializeField] private CharacterTransforms _transforms;
}
