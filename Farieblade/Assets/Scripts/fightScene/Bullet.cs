using UnityEngine;

public class Bullet : MonoBehaviour
{
    public UnitProperties unitFrom;
    public int element;
    [HideInInspector] public float damage;
    [HideInInspector] public UnitProperties unitTarget;
    public MakeMove inpData;
    private BulletAnimation _bulletAnimation;
    public float speed;
    private AudioSource _soundHit;
    [SerializeField] private AudioClip Clip;
    private bool stop = false;
    private bool notWork = false;
    [HideInInspector] public GameObject targetBullet = null;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject debuff;
    public GameObject end;
    public GameObject end2;
    private void Start()
    {
        if(unitTarget != null) targetBullet = unitTarget.pathBulletTarget.gameObject;
        _soundHit = gameObject.GetComponent<AudioSource>();
        if (gameObject.GetComponent<BulletAnimation>() == true)
        _bulletAnimation = gameObject.GetComponent<BulletAnimation>();
        Invoke("Destroy", 1.5f);
    }
    void FixedUpdate()
    {
        if (stop == false) transform.Translate(speed * Time.fixedDeltaTime, 0, 0, Space.Self);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == targetBullet && notWork == false && unitTarget.hp != 0)
        {
            if (debuff != null)
            {
                GameObject newObject = Instantiate(debuff, unitTarget.pathDebuffs);
                if(unitFrom != null) newObject.GetComponent<AbstractSpell>().fromUnit = unitFrom.pathParent;
                else newObject.GetComponent<AbstractSpell>().fromUnit = unitFrom.GetComponent<AbstractSpell>().fromUnit;
            }
            notWork = true;
            stop = true;
            if(inpData != null) unitTarget.TakeDamage(unitFrom, inpData);
            else unitTarget.SpellDamage(damage, element);
            if (_soundHit != null)
            {
                _soundHit.pitch = Random.Range(0.8f, 1f);
                _soundHit.PlayOneShot(Clip);
            }

            if (gameObject.GetComponent<BulletAnimation>() == true)
            {
                _bulletAnimation.SetCaracterState("death");
                gameObject.tag = "Respawn";
            }
            else if (start != null)
            {
                start.SetActive(false);
                if (end != null)
                    end.SetActive(true);
                gameObject.tag = "Respawn";
                Destroy(gameObject, 2f);
            }
            else Destroy(gameObject);
        }
    }
    private void Destroy() 
    {
        if((end != null && end.activeSelf == false) || end == null) Destroy(gameObject);
    }
}