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

    public void SetNewMaxHealth(int newMaxHealth)
    {
        slider.maxValue = newMaxHealth;
        if (slider.value > newMaxHealth) slider.value = newMaxHealth;

        text.text = slider.value + "/" + slider.maxValue;
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
