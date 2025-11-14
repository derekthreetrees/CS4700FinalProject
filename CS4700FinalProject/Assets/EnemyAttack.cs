using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damageAmount = 10;
    public float damageCooldown = 1f;

    private float nextDamageTime = 0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time >= nextDamageTime)
        {
            HealthController player = other.GetComponent<HealthController>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }
}
