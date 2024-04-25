using System.Collections;
using UnityEngine;

public class ActiveEffect : MonoBehaviour
{
    [SerializeField] private float time;
    private void OnEnable()
    {
        if (time == 0) StartCoroutine(Hide(2));
        else StartCoroutine(Hide(time));
    }

    private IEnumerator Hide(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}