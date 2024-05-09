using System.Collections.Generic;
using UnityEngine;

public class Filter : MonoBehaviour
{
    [SerializeField] private GameObject content;
    private List<CardVeiw> tableList = new();
    [SerializeField] private IDCaracter _idCharacter;

    public void SetFilter(int filter)
    {
        tableList.Clear();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            tableList.Add(content.transform.GetChild(i).GetComponent<CardVeiw>());
        }
        foreach (CardVeiw item in tableList)
        {
            item.gameObject.SetActive(true);
        }
        if (filter == 2)
        {
            foreach (CardVeiw item in tableList)
            {
                if (_idCharacter.CharacterData[item.Id].Attributes.Type.Id != 0) 
                    item.gameObject.SetActive(false);
            }
        }
        else if (filter == 3)
        {
            foreach (CardVeiw item in tableList)
            {
                if (_idCharacter.CharacterData[item.Id].Attributes.Type.Id != 1)
                    item.gameObject.SetActive(false);
            }
        }
        else if (filter == 4)
        {
            foreach (CardVeiw item in tableList)
            {
                if (_idCharacter.CharacterData[item.Id].Attributes.Type.Id != 2)
                    item.gameObject.SetActive(false);
            }
        }
        else if (filter == 5)
        {
            foreach (CardVeiw item in tableList)
            {
                if (_idCharacter.CharacterData[item.Id].Attributes.Rang.Id != 0)
                    item.gameObject.SetActive(false);
            }
        }
        else if (filter == 6)
        {
            foreach (CardVeiw item in tableList)
            {
                if (_idCharacter.CharacterData[item.Id].Attributes.Rang.Id != 1)
                    item.gameObject.SetActive(false);
            }
        }
        else if (filter == 7)
        {
            foreach (CardVeiw item in tableList)
            {
                if (_idCharacter.CharacterData[item.Id].Attributes.Rang.Id != 2)
                    item.gameObject.SetActive(false);
            }
        }
        else if (filter == 8)
        {
            foreach (CardVeiw item in tableList)
            {
                if (_idCharacter.CharacterData[item.Id].Attributes.Rang.Id != 3)
                    item.gameObject.SetActive(false);
            }
        }
    }
}
