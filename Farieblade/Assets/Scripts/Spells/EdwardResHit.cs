public class EdwardResHit : AbstractSpell
{
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            parentUnit.transform.Find("ModeParticle2").gameObject.SetActive(true);
        }
        if (PlayerData.language == 0)
        {
            nameText = "Strike the resistance";
            SType = "Mode";
            description = $"Edward endows his weapon with special magic that itself adapts to the enemyТs resistance. (The ability does not apply to opponents who have no resistance).";
        }
        else
        {
            nameText = "”дар по сопротивлению";
            SType = "–ежим";
            description = $"Ёдвард надел€ет свое оружие особой магией котора€ сама подстраиваетс€ под сопротивление противника. (—пособность не распростран€етс€ на противников у которых нет сопротивлени€).";
        }
    }
    public override void EndDebuff()
    {
        parentUnit.transform.Find("ModeParticle2").gameObject.SetActive(false);
    }
}
