using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public HealthController enemyHealth; // assign enemy's HealthController
    public Slider slider;                // assign slider in inspector

    void Start()
    {
        if (enemyHealth != null && slider != null)
        {
            slider.maxValue = enemyHealth.maxHealth;  // important
            slider.value = enemyHealth.currentHealth;
        }
    }

    void Update()
    {
        if (enemyHealth != null && slider != null)
        {
            slider.value = enemyHealth.currentHealth;
        }
    }
}
