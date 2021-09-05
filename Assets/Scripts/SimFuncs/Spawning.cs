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

        SpawnFood(17);

        //test spawning
        if (m_Creature)
        {
            SpawnAlongXLine(2, 25f, 25f);
            SpawnAlongNegXLine(1, 25f, 25f);
            SpawnAlongZLine(3, 25f, 25f);
            SpawnAlongNegZLine(2, 25f, 25f);
        }
    }

    void Update()
    {
        
    }

    //Spawn Food Funcs
    /*--------------------------------------------------*/
    void SpawnFood(int numToSpawn)
    {
        for (int i = 1; i <= numToSpawn; i++)
        {
            MeshRenderer foodObjMeshRenderer = Instantiate(m_Food, new Vector3(Random.Range(-15f, 15f), 0f, Random.Range(-15f, 15f)), Quaternion.identity).GetComponent<MeshRenderer>();
            ChangeMaterialColour(foodObjMeshRenderer);
        }
    }

    void ChangeMaterialColour(MeshRenderer renderComponent)
    {
        Color color = new Color(Random.Range(0f, 0.1f), Random.Range(0.3f, 1f), Random.Range(0f, 0.1f), Random.Range(0.3f, 1f));
        renderComponent.material.color = color;
    }
    /*--------------------------------------------------*/


    //Spawn Creature Funcs
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

    public static void EndOfDayTPCreaturesToEdge()
    {

    }
}
