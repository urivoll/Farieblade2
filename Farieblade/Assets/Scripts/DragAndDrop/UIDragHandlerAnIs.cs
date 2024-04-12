using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragHandlerAnIs : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform tempMovePlace;
    private RectTransform _rectTransform;
    private Canvas _canvas;
    [HideInInspector] public GameObject parentCircle = null;
    [HideInInspector] public UnityEngine.Transform _previousParent;
    private GameObject obj;
    private AudioSource audioSource;
    private MultiplayerSorting sorting;
    private AnIsCollection anIsCollection;

    private void Start()
    {
        if (GameObject.Find("Right0") == null)
        {
            anIsCollection = GetComponentInParent<AnIsCollection>();
            sorting = GetComponentInParent<MultiplayerSorting>();
            audioSource = gameObject.GetComponent<AudioSource>();
            obj = gameObject.transform.parent.gameObject;
            _rectTransform = gameObject.transform.parent.GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
            tempMovePlace = transform.parent.parent.parent.parent.parent;
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
        if (parentCircle != null)
        {
            AnIsCollection.PowerAnIs -= Convert.ToInt32(obj.GetComponent<Unit>().Power);
            anIsCollection.SetPower();
            parentCircle.GetComponent<UIDropHandlerAnIs>().newObject = null;
            parentCircle = null;
        }
        obj.transform.localScale = new Vector2(obj.transform.localScale[0] + 0.5f, obj.transform.localScale[1] + 0.5f);
        _previousParent = obj.transform.parent;
        PanelPropertisMainMenu.cardRayCastOff?.Invoke();
        obj.transform.SetParent(tempMovePlace);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out UIDropHandlerAnIs container);
            if (container == null)
            {
                if (_previousParent.gameObject.tag == "Our")
                {
                    _previousParent.gameObject.GetComponent<UIDropHandlerAnIs>().StartMoveUnit(obj);
                    AnIsCollection.PowerAnIs += Convert.ToInt32(obj.GetComponent<Unit>().Power);
                    anIsCollection.SetPower();
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
    private void RayCastOffInside() => GetComponent<CanvasGroup>().blocksRaycasts = false;
    private void RayCastOnInside() => GetComponent<CanvasGroup>().blocksRaycasts = true;
}
