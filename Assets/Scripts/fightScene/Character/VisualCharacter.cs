using Spine.Unity;
using UnityEngine;

public class VisualCharacter : MonoBehaviour
{
    public void Init(int side, int sortingOrder)
    {
        int intState = (side == 0) ? -1 : 1;
        transform.localScale = new Vector2(0.55f * intState, 0.55f);
        GetComponent<SkeletonPartsRenderer>().MeshRenderer.sortingOrder = sortingOrder;
    }
}
