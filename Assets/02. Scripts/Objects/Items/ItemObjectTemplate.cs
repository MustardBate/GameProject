using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemObject")]
public class ItemObjectTemplate : ScriptableObject
{
    [Header("Item Graphics")]
    public Sprite sprite;
    public new string name;
    public string itemDescription;
    [Space(10)]

    [Header("Item Properties")]
    [Header("Types")]
    public bool isHealthUp;
    public bool isDamageUp;
    public bool isSpeedUp;
    public bool isBulletSpeedUp;
    [Space(5)]

    // public ItemTypes itemType;
    public ItemRarity itemRarity;
    [Space(5)]

    [Header("Stats")]
    [Range(1, 100)]
    public int dropChance;

    public int HealthUp = 0;
    public float DamageUp = 0;
    public float DamageScale = 1;
    public float WalkingSpeedUp = 0;
    public float BulletSpeedUp = 0;
    

    // public enum ItemTypes
    // {
    //     Health,
    //     Damage,
    //     Speed,
    //     BulletSpeed
    // }

    public enum ItemRarity
    {
        Common,
        Uncommon,
        Rare,
        Legendary
    }

    public ItemObjectTemplate(string name)
    {
        this.name = name;
    }

    public void ApplyBuff(GameObject target)
    {
        if (isHealthUp)
        {
            target.GetComponent<PlayerHealth>().maxHealth += HealthUp;
            target.GetComponent<PlayerHealth>().healthBar
            .SetNewMaxHealth(target.GetComponent<PlayerHealth>().maxHealth, target.GetComponent<PlayerHealth>().currentHealth);
        }

        if (isDamageUp)
        {
            target.GetComponentInChildren<PlayerGun>().SetNewDamage(DamageUp, DamageScale);
            GameObject.FindGameObjectWithTag("StatsUI").GetComponent<StatsUIContainer>().SetDamageUI(target.GetComponentInChildren<PlayerGun>().currentDamage);
        }

        if (isSpeedUp)
        {
            target.GetComponent<PlayerMovement>().currentSpeed += WalkingSpeedUp;
            GameObject.FindGameObjectWithTag("StatsUI").GetComponent<StatsUIContainer>().SetSpeedUI(target.GetComponent<PlayerMovement>().currentSpeed);
        }

        if (isBulletSpeedUp)
        {
            target.GetComponentInChildren<PlayerGun>().bulletSpeed += BulletSpeedUp;
            GameObject.FindGameObjectWithTag("StatsUI").GetComponent<StatsUIContainer>().SetBulletSpeedUI(target.GetComponentInChildren<PlayerGun>().bulletSpeed);
        }

        else Debug.Log("Please select a valid item type");
    }
}
