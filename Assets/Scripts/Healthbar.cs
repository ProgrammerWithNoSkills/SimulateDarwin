using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Healthbar : MonoBehaviour
{
    public Image healthBarImg;
    public Health playerHealth;
    void Start()
    { 
        DOTween.SetTweensCapacity(1250, 50);
    }
    public void UpdateHealthBar()
    {
        float duration = 0.75f * (playerHealth.curHealth / playerHealth.maxHealth);
        healthBarImg.DOFillAmount(playerHealth.curHealth / playerHealth.maxHealth, duration);

        Color newColor = Color.green;
        if (playerHealth.curHealth < playerHealth.maxHealth * 0.25f)
        {
            newColor = Color.red;
        }
        else if (playerHealth.curHealth < playerHealth.maxHealth * 0.65f)
        {
            newColor = new Color(255, 165, 0, 1f);
        }
        healthBarImg.DOColor(newColor, duration);
    }
}
