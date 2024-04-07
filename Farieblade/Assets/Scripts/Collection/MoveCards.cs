using UnityEngine;
public class MoveCards : MonoBehaviour
{
    private GameObject[] unitOur = new GameObject[6];
    private GameObject[] unitEnemy = new GameObject[6];
    [SerializeField] private GameObject[] placeOur;
    [SerializeField] private GameObject[] placeEnemy;
    private void OnEnable()
    {
        for (int i = 0; i < PlayerData.troop.Length; i++)
        {
            if (PlayerData.troop[i] != -666)
            {
                unitOur[i] = Instantiate(PlayerData.myCollection[PlayerData.troop[i]], placeOur[i].transform);
                unitOur[i].SetActive(true);
                Destroy(unitOur[i].transform.Find("Card").gameObject.GetComponent<UIDragHandler>());
            }
            if (Campany.enemy[i] != -666)
            {
                unitEnemy[i] = Instantiate(PlayerData.defaultCards[Campany.enemy[i]], placeEnemy[i].transform);
                Destroy(unitEnemy[i].transform.Find("Card").gameObject.GetComponent<UIDragHandler>());
                unitEnemy[i].GetComponent<Unit>().grade = Campany.enemyGrades;
                unitEnemy[i].GetComponent<Unit>().level = Campany.enemyLevel[i];
                unitEnemy[i].GetComponent<Unit>().SetValues();
                unitEnemy[i].transform.Find("Card").gameObject.GetComponent<CardVeiw>().SetCardValues();
            }
        }
    }
    public void OnDisable()
    {
        for (int i = 0; i < unitOur.Length; i++)
        {
            if (unitOur[i] != null) Destroy(unitOur[i]);
            if (unitEnemy[i] != null) Destroy(unitEnemy[i]);
        }
    }
}