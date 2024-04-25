using Spine.Unity;
using UnityEngine;
using Zenject;
public class CircleProperties : MonoBehaviour
{
    public int intState;
    [HideInInspector] public CircleAnimation PathAnimation;
    [HideInInspector] public float objectReward = 0;
    public int Side => _side;
    public int Place => _place;
    public UnitProperties ChildCharacter => _childCharacter;

    private UnitProperties _childCharacter = null;
    [SerializeField] private int _side;
    [SerializeField] private int _place;
    [SerializeField] private float[] vector;
    [SerializeField] private float[] vector2;
    [SerializeField] private GameObject _character;

    [Inject] private DiContainer _diContainer;
    [Inject] private CharacterPlacement _characterPlacement;

    public void Init()
    {
        StartIni.setViewTrops += SetView;
        PathAnimation = GetComponent<CircleAnimation>();
        CreateUnit();
    }

    public void TryDeleteChild()
    {
        if (_childCharacter == null) return;
        _childCharacter = null;
    }

    public void CreateUnit()
    {
        int unitID = MultiplayerDraft.units[_side, _place];
        if (unitID == -666) return;

        GameObject tempNewObject;
        tempNewObject = _diContainer.InstantiatePrefab(_character, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        
        tempNewObject.transform.localScale = new Vector2(1, 1);
        _childCharacter = tempNewObject.GetComponent<UnitProperties>();
        if(_characterPlacement != null)
        _characterPlacement.AddCharacter(_side, _childCharacter);
        _childCharacter.ParentCircle = this;
        _childCharacter.Init(
            unitID, 
            _side, 
            GetComponent<SkeletonPartsRenderer>().MeshRenderer.sortingOrder);
    }
    
    private void SetView()
    {
        if (PlayerData.combatView == 1) 
            transform.position = new Vector2(vector[0], vector[1]);
        else if (PlayerData.combatView == 2) 
            transform.position = new Vector2(vector2[0], vector2[1]);
    }

    private void OnDestroy() => StartIni.setViewTrops -= SetView;
}
