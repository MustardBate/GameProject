using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class RangedEnemy : EnemyLogic
{
    new private void Start()
    {
        base.Start();
    }

    new private void Update()
    {
        base.Update();
    }

    new private void FixedUpdate()
    {
        base.ChasePlayer();
    }
}
