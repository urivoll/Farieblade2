using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragHandlerMultiplayer : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform tempMovePlace;
    private RectTransform _rectTransform;
    private Canvas _canvas;
    [HideInInspector] public UnityEngine.Transform _previousParent;
    private GameObject obj;
    private MultiplayerSorting sorting;
    private GameObject block;
    private MultiplayerDraft draft;

    private void OnEnable()
    {
        StartIni.cardRayCastOff += RayCastOffInside;
        StartIni.cardRayCastOn += RayCastOnInside;
        obj = gameObject.transform.parent.gameObject;
        _rectTransform = transform.parent.GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        tempMovePlace = obj.transform.parent.parent.parent.parent.parent.parent.Find("topPanel");
        //print(tempMovePlace.gameObject.name);
        sorting = GetComponentInParent<MultiplayerSorting>();
        block = GetComponentInParent<MultiplayerDraft>().blockCircle[BattleNetwork.sideOnBattle];
        draft = GetComponentInParent<MultiplayerDraft>();
    }
    private void OnDisable()
    {
        StartIni.cardRayCastOff -= RayCastOffInside;
        StartIni.cardRayCastOn -= RayCastOnInside;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        obj.transform.localScale = new Vector2(obj.transform.localScale[0] + 0.5f, obj.transform.localScale[1] + 0.5f);
        _previousParent = obj.transform.parent;
        StartIni.cardRayCastOff?.Invoke();
        obj.transform.SetParent(tempMovePlace);
        draft.ShineSlot(obj.GetComponent<Unit>().state);
        block.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draft.HideSlot();
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out UIDropHandlerMultiplayer container);
            if (container == null)
            {
                obj.transform.SetParent(_previousParent);
                sorting.Sort();
            }
        }
        else
        {
            obj.transform.SetParent(_previousParent);
            sorting.Sort();
        }
        obj.transform.localScale = new Vector2(obj.transform.localScale[0] - 0.5f, obj.transform.localScale[1] - 0.5f);
        StartIni.cardRayCastOn?.Invoke();
        block.SetActive(true);
    }
    private void RayCastOffInside() => GetComponent<CanvasGroup>().blocksRaycasts = false;
    private void RayCastOnInside() => GetComponent<CanvasGroup>().blocksRaycasts = true;
}
