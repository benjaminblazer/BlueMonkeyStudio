using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Collider MainCollider;
    public Collider[] AllColliders;
    public int maxHealth = 100;
    public int health;

    public HealthBar healthBar;

    private void Start()
    {
        health = maxHealth;
        healthBar.MaxHealth(health);
    }
    void Awake()
    {
        SetupColliders();
        DoRagdoll(false);
        SetKenimatic(true);
    }
    void SetupColliders()
    {
        MainCollider = GetComponent<Collider>();
        AllColliders = GetComponentsInChildren<Collider>(true);
        Physics.IgnoreLayerCollision(0, 6);
    }
    
    public void DoRagdoll(bool isRagdoll)
    {
        SetKenimatic(!isRagdoll);
        GetComponent<Rigidbody>().useGravity = !isRagdoll;
        GetComponent<Animator>().enabled = !isRagdoll;
    }
    
    //Used to avoid force stacking on player during animation
    void SetKenimatic(bool kinematic)
    {
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            if (rb.CompareTag("Unanimated"))
            {
                rb.isKinematic = false;
            }
            else
            {
                rb.isKinematic = kinematic;
            }
        }
    }
}
