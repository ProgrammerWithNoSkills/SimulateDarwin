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

        //test spawning
        SpawnAlongXLine(4, 25f, 25f);
        SpawnAlongNegXLine(4, 25f, 25f);
        SpawnAlongZLine(4, 25f, 25f);
        SpawnAlongNegZLine(6, 25f, 25f);
    }

    void Update()
    {
        
    }

    public void EndOfDayClearCreatures()
    {
        
    }

    //Spawn Funcs
    //Eulers to be corrected when creature models complete
    //1f y value because we're spawning on a plane and want the creatures to fall down onto it.
    /*--------------------------------------------------*/
    public void SpawnAlongXLine(int numToSpawn, float xBound, float zBound)
    {
        for (int i = 1; i <= numToSpawn; i++)
        {
            Vector3 spawnLoc = new Vector3(xBound, 1f, Random.Range(-zBound, zBound));

            Instantiate(m_Creature, spawnLoc, Quaternion.Euler(0, 0, 90));
        }
    }
    public void SpawnAlongNegXLine(int numToSpawn, float xBound, float zBound)
    {
        for (int i = 1; i <= numToSpawn; i++)
        {
            Vector3 spawnLoc = new Vector3(-xBound, 1f, Random.Range(-zBound, zBound));

            Instantiate(m_Creature, spawnLoc, Quaternion.Euler(0, 0, 90));
        }
    }
    public void SpawnAlongZLine(int numToSpawn, float xBound, float zBound)
    {
        for (int i = 1; i <= numToSpawn; i++)
        {
            Vector3 spawnLoc = new Vector3(Random.Range(-xBound, xBound), 1f, zBound);

            Instantiate(m_Creature, spawnLoc, Quaternion.Euler(0, 0, 90));
        }
    }
    public void SpawnAlongNegZLine(int numToSpawn, float xBound, float zBound)
    {
        for (int i = 1; i <= numToSpawn; i++)
        {
            Vector3 spawnLoc = new Vector3(Random.Range(-xBound, xBound), 1f, -zBound);

            Instantiate(m_Creature, spawnLoc, Quaternion.Euler(0, 0, 90));
        }
    }
    /*-----------------------------------------------------*/
    //End Spawn Funcs
}
