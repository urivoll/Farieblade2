using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDropHandlerAnIs : MonoBehaviour, IDropHandler
{
    [HideInInspector] public GameObject newObject = null;
    [SerializeField] private int _class;
    private MultiplayerSorting sorting;
    private AnIsCollection anIsCollection;
    private void Start()
    {
        anIsCollection = GetComponentInParent<AnIsCollection>();
        sorting = GetComponentInParent<MultiplayerSorting>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.name == "Card")
        {
            if (_class == 1)
            {
                if (newObject == null)
                {
                    StartMoveUnit(eventData.pointerDrag.transform.parent.gameObject);
                    AnIsCollection.PowerAnIs += Convert.ToInt32(eventData.pointerDrag.transform.parent.GetComponent<Unit>().Power);
                    anIsCollection.SetPower();
                }
                //Возвращение в предыдущий слот
                else
                {
                    //Если прошлый слот был серклом
                    if (eventData.pointerDrag.GetComponent<UIDragHandlerAnIs>()._previousParent.gameObject.tag == "Our")
                    {
                        eventData.pointerDrag.GetComponent<UIDragHandlerAnIs>()._previousParent.gameObject.GetComponent<UIDropHandler>().StartMoveUnit(eventData.pointerDrag.transform.parent.gameObject);
                        AnIsCollection.PowerAnIs += Convert.ToInt32(eventData.pointerDrag.transform.parent.GetComponent<Unit>().Power);
                        anIsCollection.SetPower();
                    }

                    //Если прошлый слот был коллекцией
                    else
                    {
                        eventData.pointerDrag.transform.parent.gameObject.transform.SetParent(eventData.pointerDrag.GetComponent<UIDragHandlerAnIs>()._previousParent);
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
            newObject = obj;
            newObject.transform.Find("Card").gameObject.GetComponent<UIDragHandlerAnIs>().parentCircle = gameObject;
            obj.transform.SetParent(gameObject.transform);
        }
        else
            obj.transform.SetParent(gameObject.transform.Find("Viewport/Content").gameObject.transform);
    }
}