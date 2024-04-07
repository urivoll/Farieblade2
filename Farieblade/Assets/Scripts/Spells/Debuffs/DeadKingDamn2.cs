public class DeadKingDamn2 : AbstractSpell
{
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            duration = 2;
            if (PlayerData.language == 0)
            {
                nameText = "Weakness";
                SType = "Debuff";
                description = $"This character is weak; when hit, there is a 50% chance of dealing only 40% of the damage.";
            }
            else
            {
                nameText = "—лабость";
                SType = "ѕрокл€тье";
                description = $"Ётот персонаж слаб, при ударе есть 50% шанс нанести лишь 40% от урона.";
            }
        }
    }
}
