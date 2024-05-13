using UnityEngine;

[CreateAssetMenu(menuName = "SO/Character Data")]
public class CharacterData : ScriptableObject
{
    public AudioClip[] Presenter => _presenter;
    public string[] Name => _name;
    public string[] Description => _description;
    public Sprite Portrait => _portrait;
    public Sprite CardShell => _cardShell;
    public CharacterAttributes Attributes => _attributes;
    public CharacterTransforms Transforms => Transforms;
    public AnimationAssets AnimationAssets => _animationAssets;
    public GameObject Prefub => _prefub;
    public GameObject[] Spells => _spells;
    public GameObject StartDebuff => _startDebuff;

    [SerializeField] private AudioClip[] _presenter;
    [SerializeField] private string[] _name;
    [SerializeField] private string[] _description;
    [SerializeField] private Sprite _portrait;
    [SerializeField] private Sprite _cardShell;
    [SerializeField] private GameObject _prefub;
    [SerializeField] private CharacterAttributes _attributes;
    [SerializeField] private CharacterTransforms _transforms;
    [SerializeField] private AnimationAssets _animationAssets;
    [SerializeField] private GameObject[] _spells;
    [SerializeField] private GameObject _startDebuff;
}
