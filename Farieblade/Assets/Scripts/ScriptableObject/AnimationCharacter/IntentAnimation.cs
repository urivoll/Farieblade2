using Spine.Unity;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/IntentAnimation")]
public class IntentAnimation : ScriptableObject
{
    [SerializeField] private AnimationReferenceAsset _animation;
    [SerializeField] private string _name;
    [SerializeField] private float _activeTime;
}
