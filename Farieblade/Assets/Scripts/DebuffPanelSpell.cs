using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DebuffPanelSpell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textType;
    [SerializeField] private TextMeshProUGUI textDescriprion;
    [SerializeField] private Image pic;
    [SerializeField] private Image picBase;
    [SerializeField] private Image picState;
    [SerializeField] private Sprite melee;
    [SerializeField] private Sprite range;
    [SerializeField] private Sprite buff;
    [SerializeField] private Sprite debuff;
    [SerializeField] private Sprite aura;
    [SerializeField] private Sprite mode;
    [SerializeField] private Sprite passive;
    [SerializeField] private Sprite meleeBase;
    [SerializeField] private Sprite buffBase;
    [SerializeField] private Sprite debuffBase;
    [SerializeField] private Sprite auraBase;
    [SerializeField] private Sprite modeBase;
    [SerializeField] private Sprite passiveBase;
    public void SetDebuff(int index)
    {
        DoIt(Turns.unitChoose.idDebuff[index].GetComponent<AbstractSpell>());
    }
    public void SetSpell(int index)
    {
        DoIt(Turns.unitChoose.GetComponent<Spells>().SpellList[index].GetComponent<AbstractSpell>());
    }
    public void SetMode(int index)
    {
        DoIt(Turns.unitChoose.GetComponent<Spells>().modeList[index].GetComponent<AbstractSpell>());
    }
    private void DoIt(AbstractSpell debuff2)
    {
        AbstractSpell magic = debuff2.GetComponent<AbstractSpell>();
        if (magic.Type == "Buff" || magic.Type == "Heal")
        {
            picState.sprite = buff;
            picBase.sprite = buffBase;
        }
        else if (magic.Type == "Debuff")
        {
            picState.sprite = debuff;
            picBase.sprite = debuffBase;
        }
        else if (magic.Type == "Passive")
        {
            picState.sprite = passive;
            picBase.sprite = passiveBase;
        }
        else if (magic.Type == "Mode")
        {
            picState.sprite = mode;
            picBase.sprite = modeBase;
        }
        else if (magic.Type == "Aura")
        {
            picState.sprite = aura;
            picBase.sprite = auraBase;
        }
        else if (magic.Type == "Ranged Magic")
        {
            picState.sprite = range;
            picBase.sprite = meleeBase;
        }
        else if (magic.Type == "Melee magic")
        {
            picState.sprite = melee;
            picBase.sprite = meleeBase;
        }
        pic.sprite = debuff2.transform.Find("Mask/Pic").gameObject.GetComponent<Image>().sprite;
        textName.text = magic.nameText;
        textType.text = magic.SType;
        textDescriprion.text = magic.description;
    }
}