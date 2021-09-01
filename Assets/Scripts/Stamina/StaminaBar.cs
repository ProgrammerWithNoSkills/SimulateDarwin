using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Image staminaBarImg;
    public Stamina stamina;

    public void UpdateStaminaBar()
    {
        if (staminaBarImg && stamina)
        {
            staminaBarImg.fillAmount = Mathf.Clamp(stamina.curStamina / stamina.maxStamina, 0f, 1f);
        }
    }
}

