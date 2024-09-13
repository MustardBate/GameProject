using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;
    

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth; 
    }

    public void SetNewMaxHealth(int newMaxHealth, int currentHealth)
    {
        slider.maxValue = newMaxHealth;
        slider.value = currentHealth;
        if (slider.value > newMaxHealth) slider.value = newMaxHealth;

        text.text = slider.value.ToString("00") + "/" + slider.maxValue.ToString("00");
    }


    public void SetHealth(int health)
    {
        slider.value = health;
    }

    
    public void SetHealthNumber(int maxHealth, int health)
    {
        text.text = health.ToString("00") + "/" + maxHealth.ToString("00");
    }
}
