using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Shooter : Weapon
{
    private Transform shootPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private AudioClip soundSwish;
    [SerializeField] private AudioClip soundShoot;
    [SerializeField] private GameObject attackEffect;

    void Start() => shootPoint = transform.Find("bullet");
    public override IEnumerator Attack(UnitProperties from, List<MakeMove> inpData)
    {
        UnitProperties unitForHit = _characterPlacement.CirclesMap[inpData[0].attackSend["sideTarget"], inpData[0].attackSend["placeTarget"]].ChildCharacter;
        int times = from.Weapon.Times;
        int damage = from.Weapon.Damage;
        if (attackEffect != null) attackEffect.SetActive(true);
        if (_soundBeforeHit != null) BattleSound.sound.PlayOneShot(_soundBeforeHit);
        int count = 0;
        yield return new WaitForSeconds(_timeBeforeHit);
        GameObject bulletTarget = unitForHit.PathBulletTarget.gameObject;
        Bullet newBullet = null;
        while (count != times)
        {
            if (attackEffect != null) attackEffect.SetActive(false);
            if (soundSwish != null) BattleSound.sound.PlayOneShot(soundSwish);
            BattleSound.sound.PlayOneShot(soundShoot);
            GameObject newBulletObj = Instantiate(bullet, shootPoint.position, Quaternion.identity, Turns.circlesTransform.transform);
            newBullet = newBulletObj.GetComponent<Bullet>();
            var direction = bulletTarget.transform.position - newBullet.transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (inpData[count].attackSend["catch"] == 1)
            {
                if (times > 0) newBullet.transform.rotation = Quaternion.Euler(0, 0, angle + Random.Range(0, -6));
                else newBullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                newBullet.unitTarget = unitForHit;
                newBullet.unitFrom = from;
                newBullet.inpData = inpData[count];
                Turns.shooterPunch?.Invoke(unitForHit, from, inpData[count].shooterHitSend);
            }
            else
            {
                //if (unitForHit != null) unitForHit.HpCharacter.Miss();
                newBullet.transform.rotation = Quaternion.Euler(0, 0, angle + Random.Range(-20, 20));
            }
            count++;
            if (times > 1) yield return new WaitForSeconds(_behiendTimes);
        }
        yield return new WaitForSeconds(1f);
        Turns.hitDone = true;
        if (newBullet == null) yield break;
        newBullet.tag = "Respawn";
        newBullet = null;
    }
}
public interface IWeapon
{
    public void Attack();
}
