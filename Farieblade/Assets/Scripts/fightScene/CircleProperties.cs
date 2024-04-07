using Spine.Unity;
using UnityEngine;
public class CircleProperties : MonoBehaviour
{
    public int intState;
    [HideInInspector] public CircleAnimation PathAnimation;
    [HideInInspector] public int unitID = -666;
    public int sideOnMap;
    public int placeOnMap;
    [HideInInspector] public UnitProperties newObject = null;
    [HideInInspector] public int levelObject = 0;
    [HideInInspector] public int gradeObject = 0;
    [HideInInspector] public int objectGoldReward = 0;
    [HideInInspector] public float objectReward = 0;
    [SerializeField] private float[] vector;
    [SerializeField] private float[] vector2;
    public void InitializationCircle()
    {
        StartIni.setViewTrops += SetView;
        PathAnimation = GetComponent<CircleAnimation>();
        SetUnitProperties();
    }
    public void SetUnitProperties()
    {
        unitID = MultiplayerDraft.units[sideOnMap, placeOnMap];
        levelObject = MultiplayerDraft.unitsLevel[sideOnMap, placeOnMap];
        gradeObject = MultiplayerDraft.unitsGrade[sideOnMap, placeOnMap];
        if (unitID == -666) return;
        CreateUnit(PlayerData.defaultCards[unitID]);
        newObject.pathParent.level = levelObject;
        newObject.pathParent.grade = gradeObject;
        newObject.pathParent.SetValues();
        newObject.Instantiate();
        objectReward = newObject.pathParent.expReward;
        objectGoldReward = newObject.pathParent.goldReward;
    }
    public UnitProperties CreateUnit(GameObject createUnit2)
    {
        GameObject tempNewObject;
        //Создание и назначение его круга
        tempNewObject = Instantiate(createUnit2, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        newObject = tempNewObject.GetComponentInChildren<UnitProperties>();
        if (sideOnMap == 1)
        {
            Turns.listUnitRight.Add(newObject);
            Turns.listFinishRight.Add(this);
        }
        else Turns.listUnitLeft.Add(newObject);
        Turns.listUnitAll.Add(newObject);
        newObject.pathCircle = this;
        newObject.sideOnMap = sideOnMap;
        newObject.placeOnMap = placeOnMap;
        //дальность прорисовки и поворот
        if (sideOnMap == 0) intState = -1; else intState = 1;
        //Слои
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
