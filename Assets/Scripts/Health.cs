using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float curHealth = 0f;
    public float maxHealth = 100f;
    public Healthbar healthBar;

    //player damage handling script 
    private DamagePlayer damagePlayer;

    private void Awake()
    {
        //attach player damage script to player
        damagePlayer = GameObject.Find("Player").AddComponent<DamagePlayer>();
    }

    void Start()
    {
        curHealth = maxHealth;
    }

    void Update()
    {

      //test
        if (Input.GetKeyDown(KeyCode.P))
        {
            curHealth = damagePlayer.dealPhysicalDamage(curHealth, 10);
        } 

        //update GUI health bar
        healthBar.UpdateHealthBar(); 

    }

}


 