using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Speed Items")]
public class SpeedItemTemplate : ScriptableObject
{
    public float speedUp = .5f;

    public void SpeedBuff(GameObject target)
    {
        target.GetComponent<PlayerMovement>().currentSpeed += speedUp;
    }
}
