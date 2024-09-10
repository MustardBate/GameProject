using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Damage Items")]
public class DamageItemTemplate : ScriptableObject
{
    public Sprite sprite;
    public int damageUp = 0;
    public float damageScale = 1;
    
    [Range(1, 100)]
    public int dropChance;

    public void DamageBuff(GameObject target)
    {
        target.GetComponentInChildren<PlayerGun>().SetNewDamage(damageUp, damageScale);
    }
}
