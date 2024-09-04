using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class RangedEnemy : EnemyManager
{
    new private void Start()
    {
        base.Start();
    }

    new private void FixedUpdate()
    {
        base.ChasePlayer();
    }
}
