using UnityEngine;

public class Filter : MonoBehaviour
{
    public void SetFilter(int filter)
    {
        foreach (GameObject item in PlayerData.myCollection)
        {
            item.SetActive(true);
        }

        if (filter == 2)
        {
            foreach (GameObject item in PlayerData.myCollection)
            {
                if (item.GetComponent<Unit>().Type != 0 && item.transform.Find("Card").gameObject.GetComponent<UIDragHandler>().parentCircle == null) item.SetActive(false);
            }
        }
        else if (filter == 3)
        {
            foreach (GameObject item in PlayerData.myCollection)
            {
                if (item.GetComponent<Unit>().Type != 1 &&
                    item.transform.Find("Card").gameObject.GetComponent<UIDragHandler>().parentCircle == null)
                    item.SetActive(false);
            }
        }
        else if (filter == 4)
        {
            foreach (GameObject item in PlayerData.myCollection)
            {
                if (item.GetComponent<Unit>().Type != 2 &&
                    item.transform.Find("Card").gameObject.GetComponent<UIDragHandler>().parentCircle == null)
                    item.SetActive(false);
            }
        }
        else if (filter == 5)
        {
            foreach (GameObject item in PlayerData.myCollection)
            {
                if (item.GetComponent<Unit>().rang != 0 && 
                    item.transform.Find("Card").gameObject.GetComponent<UIDragHandler>().parentCircle == null)
                    item.SetActive(false);
            }
        }
        else if (filter == 6)
        {
            foreach (GameObject item in PlayerData.myCollection)
            {
                if (item.GetComponent<Unit>().rang != 1 &&
                    item.transform.Find("Card").gameObject.GetComponent<UIDragHandler>().parentCircle == null)
                    item.SetActive(false);
            }
        }
        else if (filter == 7)
        {
            foreach (GameObject item in PlayerData.myCollection)
            {
                if (item.GetComponent<Unit>().rang != 2 && item.transform.Find("Card").gameObject.GetComponent<UIDragHandler>().parentCircle == null)
                    item.SetActive(false);
            }
        }
        else if (filter == 8)
        {
            foreach (GameObject item in PlayerData.myCollection)
            {
                if (item.GetComponent<Unit>().rang != 3 && item.transform.Find("Card").gameObject.GetComponent<UIDragHandler>().parentCircle == null)
                    item.SetActive(false);
            }
        }
    }
}
