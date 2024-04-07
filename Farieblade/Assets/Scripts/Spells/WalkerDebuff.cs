using System.Collections;
using UnityEngine;
public class WalkerDebuff : MonoBehaviour
{
    private bool work = true;
    Bullet bullet;
    private void Start()
    {
        bullet = GetComponent<Bullet>();
        bullet.speed = 25;
        bullet.unitTarget = null;
        bullet.targetBullet = null;
        StartCoroutine(Debuff());
    }

    private IEnumerator Debuff()
    {
        yield return new WaitForSeconds(0.2f);
        Instantiate(Camera.main.GetComponent<Turns>().lightning, gameObject.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        bullet.unitTarget = bullet.unitFrom;
        bullet.targetBullet = bullet.unitFrom.pathBulletTarget.gameObject;
        bullet.inpData = null;
        bullet.element = bullet.unitFrom.pathParent.damageType;

        work = false;
        var direction = bullet.unitFrom.transform.position - gameObject.transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void Update()
    {
        if (work) bullet.speed -= 60 * Time.deltaTime;
        else bullet.speed += 60 * Time.deltaTime;
    }
}
