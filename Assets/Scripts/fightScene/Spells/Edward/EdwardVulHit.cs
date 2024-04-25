public class EdwardVulHit : AbstractSpell
{
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            parentUnit.transform.Find("ModeParticle1").gameObject.SetActive(true);
        }
        if (PlayerData.language == 0)
        {
            nameText = "Weak spot";
            SType = "Mode";
            description = $"Edward endows his weapon with special magic that itself adapts to the enemyТs weak spot. (The ability does not apply to opponents who do not have a weak point).";
        }
        else
        {
            nameText = "”€звимое место";
            SType = "–ежим";
            description = $"Ёдвард надел€ет свое оружие особой магией котора€ сама подстраиваетс€ под у€звимое место противника. (—пособность не распростран€етс€ на противников у которых нет у€звимого места).";
        }
    }
    public override void EndDebuff()
    {
        parentUnit.transform.Find("ModeParticle1").gameObject.SetActive(false);
    }
}
