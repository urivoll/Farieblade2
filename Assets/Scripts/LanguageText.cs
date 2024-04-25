using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageText : MonoBehaviour
{
    public string[] text;
    private TextMeshProUGUI textLine;

    private void Start()
    {
        textLine = gameObject.GetComponent<TextMeshProUGUI>();
        textLine.text = text[PlayerData.language];
    }
}