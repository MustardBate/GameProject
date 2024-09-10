using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemObject")]
public class ItemObjectTemplate : ScriptableObject
{
    public Sprite sprite;
    public string itemName, itemDescription;
    public int HealthUp = 0;

    public int DamageUp = 0;
    public float DamageScale = 1;

    public float WalkingSpeedUp = 0;
    public float BulletSpeedUp = 0;
    
    [Range(1, 100)]
    public int dropChance;

    public ItemTypes itemType;
    public enum ItemTypes
    {
        Health,
        Damage,
        Speed,
        BulletSpeed
    }


    public void ApplyBuff(GameObject target)
    {
        if (itemType == ItemTypes.Health)
        {
            target.GetComponent<PlayerHealth>().maxHealth += HealthUp;
            target.GetComponent<PlayerHealth>().healthBar
            .SetNewMaxHealth(target.GetComponent<PlayerHealth>().maxHealth, target.GetComponent<PlayerHealth>().currentHealth);
        }

        else if (itemType == ItemTypes.Damage)
        {
            target.GetComponentInChildren<PlayerGun>().SetNewDamage(DamageUp, DamageScale);
        }

        else if (itemType == ItemTypes.Speed)
        {
            target.GetComponent<PlayerMovement>().currentSpeed += WalkingSpeedUp;
        }

        else if (itemType == ItemTypes.BulletSpeed)
        {
            target.GetComponentInChildren<PlayerGun>().bulletSpeed += BulletSpeedUp;
        }

        else Debug.Log("Please select a valid item type");
    }
}
