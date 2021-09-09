using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class DayManager : MonoBehaviour
{
    private static TMP_Text m_textComponent;
    private static int m_dayCount;
    public static bool m_isSimStarted;
    private static bool m_dayEnded;

    public static float m_timeSpeed;

    void Awake()
    {
        m_textComponent = GetComponent<TMP_Text>();
        m_textComponent.text = " Day 0";
        m_dayCount = 0;
        m_isSimStarted = false;
        m_dayEnded = true;
        m_timeSpeed = 3f;
    }

    private void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_isSimStarted == false)
            {
                BeginSim();
            }
            /*else if (m_dayEnded == false)
            {
                EndDay();
            }*/ //broken btw
        }

        //Debug.Log(GameObject.FindGameObjectsWithTag("Food"));

        if (m_dayEnded == false && GameObject.FindGameObjectsWithTag("Food").Length < 1)
        {
            EndDay();
        }

    }

    public void BeginSim()
    {
        m_dayCount++;
        m_textComponent.text = $" Day {m_dayCount}";
        m_isSimStarted = true;
        m_dayEnded = false;

        Spawning.SpawnFood(50);

    }

    public void EndDay()
    {
        //clear food if any are left (purely precautionary function)
        foreach (GameObject food in GameObject.FindGameObjectsWithTag("Food"))
        {
            if (food)
            {
                Destroy(food);
            } 
        }

        m_isSimStarted = false;
        m_dayEnded = true;

        //Find twice cause the creatures reproduce between this
        LiveOrDie(GameObject.FindGameObjectsWithTag("Creature"));
        //and this
        GameObject[] survivingCreatures = GameObject.FindGameObjectsWithTag("Creature");
        StartCoroutine(Spawning.EndOfDayTPCreaturesToEdge(GameObject.FindGameObjectsWithTag("Creature")));

        StartCoroutine(Reproduce(GameObject.FindGameObjectsWithTag("Creature")));
        StartCoroutine(UpdateFitness(GameObject.FindGameObjectsWithTag("Creature")));
        StartCoroutine(ExtractDataIntoJsonFile(GameObject.FindGameObjectsWithTag("Creature")));
    }
    
    //End of day functions to run.
    /*-------------------------------------*/
    void LiveOrDie(GameObject[] creatures)
    {
        foreach (GameObject creature in creatures)
        {
            Fitness creatureFitness = creature.GetComponent<Fitness>();

            //stop all movement
            Rigidbody m_rigidBody = creature.GetComponent<Rigidbody>();
            m_rigidBody.isKinematic = false; //turns off movement
            m_rigidBody.isKinematic = true; //turns on movement

            if (creatureFitness.foodcount < 1)
            {
                //Debug.Log($"I, {creature.GetComponent<SpeciesID>().UUID} die");
                creature.GetComponent<DieAnim>().isDead = true;
                continue;
            }
        }
    }

    IEnumerator UpdateFitness(GameObject[] creatures)
    {
        yield return new WaitForSeconds(1.2f / m_timeSpeed);

        foreach (GameObject creature in creatures)
        {
            Fitness creatureFitness = creature.GetComponent<Fitness>();
            creatureFitness.curFitnessScore += creatureFitness.foodcount + creatureFitness.offspring;
            creatureFitness.foodcount = 0;
        }
    }
   IEnumerator Reproduce(GameObject[] creatures)
    {
        yield return new WaitForSeconds(1f / m_timeSpeed);

        foreach (GameObject creature in creatures)
        {
            Fitness creatureFitness = creature.GetComponent<Fitness>();

            if (creatureFitness.foodcount > 1)
            {
                Vector3 spawnPos = new Vector3(0, 0, 0) + creature.transform.position;//place child to the side of parent NEED FIX TO OFFSET TOWARDS WORLD CENTER
                GameObject newCreature = Instantiate(Spawning.m_Creature, spawnPos, Quaternion.identity);//instantiate child

                CreatureManagement parentCreatureManagementComp = creature.GetComponent<CreatureManagement>();//get parent pathfinding ai component
                CreatureManagement childCreatureManagementComp = newCreature.GetComponent<CreatureManagement>();//get child pathfinding ai component

                MeshRenderer parentMeshRendererComp = creature.GetComponentInChildren(typeof(MeshRenderer)) as MeshRenderer;//get parent rendering component
                MeshRenderer childMeshRendererComp = newCreature.GetComponentInChildren(typeof(MeshRenderer)) as MeshRenderer;//get child rendering component

                /*------------- Inherit Traits from Parent And Mutate -----------*/
                //Set child movespeed to parent's move speed plus mutation offset
                childCreatureManagementComp.SetMoveSpeed(parentCreatureManagementComp.m_geneticMoveSpeed += Random.Range(-1f, 1f));

                //Set child sightrange to parent's sightrange plus mutation offset
                childCreatureManagementComp.SetSigtRange(parentCreatureManagementComp.m_geneticSightRange += Random.Range(-2f, 2f));

                //set child colour to parent colour
                if (parentMeshRendererComp != null && childMeshRendererComp != null)
                {
                    childMeshRendererComp.material.color = parentMeshRendererComp.material.color;
                }

                //set child mass to parent mass, plus offset mutation
                childCreatureManagementComp.SetGeneticMass(parentCreatureManagementComp.GetGeneticMass() + Random.Range(-20f, 20f)); //adjust RandomRange value to change mutation rates.
                /*----------------------------- End ----------------------------*/

                creatureFitness.offspring++;
            }
        }
    }
    /*-------------------------------------*/

    IEnumerator ExtractDataIntoJsonFile(GameObject[] creatures)
    {
        yield return new WaitForSeconds(1.5f / m_timeSpeed);

        List<string> speciesList = new List<string>();

        foreach (GameObject creature in creatures)
        {
            List<string> creatureList = new List<string>();

            CreatureManagement creatureManagementComp = creature.GetComponent<CreatureManagement>();

            string jsonCreatureSpecies = JsonUtility.ToJson(creatureManagementComp, true);
            Debug.Log(jsonCreatureSpecies);
            creatureList.Add(jsonCreatureSpecies);

            string creatureListString = string.Join(",", creatureList);
            speciesList.Add(creatureListString);
        }

        string speciesArrayString = JsonUtility.ToJson(string.Join(",", speciesList));

        //Writing into a JSON file in the persistent path
        using (FileStream fs = new FileStream(
                Path.Combine(Application.dataPath + "/Data", $"Day_{m_dayCount}" + ".json"),
                FileMode.Create))
        {
            BinaryWriter filewriter = new BinaryWriter(fs);
            Debug.Log(fs.Name);
            filewriter.Write(speciesArrayString);
            fs.Close();
        }
    }
}
