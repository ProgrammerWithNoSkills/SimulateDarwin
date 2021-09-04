using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{

    public static int m_StartingNumOfCreatures;

    public static float m_xSpawnCoord, m_ySpawnCoord;

    GameObject m_Creature, m_Food;

    void Start()
    {
        m_Creature = Resources.Load("Prefabs/Creature") as GameObject;
        m_Food = Resources.Load("Prefabs/Food") as GameObject;

        Instantiate(m_Food, new Vector3(0, 0, 0), Quaternion.identity);

        //test spawning
        if (m_Creature)
        {
            SpawnAlongXLine(1, 25f, 25f);
            SpawnAlongNegXLine(1, 25f, 25f);
            SpawnAlongZLine(1, 25f, 25f);
            SpawnAlongNegZLine(1, 25f, 25f);
        }
    }

    void Update()
    {
        
    }

    void SpawnFood(int numToSpawn, Vector3 spawnArea)
    {
        for (int i = 1; i <= numToSpawn; i++)
        {

        }
    }

    //Spawn Funcs
    //Eulers to be corrected when creature models complete
    //1f y value because we're spawning on a plane and want the creatures to fall down onto it.
    /*--------------------------------------------------*/
    void SpawnAlongXLine(int numToSpawn, float xBound, float zBound)
    {
        for (int i = 1; i <= numToSpawn; i++)
        {
            Vector3 spawnLoc = new Vector3(xBound, 1f, Random.Range(-zBound, zBound));

            Instantiate(m_Creature, spawnLoc, Quaternion.Euler(0, 0, 90));
        }
    }
    void SpawnAlongNegXLine(int numToSpawn, float xBound, float zBound)
    {
        for (int i = 1; i <= numToSpawn; i++)
        {
            Vector3 spawnLoc = new Vector3(-xBound, 1f, Random.Range(-zBound, zBound));

            Instantiate(m_Creature, spawnLoc, Quaternion.Euler(0, 0, 90));
        }
    }
    void SpawnAlongZLine(int numToSpawn, float xBound, float zBound)
    {
        for (int i = 1; i <= numToSpawn; i++)
        {
            Vector3 spawnLoc = new Vector3(Random.Range(-xBound, xBound), 1f, zBound);

            Instantiate(m_Creature, spawnLoc, Quaternion.Euler(0, 0, 90));
        }
    }
    void SpawnAlongNegZLine(int numToSpawn, float xBound, float zBound)
    {
        for (int i = 1; i <= numToSpawn; i++)
        {
            Vector3 spawnLoc = new Vector3(Random.Range(-xBound, xBound), 1f, -zBound);

            Instantiate(m_Creature, spawnLoc, Quaternion.Euler(0, 0, 90));
        }
    }
    /*-----------------------------------------------------*/
    //End Spawn Funcs

    void EndOfDayClearCreatures()
    {

    }
}
