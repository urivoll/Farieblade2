using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class MapMarks : MonoBehaviour
{
    [SerializeField] private int map1_1;
    [SerializeField] private int map2_1;
    [SerializeField] private int map1_2;
    [SerializeField] private int map2_2;
    [SerializeField] private int map1_3;
    [SerializeField] private int map2_3;
    [SerializeField] private int battleMap;
    [SerializeField] private int idPlace;
    [SerializeField] private int Grade;
    [SerializeField] private int Level1_1;
    [SerializeField] private int Level2_1;
    [SerializeField] private int Level1_2;
    [SerializeField] private int Level2_2;
    [SerializeField] private int Level1_3;
    [SerializeField] private int Level2_3;
    [SerializeField] private Sprite done;
    private Button _button;

    public void Start()
    {
         _button = GetComponent<Button>();
        if (Campany.changeProgress == true)
        {
            CheckCampanyBefore();
            if (idPlace == Campany.campanyProgress)
                StartCoroutine(Change());
            else if (idPlace == Campany.campanyProgress - 1)
            {
                StartCoroutine(ChangeOff());
            }
        }
        else
        {
            CheckCampany();

        }
    }
    private void OnEnable()
    {
        if (Campany.changeProgress == false && Campany.campanyProgress == idPlace) GetComponent<Animator>().SetTrigger("idle");
    }
    public void SetEnemyMap() 
    {
        Campany.battleField = battleMap;
        Campany.currentPlace = idPlace;
        Campany.enemy[0] = map1_1;
        Campany.enemy[1] = map2_1;
        Campany.enemy[2] = map1_2;
        Campany.enemy[3] = map2_2;
        Campany.enemy[4] = map1_3;
        Campany.enemy[5] = map2_3;
        Campany.enemyLevel[0] = Level1_1;
        Campany.enemyLevel[1] = Level2_1;
        Campany.enemyLevel[2] = Level1_2;
        Campany.enemyLevel[3] = Level2_2;
        Campany.enemyLevel[4] = Level1_3;
        Campany.enemyLevel[5] = Level2_3;
        Campany.enemyGrades = Grade;
        Campany.enemyGrade[0] = Grade;
        Campany.enemyGrade[1] = Grade;
        Campany.enemyGrade[2] = Grade;
        Campany.enemyGrade[3] = Grade;
        Campany.enemyGrade[4] = Grade;
        Campany.enemyGrade[5] = Grade;
        PlayerData.afterFight = 2;
    }
    private void CheckCampany()
    {
        if (Campany.campanyProgress > idPlace)
        {
            gameObject.GetComponent<Image>().sprite = done;
            gameObject.GetComponent<Button>().interactable = false;
        }
        else if (Campany.campanyProgress == idPlace)
            _button.interactable = true;
        else _button.interactable = false;
    }
    private void CheckCampanyBefore()
    {
        if (idPlace < Campany.campanyProgress - 1)
        {
            gameObject.GetComponent<Image>().sprite = done;
            gameObject.GetComponent<Button>().interactable = false;
        }
        else if (idPlace == Campany.campanyProgress - 1)
            _button.interactable = true;
        else _button.interactable = false;
    }
    private IEnumerator Change()
    {
        Campany.changeProgress = false;
        LoadingManager.LoadingIcon.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        GetComponent<Animator>().SetTrigger("up");
        yield return new WaitForSeconds(0.1f);
        _button.interactable = true;
        LoadingManager.LoadingIcon.SetActive(false);
    }
    private IEnumerator ChangeOff()
    {
        yield return new WaitForSeconds(0.8f);
        GetComponent<Animator>().SetTrigger("off");
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Image>().sprite = done;
        gameObject.GetComponent<Button>().interactable = false;
    }
}
