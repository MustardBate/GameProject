using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ReloadIndicatorUI : MonoBehaviour
{
    private float currentTime = 1f;
    private readonly float reloadTime = 1f;
    [SerializeField] private Image indicator;
    private bool isReloading;
    

    void Start()
    {
        indicator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        isReloading = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<PlayerGun>().isReloading;

        if (isReloading == true)
        {
            indicator.enabled = true;
            currentTime -= Time.deltaTime;
            indicator.fillAmount = currentTime;

            if (currentTime <= .1f)
            {
                currentTime = reloadTime;
                indicator.fillAmount = currentTime;
                indicator.enabled = false;
            }
        }

        else indicator.enabled = false;
    }
}
