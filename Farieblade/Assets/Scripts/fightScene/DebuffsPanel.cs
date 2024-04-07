using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuffsPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] debuffs;
    private List<GameObject> debuffsTurnUnit;
    private void OnEnable()
    {
        if (Turns.unitChoose == null) return;
        debuffsTurnUnit = Turns.unitChoose.idDebuff;

        for (int i = 0; i < debuffsTurnUnit.Count; i++)
        {
            debuffs[i].SetActive(true);
            debuffs[i].transform.Find("Mask/Pic").gameObject.GetComponent<Image>().sprite = debuffsTurnUnit[i].transform.Find("Mask/Pic").gameObject.GetComponent<Image>().sprite;
            debuffs[i].transform.Find("Frame").gameObject.GetComponent<Image>().color = debuffsTurnUnit[i].transform.Find("Frame").gameObject.GetComponent<Image>().color;
        }
    }
    private void OnDisable()
    {
        if (debuffsTurnUnit == null || debuffsTurnUnit.Count <= 0) return;
        for (int i = 0; i < debuffsTurnUnit.Count; i++) debuffs[i].SetActive(false);
    }
}
