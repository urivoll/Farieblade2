using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIDropHandler : MonoBehaviour, IDropHandler
{
    [HideInInspector] public GameObject newObject = null;
    [HideInInspector] public GameObject cloneObject;
    [SerializeField] private int place;
    [SerializeField] private int _class;
    private Sorting sorting;
    private MyCollection myCollection;
    private void Start()
    {
        sorting = GetComponentInParent<Sorting>();
        myCollection = GetComponentInParent<MyCollection>();
    }
    public void Arrangement()
    {
        if (_class == 1 && PlayerData.troop[place] != -666) StartMoveUnit(PlayerData.myCollection[PlayerData.troop[place]]);
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.name == "Card")
        {
            if (_class == 1)
            {
                int state = eventData.pointerDrag.transform.parent.GetComponent<Unit>().state;

                if (eventData.pointerDrag.transform.parent.GetComponent<Unit>().level != 0 && 
                    newObject == null && 
                    (((place == 1 || place == 3 || place == 5) && (state == 1 || state == 3 || state == 4)) ||
                    ((place == 0 || place == 2 || place == 4) && state == 0)))
                {
                    StartMoveUnit(eventData.pointerDrag.transform.parent.gameObject);
                    myCollection.SetAmount();
                    PlayerData.totalPower += Convert.ToInt32(eventData.pointerDrag.transform.parent.GetComponent<Unit>().Power);
                    myCollection.SetPower();
                }
                //Возвращение в предыдущий слот
                else
                {
                    //Если прошлый слот был серклом
                    if (eventData.pointerDrag.GetComponent<UIDragHandler>()._previousParent.gameObject.tag == "circleMelee" ||
                        eventData.pointerDrag.GetComponent<UIDragHandler>()._previousParent.gameObject.tag == "circleShooters")
                    {
                        eventData.pointerDrag.GetComponent<UIDragHandler>()._previousParent.gameObject.GetComponent<UIDropHandler>().StartMoveUnit(eventData.pointerDrag.transform.parent.gameObject);

                        myCollection.SetAmount();
                        PlayerData.totalPower += Convert.ToInt32(eventData.pointerDrag.transform.parent.GetComponent<Unit>().Power);
                        myCollection.SetPower();
                    }
                    //Если прошлый слот был коллекцией
                    else
                    {
                        eventData.pointerDrag.transform.parent.gameObject.transform.SetParent(eventData.pointerDrag.GetComponent<UIDragHandler>()._previousParent);
                        sorting.Sort();
                    }
                }
            }
            else
            {
                StartMoveUnit(eventData.pointerDrag.transform.parent.gameObject);
                sorting.Sort();
            }
        }
    }
    public void StartMoveUnit(GameObject obj)
    {
        if (_class == 1)
        {
            PlayerData.troopAmount += 1;
            newObject = obj;
            newObject.transform.Find("Card").gameObject.GetComponent<UIDragHandler>().parentCircle = gameObject;
            obj.transform.SetParent(gameObject.transform);
        }
        else obj.transform.SetParent(gameObject.transform.Find("Viewport/Content").gameObject.transform);


    }
/*    public void CreateGiant(GameObject obj)
    {
        cloneObject = Instantiate(Camera.main.GetComponent<PanelPropertisMainMenu>().Clone, _2_1.transform);
        cloneObject.transform.Find("Card/IconMask/Icon").gameObject.GetComponent<Image>().sprite = obj.transform.Find("Card/IconMask/Icon").gameObject.GetComponent<Image>().sprite;
    }*/
}