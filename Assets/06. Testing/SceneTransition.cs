using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    private void Start()
    {
        StartCoroutine(LockMovement());
    }

    private IEnumerator LockMovement()
    {
        playerMovement.enabled = false;
        yield return new WaitForSeconds(1f);
        playerMovement.enabled = true;
    }
}
