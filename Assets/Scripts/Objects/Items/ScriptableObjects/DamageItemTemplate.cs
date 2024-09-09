using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Damage Items")]
public class DamageItemTemplate : ScriptableObject
{
    public int damageUp = 0;
    public float damageScale = 1;

    public void DamageBuff(GameObject target)
    {
        target.GetComponentInChildren<PlayerGun>().SetNewDamage(damageUp, damageScale);
    }
}
