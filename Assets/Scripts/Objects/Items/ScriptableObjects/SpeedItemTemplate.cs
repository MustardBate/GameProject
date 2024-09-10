using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Speed Items")]
public class SpeedItemTemplate : ScriptableObject
{
    public Sprite sprite;
    public float speedUp = .5f;
    
    [Range(1, 100)]
    public int dropChance;

    public void SpeedBuff(GameObject target)
    {
        target.GetComponent<PlayerMovement>().currentSpeed += speedUp;
    }
}
