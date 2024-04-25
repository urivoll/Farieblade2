using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlideDamageSpawner : SlideTextPool
{
    [SerializeField] private GameObject _slideTextPrefab;
    [SerializeField] private ElementalTypes[] _elementalTypes;
    public void TryShowSlideDamage(int inpDamage, int element, Vector2 spawnPoint)
    {
        if (TryGetObject(out GameObject slideTextPrefab))
        {
            int[] color;
            TextMeshProUGUI text = slideTextPrefab.GetComponentInChildren<TextMeshProUGUI>();
            slideTextPrefab.GetComponentInChildren<Image>().sprite = _elementalTypes[element].Sprite;
            if (element == 7)
            {
                color = new int[] { 150, 150, 150 };
                string miss;
                if (PlayerData.language == 0) miss = "Miss";
                else miss = "Промах";
                SetText(text, miss, spawnPoint, color);
                return;
            }
            if (inpDamage <= 0) return;
            color = new int[] { 255, 235, 0 };
            SetText(text, inpDamage.ToString(), spawnPoint, color);
        }
    }
    private void Start()
    {
        Initialize(_slideTextPrefab.gameObject);
    }
    private void SetText(TextMeshProUGUI textMeshPro, string text, Vector2 spawnPoint, int[] color)
    {
        textMeshPro.text = text;
        textMeshPro.gameObject.SetActive(true);
        textMeshPro.transform.position = spawnPoint;
    }
}
