using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideUnitUi : MonoBehaviour
{
    public static int spell = -555;
    public static bool modeBlock = false;

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
    [SerializeField] private GameObject buttonHit;
    [SerializeField] private Sprite[] attackSprite;
    [SerializeField] private Image attackImg;

    private EnergyUnit energy;
    private List<GameObject> currentSpellList;
    private List<GameObject> currentModeList;

    private void Start() => Exit();
    public void SetSpells()
    {
        if (PlayerData.ai != 2) 
            defend.SetActive(true);
        energy = Turns.turnUnit.Energy;

        currentSpellList = Turns.turnUnit.Spells.SpellList;
        currentModeList = Turns.turnUnit.Spells.modeList;
        
        for (int i = 0; i < currentSpellList.Count; i++)
        {
            if (i == 3) break;
            //����
            if (Turns.turnUnit.CharacterState.Silence == true) 
                spellListSilence[i].SetActive(true);
            else 
                spellListSilence[i].SetActive(false);

            string state = currentSpellList[i].GetComponent<AbstractSpell>().state;
            if (state == "Passive" || state == "Aura") continue;
            if (PlayerData.ai != 2)
            {
                spellListUI[i].SetActive(true);
                spellListUI[i].transform.Find("mask/image").gameObject.GetComponent<Image>().sprite = currentSpellList[i].transform.Find("Mask/Pic").gameObject.GetComponent<Image>().sprite;
            }
            if (Turns.turnUnit.Energy�onsumption > energy.energy) spellListMana[i].SetActive(true);
        }
        if (Turns.turnUnit.Weapon.Damage <= 0)
        {
            if (Turns.turnUnit.Energy.energy >= Turns.turnUnit.Energy�onsumption &&
                !Turns.turnUnit.CharacterState.Silence) ChangeState(0);
            else 
                spell = -555;
            buttonHit.SetActive(false);
        }
        else
        {
            ChangeState(-666);
            if (PlayerData.ai != 2) buttonHit.SetActive(true);
            attackImg.sprite = attackSprite[Turns.turnUnit.Weapon.Type];
        }
        if (currentModeList.Count <= 0) return;
        int startModeIndex = Turns.turnUnit.Spells.modeIndex;
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
        Turns.turnUnit.Spells.SetState(i);
        yield return new WaitForSeconds(0.5f);
        foreach (Button i2 in buttons) i2.interactable = true;
        modeBlock = false;
    }
    public void SetDefaultHit()
    {
        if (Turns.turnUnit.Weapon.Type == 0) check.CheckUnit(0, false);
        else if (Turns.turnUnit.Weapon.Type == 1 || Turns.turnUnit.Weapon.Type == 2) check.CheckUnit(1, false);
    }
}
