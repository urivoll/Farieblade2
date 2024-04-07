using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SideUnitUi : MonoBehaviour
{
    [SerializeField] private GameObject[] spellListUI;
    [SerializeField] private GameObject[] spellListSilence;
    [SerializeField] private GameObject[] spellListMana;
    [SerializeField] private GameObject[] particles;

    [SerializeField] private GameObject[] modeListUI;
    [SerializeField] private GameObject[] modeListMark;
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject defend;
    [SerializeField] private BattleNetwork battleNetwork;
    [SerializeField] private CheckAllowHit check;
    public static int spell = -555;
    private EnergyUnit energy;
    private List<GameObject> currentSpellList;
    private List<GameObject> currentModeList;
    public static bool modeBlock = false;
    [SerializeField] private GameObject buttonHit;
    [SerializeField] private Sprite[] attackSprite;
    [SerializeField] private Image attackImg;
    private void Start() => Exit();
    public void SetSpells()
    {
        if (PlayerData.ai != 2) defend.SetActive(true);
        energy = Turns.turnUnit.pathEnergy;

        currentSpellList = Turns.turnUnit.pathSpells.SpellList;
        currentModeList = Turns.turnUnit.pathSpells.modeList;
        
        for (int i = 0; i < currentSpellList.Count; i++)
        {
            if (i == 3) break;
            //—‡ÎÓ
            if (Turns.turnUnit.silence == true) spellListSilence[i].SetActive(true);
            else spellListSilence[i].SetActive(false);

            string state = currentSpellList[i].GetComponent<AbstractSpell>().state;
            if (state == "Passive" || state == "Aura") continue;
            if (PlayerData.ai != 2)
            {
                spellListUI[i].SetActive(true);
                spellListUI[i].transform.Find("mask/image").gameObject.GetComponent<Image>().sprite = currentSpellList[i].transform.Find("Mask/Pic").gameObject.GetComponent<Image>().sprite;
            }
            if (Turns.turnUnit.pathParent.Energy—onsumption > energy.energy) spellListMana[i].SetActive(true);
        }
        if (Turns.turnUnit.damage <= 0)
        {
            if (Turns.turnUnit.pathEnergy.energy >= Turns.turnUnit.pathParent.Energy—onsumption &&
                !Turns.turnUnit.silence) ChangeState(0);
            else spell = -555;
            buttonHit.SetActive(false);
        }
        else
        {
            ChangeState(-666);
            if (PlayerData.ai != 2) buttonHit.SetActive(true);
            attackImg.sprite = attackSprite[Turns.turnUnit.state];
        }
        if (currentModeList.Count <= 0) return;
        int startModeIndex = Turns.turnUnit.pathSpells.modeIndex;
        for (int i = 0; i < currentModeList.Count; i++)
        {
            modeListUI[i].SetActive(true);
            modeListUI[i].transform.Find("mask/image").gameObject.GetComponent<Image>().sprite = currentModeList[i].transform.Find("Mask/Pic").gameObject.GetComponent<Image>().sprite;
            if (startModeIndex != i) modeListMark[i].SetActive(false);
            else modeListMark[i].SetActive(true);
        }
    }
    public void Exit()
    {
        for (int i = 0; i < spellListUI.Length; i++)
        {
            spellListUI[i].SetActive(false);
            spellListSilence[i].SetActive(false);
            spellListMana[i].SetActive(false);
            particles[i].SetActive(false);
        }
        for (int i = 0; i < 2; i++)
        {
            modeListUI[i].SetActive(false);
            modeListMark[i].SetActive(false);
        }
        defend.SetActive(false);
        buttonHit.SetActive(false);
    }
    public void ChangeState(int index)
    {
        if (index != -666)
        {
            AbstractSpell currentSpell;
            currentSpell = currentSpellList[index].GetComponent<AbstractSpell>();
            spell = index;
            for (int i = 0; i < particles.Length; i++) particles[i].SetActive(false);
            particles[index].SetActive(true);
            if (currentSpell.ToEnemy)
            {
                if (currentSpell.AllPlace) check.CheckUnit(1, true);
                else check.CheckUnit(0, true);
            }
            else
            {
                if (currentSpell.NotMe) check.CheckUnit(2, true, true);
                else check.CheckUnit(2, true);
            }
        }
        else if (index == -666)
        {
            spell = index;
            particles[3].SetActive(true);
            SetDefaultHit();
        }
    }
    public void ChangeMode2(int index)
    {
        StartCoroutine(ChangeMode(index));
    }
    public IEnumerator ChangeMode(int i)
    {
        modeBlock = true;
        foreach (GameObject i2 in modeListMark) i2.SetActive(false);
        modeListMark[i].SetActive(true);
        foreach (Button i2 in buttons) i2.interactable = false;
        Turns.turnUnit.pathSpells.SetState(i);
        yield return new WaitForSeconds(0.5f);
        foreach (Button i2 in buttons) i2.interactable = true;
        modeBlock = false;
    }
    public void SetDefaultHit()
    {
        if (Turns.turnUnit.state == 0) check.CheckUnit(0, false);
        else if (Turns.turnUnit.state == 1 || Turns.turnUnit.state == 2) check.CheckUnit(1, false);
    }
}
