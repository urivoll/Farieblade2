using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Shooter : MonoBehaviour
{
    private Transform shootPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private AudioClip soundBeforeShoot;
    [SerializeField] private AudioClip soundSwish;
    [SerializeField] private AudioClip soundShoot;
    [SerializeField] private GameObject attackEffect;
    [SerializeField] private float timeBeforeShoot;
    [HideInInspector] public Bullet newBullet;
    void Start() => shootPoint = transform.Find("bullet");
    public IEnumerator Shoot(UnitProperties unitForHit, UnitProperties from, List<MakeMove> inpData)
    {
        int times = from.times;
        int damage = from.damage;
        float behiendTimes = from.behiendTimes;
        if (attackEffect != null) attackEffect.SetActive(true);
        if (soundBeforeShoot != null) BattleSound.sound.PlayOneShot(soundBeforeShoot);
        int count = 0;
        yield return new WaitForSeconds(timeBeforeShoot);
        GameObject bulletTarget = unitForHit.pathBulletTarget.gameObject;
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
            //промах
            else
            {
                if (unitForHit != null) unitForHit.Miss();
                newBullet.transform.rotation = Quaternion.Euler(0, 0, angle + Random.Range(-20, 20));
            }
            count++;
            if (times > 1) yield return new WaitForSeconds(behiendTimes);
        }
        yield return new WaitForSeconds(1f);
        Turns.hitDone = true;
        if (newBullet == null) yield break;
        newBullet.tag = "Respawn";
        newBullet = null;
    }
}
