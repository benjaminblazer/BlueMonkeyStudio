using UnityEngine;

public class ShootManager : MonoBehaviour
{
    public GameObject Bullet;
    RaycastHit hit;
    Rigidbody rb;
    public float recoilForce = 2000f;
    public int damages;
    private bool isBot;
    public float bulletSpeed;
    public Transform bulletSpawn;
    private GameManager GM;
    private ParticleSystem muzzleFlash;
    private void Start()
    {
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        isBot = GetComponentInParent<BotManager>() != null;
        if (isBot)
            recoilForce *= 1.5f;
        GM = GameObject.Find("GameManager").GetComponent< GameManager>();
    }
    void Update()
    {
        if (!isBot && Input.GetMouseButtonDown(0))
        {
            CastBullet();
        }
    }
    public void CastBullet()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            ManageDamages(hit.collider);
        }
        muzzleFlash.Play();
        rb.AddForce(-Vector3.forward * recoilForce);
        GameObject currentBullet = Instantiate(Bullet, bulletSpawn.position,Quaternion.identity);
        currentBullet.transform.forward = bulletSpawn.TransformDirection(Vector3.forward);
        currentBullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.TransformDirection(Vector3.forward)* bulletSpeed, ForceMode.Impulse);

    }
    void ManageDamages(Collider collider)
    {
        var enemy = collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.health = Mathf.Max(enemy.health - damages, 0);
            enemy.healthBar.SetHealth(enemy.health);
            if (enemy.health == 0)
                Die(enemy);
        }
    }

    void Die(Enemy enemy)
    {
        Camera.main.gameObject.GetComponent<CameraController>().target = enemy.transform.position;
        enemy.DoRagdoll(true);
        GM.LevelTransition(!isBot);
    }
}
