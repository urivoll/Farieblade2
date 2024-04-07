using UnityEngine;
using UnityEngine.EventSystems;

public class ModelPanelScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject parentCircle;

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(gameObject);
        parentCircle.GetComponent<UIDropHandler>().newObject = null;
    }
}
