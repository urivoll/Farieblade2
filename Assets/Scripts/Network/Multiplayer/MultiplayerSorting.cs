using UnityEngine;

public class MultiplayerSorting : MonoBehaviour
{
    [SerializeField] private GameObject[] localUnits;
    private void OnEnable()
    {
        for (int i = 0; i < IdUnit.idLevel.Length; i++)
        {
            if (IdUnit.idLevel[i] != 0)
            {
                localUnits[i].SetActive(true);
            }
        }
        Sort();
    }
    private void OnDisable()
    {
        foreach (GameObject i in localUnits)
        {
            i.SetActive(false);
        }
    }
    public void Sort()
    {
        int count = 0;
        while (true)
        {
            float maxPower = -1;
            GameObject maxPowerUnit = null;
            foreach (GameObject item in localUnits)
            {
                if (
                    item.GetComponent<Unit>().Power > maxPower &&
                    item.GetComponent<Unit>().forSort == true && item.GetComponent<Unit>().level != 0)
                {
                    maxPowerUnit = item;
                    maxPower = item.GetComponent<Unit>().Power;
                }
            }
            if (maxPowerUnit == null) break;
            maxPowerUnit.GetComponent<Unit>().forSort = false;
            maxPowerUnit.transform.SetSiblingIndex(count);
            count++;
        }
        foreach (GameObject item in localUnits)
        {
            item.GetComponent<Unit>().forSort = true;
        }
    }
}

