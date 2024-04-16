using Spine.Unity;
using UnityEngine;
using Zenject;
public class CircleProperties : MonoBehaviour
{
    public int intState;
    [HideInInspector] public CircleAnimation PathAnimation;
    [HideInInspector] public int unitID = -666;
    [HideInInspector] public UnitProperties newObject = null;
    [HideInInspector] public int levelObject = 0;
    [HideInInspector] public int gradeObject = 0;
    [HideInInspector] public int objectGoldReward = 0;
    [HideInInspector] public float objectReward = 0;

    [SerializeField] private int _side;
    [SerializeField] private int _place;
    [SerializeField] private float[] vector;
    [SerializeField] private float[] vector2;

    private CharacterPlacement _characterPlacement;

    [Inject]
    private void Construct(CharacterPlacement characterPlacement)
    {
        _characterPlacement = characterPlacement;
    }
    public void InitializationCircle()
    {
        StartIni.setViewTrops += SetView;
        PathAnimation = GetComponent<CircleAnimation>();
        SetUnitProperties();
    }

    public void SetUnitProperties()
    {
        unitID = MultiplayerDraft.units[_side, _place];
        levelObject = MultiplayerDraft.unitsLevel[_side, _place];
        gradeObject = MultiplayerDraft.unitsGrade[_side, _place];
        if (unitID == -666) return;
        CreateUnit(PlayerData.defaultCards[unitID]);
        newObject.pathParent.level = levelObject;
        newObject.pathParent.grade = gradeObject;
        newObject.pathParent.SetValues();
        newObject.Instantiate();
        objectReward = newObject.pathParent.expReward;
        objectGoldReward = newObject.pathParent.goldReward;
    }
    public UnitProperties CreateUnit(GameObject createUnit)
    {
        GameObject tempNewObject;
        tempNewObject = Instantiate(createUnit, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        newObject = tempNewObject.GetComponentInChildren<UnitProperties>();
        if(_characterPlacement != null)
        _characterPlacement.Init(_side, newObject);

        newObject.pathCircle = this;
        newObject.Init(_side, _place);
        if (_side == 0) intState = -1; else intState = 1;
        newObject.transform.localScale = new Vector2(0.55f * intState, 0.55f);
        int tempSorting = GetComponent<SkeletonPartsRenderer>().MeshRenderer.sortingOrder;
        newObject.GetComponent<SkeletonPartsRenderer>().MeshRenderer.sortingOrder = tempSorting;
        newObject.transform.parent.transform.Find("Canvas").gameObject.GetComponent<Canvas>().sortingOrder = tempSorting;
        return newObject;
    }
    private void SetView()
    {
        if (PlayerData.combatView == 1) transform.position = new Vector2(vector[0], vector[1]);
        else if (PlayerData.combatView == 2) transform.position = new Vector2(vector2[0], vector2[1]);
    }
    private void OnDestroy() => StartIni.setViewTrops -= SetView;
}
