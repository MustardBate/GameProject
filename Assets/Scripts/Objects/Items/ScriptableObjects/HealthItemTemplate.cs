using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Health Items")]
public class HealthItemTemplate : ScriptableObject
{
    public int HealthUp;

    public void HealthBuff(GameObject target)
    {
        target.GetComponent<PlayerHealth>().maxHealth += HealthUp;
        target.GetComponent<PlayerHealth>().healthBar
        .SetNewMaxHealth(target.GetComponent<PlayerHealth>().maxHealth, target.GetComponent<PlayerHealth>().currentHealth);
    }
}
