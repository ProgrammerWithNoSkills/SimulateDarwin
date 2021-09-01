using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public float curStamina = 0f;
    public float maxStamina = 100f;
    public StaminaBar staminaBar;

    void Start()
    {
        curStamina = maxStamina;
    }

    void Update()
    {

     /* test 
        if (Input.GetKeyDown(KeyCode.L))
        {
            ReducePlayerStamina(10f);
        } */

        staminaBar.UpdateStaminaBar();
    }

    public void ReducePlayerStamina(float staminaConsumption)
    {
        curStamina -= staminaConsumption;
    }

}


