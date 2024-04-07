using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }
}
