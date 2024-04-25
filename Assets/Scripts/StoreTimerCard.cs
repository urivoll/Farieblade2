using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreTimerCard : MonoBehaviour
{
    [SerializeField] private StoreCardsChange _storeCardsChange;
    [SerializeField] private TextMeshProUGUI _textTime;

    private void OnEnable()
    {
        InvokeRepeating("CardTimer", 1f, 1f);
    }

    public void CardTimer()
    {
        if (StoreCardsChange.CardsChangeTimeSec != -1)
        {
            int timeDifference = 86400 - StoreCardsChange.CardsChangeTimeSec;
            if (timeDifference == 0)
            {
                StoreCardsChange.CardsChangeTimeSec = -1;
                StartCoroutine(_storeCardsChange.ChangeCards());
            }
            else
            {
                int hours = timeDifference / 3600;
                int minutes = timeDifference % 3600 / 60;
                int seconds = timeDifference % 60;

                _textTime.text = $"{StoreCardsChange.CardsChangeTimeDay:D2}:{hours:D2}:{minutes:D2}:{seconds:D2}";
            }
        }
    }
}
