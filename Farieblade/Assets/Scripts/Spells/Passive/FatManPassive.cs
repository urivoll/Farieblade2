using System;

public class FatManPassive : AbstractSpell
{
    public float Value;

    void Start()
    {
        Value = 0.2f + (fromUnit.grade * 0.01f);
        if (PlayerData.language == 0)
        {
            nameText = "Dungeon Unity";
            SType = "Passive";
            description = $"Fat Man receives a damage bonus for each ally from his faction.\r\nDamage: +{Convert.ToInt32(Value * 100)}%";
        }
        else
        {
            nameText = "Единство подземелий";
            SType = "Пассивная";
            description = $"Толстяк получает бонус к урону за каждого союзника из его фракции.\r\nУрон: +{Convert.ToInt32(Value * 100)}%";
        }
    }
}
