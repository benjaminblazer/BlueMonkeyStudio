using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float lifeTime;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        Destroy(gameObject,lifeTime);
    }
}
