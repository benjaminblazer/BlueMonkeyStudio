using UnityEngine;

public class BotManager : MonoBehaviour
{
    public int maxCouldown;

    private GameManager GM;
    private double fireCouldown;
    private ShootManager shootManager;
    private const float RANDOM_RATE = 1;

    private void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        fireCouldown = maxCouldown;
        shootManager = GetComponentInChildren<ShootManager>();
        this.enabled = !(shootManager == null);
    }

    void FixedUpdate()
    {
        ManageBotShooting();
    }

    void ManageBotShooting()
    {
        if (fireCouldown < 0)
        {
            shootManager.CastBullet();
            fireCouldown = Random.Range(maxCouldown * (1 - RANDOM_RATE), maxCouldown * (1 + RANDOM_RATE));
        }
        fireCouldown -= Time.deltaTime * GM.level;
    }
}
