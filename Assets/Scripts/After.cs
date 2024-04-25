using System.Collections;
using UnityEngine;

public class After : MonoBehaviour
{
    public void AfterAn(GameObject obj)
    {
        StartCoroutine(AfterAnim(obj));
    }
    public void AfterAnLong(GameObject obj)
    {
        StartCoroutine(AfterAnimLong(obj));
    }
    public void AfterAnUnitUI(GameObject obj)
    {
        StartCoroutine(AfterAnimUnitUI(obj));
    }
    public IEnumerator AfterAnimUnitUI(GameObject obj)
    {
        yield return new WaitForSeconds(0.2f);
        obj.SetActive(false);
    }
    public IEnumerator AfterAnim(GameObject obj)
    {
        yield return new WaitForSeconds(0.4f);
        obj.SetActive(false);
    }
    public IEnumerator AfterAnimLong(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        obj.SetActive(false);
    }
}
