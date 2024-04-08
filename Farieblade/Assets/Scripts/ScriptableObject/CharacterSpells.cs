using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Character Spells")]
public class CharacterSpells : ScriptableObject
{
    [SerializeField] private List<GameObject> _spells;
}
