using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;
    

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth; 
    }


    public void SetHealth(int health)
    {
        slider.value = health;
    }

    
    public void SetHealthNumber(int maxHealth, int health)
    {
        text.text = health + "/" + maxHealth;
    }
}
