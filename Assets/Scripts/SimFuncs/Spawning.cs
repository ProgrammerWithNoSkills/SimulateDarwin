using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{

    public static int m_StartingNumOfCreatures;

    public static float m_xSpawnCoord;
    public static float m_ySpawnCoord;

    GameObject m_Creature;

    void Start()
    {
        m_Creature = GameObject.FindWithTag("Creature");
    }

    void Update()
    {
        
    }

}
