using Newtonsoft.Json;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    [SerializeField] private Button deck;
    private GameObject obj = null;
    private GameObject DefaultCard = null;
    private GameObject DefaultCardPrefub = null;
    [SerializeField] private GameObject parentTable;
   
    [SerializeField] private AudioClip _deckMove;
    [SerializeField] private AudioClip _pull;
    [SerializeField] private AudioClip _turnOver;
    [SerializeField] private AudioClip _swish;
    [SerializeField] private AudioClip _upgrade;
    [SerializeField] private AudioClip _zoom;
    [SerializeField] private AudioClip _upgradeSound;

    [SerializeField] private GameObject _return;
    [SerializeField] private GameObject _effect;
    [SerializeField] private GameObject _deckTable;

    [SerializeField] private Sprite[] DeckType;

    [SerializeField] private Image _imageDeck;
    [SerializeField] private GameObject mergerButton;
    private bool countinue = false;
    private int upGrade = 0;
    [SerializeField] private GameObject deck3;
    private int frag;
    [SerializeField] private GameObject box;

    [SerializeField] private GameObject UpgradeTable;
    [SerializeField] private TextMeshProUGUI DamageOrig;
    [SerializeField] private TextMeshProUGUI HPOrig;
    [SerializeField] private TextMeshProUGUI AccOrig;
    [SerializeField] private TextMeshProUGUI InitOrig;
    [SerializeField] private TextMeshProUGUI DamageUp;
    [SerializeField] private TextMeshProUGUI HPUp;
    [SerializeField] private TextMeshProUGUI AccUp;
    [SerializeField] private TextMeshProUGUI InitUp;
    [SerializeField] private GameObject SoulSeekerAttack;
    private int ID;

    [SerializeField] private TextMeshProUGUI textReturn;

    public void clickDeck()
    {
        StartCoroutine(Distirbution());
    }
    private IEnumerator Distirbution()
    {
        deck.interactable = false;
        Sound.sound.PlayOneShot(_deckMove);
        box.transform.parent.GetComponent<Animator>().SetTrigger("open");

        DefaultCard = PlayerData.defaultCards[ID];
        DefaultCardPrefub = Instantiate(DefaultCard, box.transform.position, Quaternion.identity, parentTable.transform);
        DefaultCardPrefub.transform.SetSiblingIndex(3);

        DefaultCardPrefub.GetComponent<Unit>().level = PlayerData.myCollection[DefaultCardPrefub.GetComponent<Unit>().ID].GetComponent<Unit>().level;
        DefaultCardPrefub.GetComponent<Unit>().grade = PlayerData.myCollection[DefaultCardPrefub.GetComponent<Unit>().ID].GetComponent<Unit>().grade;
        DefaultCardPrefub.GetComponent<Unit>().SetValues();
        DefaultCardPrefub.transform.Find("Card").gameObject.GetComponent<CardVeiw>().SetCardValues();


        Destroy(DefaultCardPrefub.transform.Find("Card").gameObject.GetComponent<UIDragHandler>());
        DefaultCardPrefub.GetComponent<Animator>().SetTrigger("shirt");
        yield return new WaitForSeconds(1.5f);
        //ÕŒ¬¿ﬂ  ¿–“¿!
        Sound.sound.PlayOneShot(_zoom);
        if (upGrade == 0)
        {
            DefaultCardPrefub.GetComponent<Animator>().SetTrigger("new");
            yield return new WaitForSeconds(0.7f);
            Sound.sound.PlayOneShot(_turnOver);
            if (PlayerData.language == 0) textReturn.text = "To the collection!";
            else if (PlayerData.language == 1) textReturn.text = "ƒÓ·‡‚ËÚ¸ ‚ ÍÓÎÎÂÍˆË˛!";
        }
        //”À”◊ÿ≈Õ»≈  ¿–“€
        if (upGrade == 1)
        {
            DefaultCardPrefub.GetComponent<Animator>().SetTrigger("up");
            yield return new WaitForSeconds(0.7f);
            Sound.sound.PlayOneShot(_turnOver);
            yield return new WaitForSeconds(2.3f);
            mergerButton.SetActive(true);
            UpgradeTable.SetActive(true);

            DamageOrig.text = Convert.ToString(DefaultCardPrefub.GetComponent<Unit>().damage);
            HPOrig.text = Convert.ToString(DefaultCardPrefub.GetComponent<Unit>().hpBase);
            AccOrig.text = Convert.ToString(DefaultCardPrefub.GetComponent<Unit>().accuracy);
            InitOrig.text = Convert.ToString(DefaultCardPrefub.GetComponent<Unit>().initiative);

            float damageOrigValue = DefaultCardPrefub.GetComponent<Unit>().damage;
            float hpOrigValue = DefaultCardPrefub.GetComponent<Unit>().hpBase;

            DefaultCardPrefub.GetComponent<Unit>().grade += 1;
            DefaultCardPrefub.GetComponent<Unit>().SetValues();

            DamageUp.text = "+" + Convert.ToString(DefaultCardPrefub.GetComponent<Unit>().damage - damageOrigValue);
            HPUp.text = "+" + Convert.ToString(DefaultCardPrefub.GetComponent<Unit>().hpBase - hpOrigValue);
            AccUp.text = "+1";
            InitUp.text = "+0";
            obj.GetComponent<Unit>().grade += 1;
            obj.GetComponent<Unit>().SetValues();
            obj.transform.Find("Card").gameObject.GetComponent<CardVeiw>().SetCardValues();

            Sound.sound.PlayOneShot(_upgradeSound);
            while (countinue == false) yield return null;
            Sound.sound.PlayOneShot(_swish);
            UpgradeTable.GetComponent<Animator>().SetTrigger("off");
            countinue = false;
            mergerButton.SetActive(false);
            DefaultCardPrefub.GetComponent<Animator>().SetTrigger("up2");
            yield return new WaitForSeconds(0.5f);
            Sound.sound.PlayOneShot(_swish);
            yield return new WaitForSeconds(0.5f);
            Sound.sound.PlayOneShot(_upgrade);
            yield return new WaitForSeconds(2.5f);
            Instantiate(SoulSeekerAttack, new Vector2(0,-2), Quaternion.Euler(-90, 0, 0));
            yield return new WaitForSeconds(1f);
            UpgradeTable.SetActive(false);

            if (PlayerData.language == 0) textReturn.text = "To the collection!";
            else if (PlayerData.language == 1) textReturn.text = "ƒÓ·‡‚ËÚ¸ ‚ ÍÓÎÎÂÍˆË˛!";
            DefaultCardPrefub.transform.Find("Card").gameObject.GetComponent<CardVeiw>().SetCardValues();
        }
        //”ƒ¿À≈Õ»≈  ¿–“€!
        else if (upGrade == 2)
        {
            DefaultCardPrefub.GetComponent<Animator>().SetTrigger("new");
            yield return new WaitForSeconds(0.7f);
            Sound.sound.PlayOneShot(_turnOver);
            yield return new WaitForSeconds(0.8f);
            GameObject effect = Instantiate(_effect, new Vector2(0, 0), Quaternion.identity);
            effect.transform.localScale = new Vector2(5.5f, 5.5f);
            yield return new WaitForSeconds(0.3f);
            Destroy(DefaultCardPrefub);
            Inventory.InventoryPlayer[frag] += 10;
            if (PlayerData.language == 0) textReturn.text = "Return";
            else if (PlayerData.language == 1) textReturn.text = "¬ÂÌÛÚ¸Òˇ";
        }
        yield return new WaitForSeconds(0.4f);
        _return.SetActive(true);
    }
    public void Return()
    {
        if (DefaultCardPrefub != null)
        {
            Sound.sound.PlayOneShot(_pull);
            if (upGrade == 1)
            DefaultCardPrefub.GetComponent<Animator>().SetTrigger("go");
            else if (upGrade == 0)
                DefaultCardPrefub.GetComponent<Animator>().SetTrigger("go2");
            Invoke("Exit", 0.8f);
        }
        else Exit();
    }
    private void Exit()
    {
        deck3.SetActive(false);
        box.GetComponent<Image>().color = new(255, 255, 255, 255);
        upGrade = 0;
        _return.SetActive(false);
        Destroy(DefaultCardPrefub);
        obj = null;
        DefaultCardPrefub = null;
        deck.interactable = true;
        box.transform.position = new Vector2(0, 0);
        box.transform.parent.GetComponent<Animator>().SetTrigger("re");
        _deckTable.SetActive(false);
        DefaultCard = null;
    }
    public void BuyCard(string json)
    {
        BuyCardJson obj3 = JsonConvert.DeserializeObject<BuyCardJson>(json);
        frag = obj3.rang;
        ID = obj3.unit;
        obj = PlayerData.myCollection[ID];
        _imageDeck.sprite = DeckType[obj3.rang];
        if (obj3.taskProgressWeekly != -666) TaskManager.taskProgressWeekly = obj3.taskProgressWeekly;
        if (obj3.TaskIdW4 != -666) TaskManager.Weekly[4] = obj3.TaskIdW4;

        if (obj3.level != -666)
        {
            upGrade = 0;
            IdUnit.idLevel[obj3.unit] = 1;
            obj.GetComponent<Unit>().level = 1;
            obj.transform.Find("Card").gameObject.GetComponent<CardVeiw>().SetCardValues();
        }
        else if (obj3.grade != -666) upGrade = 1;
        else upGrade = 2;
    }
    public void Click()
    {
        countinue = true;
    }
}
public class BuyCardJson
{
    public int rang;
    public int inventory;
    public int level;
    public int grade;
    public int unit;
    public int TaskIdW4;
    public int taskProgressWeekly;
}