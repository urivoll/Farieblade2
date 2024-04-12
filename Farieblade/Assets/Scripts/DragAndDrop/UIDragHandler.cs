using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform tempMovePlace;
    private RectTransform _rectTransform;
    private Canvas _canvas;
    [HideInInspector] public GameObject parentCircle = null;
    [HideInInspector] public UnityEngine.Transform _previousParent;
    private GameObject obj;
    private AudioSource audioSource;
    private Sorting sorting;
    private MyCollection myCollection;

    private void Start()
    {
        if (GameObject.Find("Right0") == null)
        {
            sorting = GetComponentInParent<Sorting>();
            myCollection = GetComponentInParent<MyCollection>();
            audioSource = gameObject.GetComponent<AudioSource>();
            obj = gameObject.transform.parent.gameObject;
            _rectTransform = gameObject.transform.parent.GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
            tempMovePlace = transform.parent.parent.parent.parent;
        }
    }
    private void OnEnable()
    {
        PanelPropertisMainMenu.cardRayCastOff += RayCastOffInside;
        PanelPropertisMainMenu.cardRayCastOn += RayCastOnInside;
    }
    private void OnDisable()
    {
        PanelPropertisMainMenu.cardRayCastOff -= RayCastOffInside;
        PanelPropertisMainMenu.cardRayCastOn -= RayCastOnInside;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameObject.transform.parent.GetComponent<Unit>().level != 0)
        {
            if (parentCircle != null)
            {
                if (parentCircle.GetComponent<UIDropHandler>().newObject.GetComponent<Unit>().Type == 3)
                {
                    PlayerData.troopAmount -= 2;
                    Destroy(parentCircle.GetComponent<UIDropHandler>().cloneObject);
                }
                else PlayerData.troopAmount -= 1;
                PlayerData.totalPower -= Convert.ToInt32(obj.GetComponent<Unit>().Power);
                myCollection.SetAmount();
                myCollection.SetPower();

                parentCircle.GetComponent<UIDropHandler>().cloneObject = null;
                parentCircle.GetComponent<UIDropHandler>().newObject = null;
                parentCircle = null;
            }
            obj.transform.localScale = new Vector2(obj.transform.localScale[0] + 0.5f, obj.transform.localScale[1] + 0.5f);
            _previousParent = obj.transform.parent;
            PanelPropertisMainMenu.cardRayCastOff?.Invoke();
            obj.transform.SetParent(tempMovePlace);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (gameObject.transform.parent.GetComponent<Unit>().level != 0)
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (gameObject.transform.parent.GetComponent<Unit>().level != 0)
        {
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out UIDropHandler container);
                if (container == null)
                {
                    if (_previousParent.gameObject.tag == "circleMelee" ||
                        _previousParent.gameObject.tag == "circleShooters")
                    {
                        _previousParent.gameObject.GetComponent<UIDropHandler>().StartMoveUnit(obj);
                        myCollection.SetAmount();
                        PlayerData.totalPower += Convert.ToInt32(obj.GetComponent<Unit>().Power);
                        myCollection.SetPower();
                    }

                    else
                    {
                        obj.transform.SetParent(_previousParent);
                        if (_previousParent.name == "Content")
                        {
                            sorting.Sort();
                        }
                    }
                }
            }
            else
            {
                obj.transform.SetParent(_previousParent);
                if (_previousParent.name == "Content")
                {
                    sorting.Sort();
                }
            }
            obj.transform.localScale = new Vector2(obj.transform.localScale[0] - 0.5f, obj.transform.localScale[1] - 0.5f);
            audioSource.Play();
            PanelPropertisMainMenu.cardRayCastOn?.Invoke();
        }
    }
    private void RayCastOffInside() => GetComponent<CanvasGroup>().blocksRaycasts = false;
    private void RayCastOnInside() => GetComponent<CanvasGroup>().blocksRaycasts = true;
}
