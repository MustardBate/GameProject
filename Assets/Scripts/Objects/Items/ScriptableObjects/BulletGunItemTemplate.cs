using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Bullet and Gun Items")]
public class BulletGunItemTemplate : ScriptableObject
{
    public Sprite sprite;
    public float bulletSpeedUp = 2f;

    [Range(1, 100)]
    public int dropChance;

    public void BulletSpeedBuff(GameObject target)
    {
        target.GetComponentInChildren<PlayerGun>().bulletSpeed += bulletSpeedUp;
    }
}
