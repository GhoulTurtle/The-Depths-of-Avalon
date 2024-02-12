using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider HealthSlider;
    //public HealthSystem Ref;

    void Start()
    {
        //PlayerManager.Instance.OnPlayerJoined += 
    }

    public void SetMaxHealth(int maxHealth)
    {
        //Slider.maxValue = maxHealth;
        //Slider.value = Ref.currentHealth;
    }

    public void SetCurrentHealth(int currentHealth)
    {
        //Slider.value = currentHealth;
    }
}
