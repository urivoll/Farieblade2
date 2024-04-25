using UnityEngine;

[CreateAssetMenu(menuName = "SO/Character Transforms")]
public class CharacterTransforms : ScriptableObject
{
    [SerializeField] private Vector2 _canvasHeight;
    [SerializeField] private Vector2 _bulletPosition;
}
