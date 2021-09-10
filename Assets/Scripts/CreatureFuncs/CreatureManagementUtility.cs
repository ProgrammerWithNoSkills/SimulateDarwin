using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManagementUtility : CreatureManagement
{
    CreatureManagement creatureManagementObj = new CreatureManagement();

    float speed;
    private void Awake()
    {
        speed = creatureManagementObj.m_geneticMoveSpeed;
    }
}
