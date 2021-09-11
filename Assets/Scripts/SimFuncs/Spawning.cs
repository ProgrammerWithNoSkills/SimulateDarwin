using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{

    public static int m_StartingNumOfCreatures;

    public static float m_xSpawnCoord, m_ySpawnCoord, m_foodDistanceFromCentre;

    public static GameObject m_Creature, m_Food;

    void Start()
    {
        m_Creature = Resources.Load("Prefabs/Creature") as GameObject;
        m_Food = Resources.Load("Prefabs/Food") as GameObject;

        m_foodDistanceFromCentre = 23f;

        //test spawning
        if (m_Creature)
        {
            SpawnAlongXLine(1, 25f, 25f);
            SpawnAlongNegXLine(0, 25f, 25f);
            SpawnAlongZLine(1, 25f, 25f);
            SpawnAlongNegZLine(0, 25f, 25f);
        }
    }

    void Update()
    {
        
    }

    //Spawn Food Funcs
    /*--------------------------------------------------*/
    public static void SpawnFood(int numToSpawn)
    {
        for (int i = 1; i <= numToSpawn; i++)
        {
            MeshRenderer foodObjMeshRenderer = Instantiate(
                m_Food, 
                new Vector3(Random.Range(-m_foodDistanceFromCentre, m_foodDistanceFromCentre), 
                0.26f, 
                Random.Range(-m_foodDistanceFromCentre, m_foodDistanceFromCentre)), 
                Quaternion.identity)
                .GetComponent<MeshRenderer>();

            ChangeMaterialColour(foodObjMeshRenderer);
        }
    }

    static void ChangeMaterialColour(MeshRenderer renderComponent)
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

    //Respawning at End of Day
    static void RespawnAlongXLine(GameObject creature, float xBound, float zBound)
    {
        Vector3 spawnLoc = new Vector3(xBound, 1f, Random.Range(-zBound, zBound));

        creature.transform.rotation = Quaternion.Euler(0, 0, 90);
        creature.transform.position = spawnLoc;
    }
    static void RespawnAlongNegXLine(GameObject creature, float xBound, float zBound)
    {
        Vector3 spawnLoc = new Vector3(-xBound, 1f, Random.Range(-zBound, zBound));

        creature.transform.rotation = Quaternion.Euler(0, 0, 90);
        creature.transform.position = spawnLoc;
    }

    static void RespawnAlongZLine(GameObject creature, float xBound, float zBound)
    {
        Vector3 spawnLoc = new Vector3(Random.Range(-xBound, xBound), 1f, zBound);

        creature.transform.rotation = Quaternion.Euler(0, 0, 90);
        creature.transform.position = spawnLoc;
    }

    static void RespawnAlongNegZLine(GameObject creature, float xBound, float zBound)
    {
        Vector3 spawnLoc = new Vector3(Random.Range(-xBound, xBound), 1f, -zBound);

        creature.transform.rotation = Quaternion.Euler(0, 0, 90);
        creature.transform.position = spawnLoc;
    }
    /*-----------------------------------------------------*/
    //End Spawn Funcs

    public static IEnumerator EndOfDayTPCreaturesToEdge(GameObject[] creatures)
    {
        yield return new WaitForSeconds(0.1f);

        //reset locations to edge of map
        for (int i = 0; i < creatures.Length; i++)
        {

            if (creatures[i].GetComponent<DieAnim>().isDead) continue; //If creature is dead don't tp them to edge

            int spawnSide = Random.Range(1, 5);
            //Debug.Log(spawnSide);
            switch (spawnSide)
            {
                case 1:
                    RespawnAlongXLine(creatures[i], 25f, 25f);
                    break;
                case 2:
                    RespawnAlongNegXLine(creatures[i], 25f, 25f);
                    break;
                case 3:
                    RespawnAlongZLine(creatures[i], 25f, 25f);
                    break;
                case 4:
                    RespawnAlongNegZLine(creatures[i], 25, 25);
                    break;
                default:
                    Debug.Log($"Error Resetting location of {creatures[i]}");
                    break;
            }

        }

    }

}
