public class ElectricWeak : AbstractSpell
{
    public float Value = 0.15f;
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            startNumberTurn = Turns.numberTurn + duration;
            if (PlayerData.language == 0)
            {
                nameText = "Weakness to electricity";
                SType = "Buff";
                description = $"This character has a weakness to electricity. All air damage deals an additional 15% damage.";
            }
            else
            {
                nameText = "Слабость к электричеству";
                SType = "Усиливающее заклинание";
                description = $"Этот персонаж имеет слабость к электричеству. Всякий урон воздухом наносит дополнительные 15% урона.";
            }
        }
    }
}
