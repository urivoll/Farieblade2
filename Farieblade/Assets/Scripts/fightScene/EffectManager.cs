using System.Collections;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private GameObject _resist;
    [SerializeField] private GameObject _vul;
    [SerializeField] private GameObject _protect;
    [SerializeField] private GameObject _heal;
    public IEnumerator ShowEffect(string what, GameObject where)
    {
        GameObject effect;
        if (what == "heal") effect = _heal;
        else if (what == "vul") effect = _vul;
        else if (what == "Defend") effect = _protect;
        else effect = _resist;

        //Принимает только Model
        GameObject newEffect = Instantiate(effect, where.transform.Find("BulletTarget").position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(newEffect);
    }
}
