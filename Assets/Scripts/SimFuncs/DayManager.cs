using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class DayManager : MonoBehaviour
{
    private static TMP_Text m_textComponent;
    private static int m_dayCount;
    public static bool m_isSimStarted, m_dayCalculated;
    private static bool m_dayEnded, m_autoStartDay;

    public string m_toggleAutoRunSimKey;

    public static float m_timeSpeed; //edit this to change sim speed

    void Awake()
    {
        m_textComponent = GetComponent<TMP_Text>();
        m_textComponent.text = " Day 0";
        m_dayCount = 0;
        m_isSimStarted = false;
        m_dayCalculated = false;
        m_autoStartDay = false;
        m_dayEnded = true;
        m_toggleAutoRunSimKey = "p";
        m_timeSpeed = 5f;
    }

    private void Update()
    {
        //toggle auto running sim
        if (Input.GetKeyDown(m_toggleAutoRunSimKey))
        {
            m_autoStartDay = onButtonPressToggleAutoRunSim();
        }

        //autorun sim if it's supposed to and the day has already been calculated.
        if (m_autoStartDay && m_dayCalculated)
        {
            BeginSim();
            m_dayCalculated = false;
        }
    }

    void FixedUpdate()
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

        Spawning.SpawnFood(10);

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
        m_dayCalculated = false;

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
            CreatureManagement creatureManagementComp = creature.GetComponent<CreatureManagement>();

            //stop all movement
            Rigidbody m_rigidBody = creature.GetComponent<Rigidbody>();
            m_rigidBody.isKinematic = false; //turns off movement
            m_rigidBody.isKinematic = true; //turns on movement

            if (creatureManagementComp.GetFoodcount() < 1)
            {
                //Debug.Log($"I, {creature.GetComponent<SpeciesID>().UUID} die");
                creature.GetComponent<DieAnim>().isDead = true;
                continue;
            }
            //Debug.Log("LIVE OR DIE RAN");
        }
    }

    IEnumerator UpdateFitness(GameObject[] creatures)
    {
        yield return new WaitForSeconds(1.2f / m_timeSpeed);

        foreach (GameObject creature in creatures)
        {
            CreatureManagement creatureManagementComp = creature.GetComponent<CreatureManagement>();
            creatureManagementComp.m_curFitness = creatureManagementComp.GetFoodcount() + creatureManagementComp.GetOffspring();
            creatureManagementComp.SetFoodcount(0);
            //Debug.Log("UPDATE FITNESS RAN");
        }
    }
   IEnumerator Reproduce(GameObject[] creatures)
    {
        yield return new WaitForSeconds(1f / m_timeSpeed);

        foreach (GameObject creature in creatures)
        {
            CreatureManagement parentCreatureManagementComp = creature.GetComponent<CreatureManagement>();//get parent behaviour and genetics management component

            float growthLimit = Mathf.Abs(parentCreatureManagementComp.GetGeneticMoveSpeed()* 
                                          parentCreatureManagementComp.GetSightRange()* 
                                          parentCreatureManagementComp.GetGeneticMass()/1000);
            Debug.Log(growthLimit);

            if (parentCreatureManagementComp.GetFoodcount() >= Mathf.Abs(Mathf.Ceil(2f * growthLimit)))//reproduce with exponential food requirement based on speed
            {
                Vector3 spawnPos = new Vector3(0, 0, 0) + creature.transform.position;//place child to the side of parent NEED FIX TO OFFSET TOWARDS WORLD CENTER
                GameObject newCreature = Instantiate(Spawning.m_Creature, spawnPos, Quaternion.identity);//instantiate child

                CreatureManagement childCreatureManagementComp = newCreature.GetComponent<CreatureManagement>();//get child behaviour and genetics management component

                MeshRenderer parentMeshRendererComp = creature.GetComponentInChildren(typeof(MeshRenderer)) as MeshRenderer;//get parent rendering component
                MeshRenderer childMeshRendererComp = newCreature.GetComponentInChildren(typeof(MeshRenderer)) as MeshRenderer;//get child rendering component

                /*------------- Inherit Traits from Parent And Mutate -----------*/
                //Set child movespeed to parent's move speed plus mutation offset
                float tryInheritMoveSpeedValue =
                    parentCreatureManagementComp.m_geneticMoveSpeed +=
                    Random.Range(-1f * parentCreatureManagementComp.m_mutationRate, 1f * parentCreatureManagementComp.m_mutationRate);
                //unrealistic, basically try to prevent move speed from going negative
                if (tryInheritMoveSpeedValue < 5f) tryInheritMoveSpeedValue = 5f;
                childCreatureManagementComp.SetMoveSpeed(tryInheritMoveSpeedValue);

                //Set child sightrange to parent's sightrange plus mutation offset
                childCreatureManagementComp.SetSightRange(parentCreatureManagementComp.m_geneticSightRange += 
                    Random.Range(-2f * parentCreatureManagementComp.m_mutationRate, 2f * parentCreatureManagementComp.m_mutationRate));

                //set child colour to parent colour
                if (parentMeshRendererComp != null && childMeshRendererComp != null)
                {
                    childMeshRendererComp.material.color = parentMeshRendererComp.material.color;
                }

                //set child mass to parent mass, plus offset mutation
                childCreatureManagementComp.SetGeneticMass(
                    parentCreatureManagementComp.GetGeneticMass() + 
                    Random.Range(-20f * parentCreatureManagementComp.m_mutationRate, 20f * parentCreatureManagementComp.m_mutationRate));
                    //adjust RandomRange value to change mutation rates.

                //inherit mutation rate from parent and mutate the mutation rate by multiplying it by itself!!!!!
                childCreatureManagementComp.SetMutationRate(
                    parentCreatureManagementComp.GetMutationRate() + 
                    Random.Range(-1f * parentCreatureManagementComp.m_mutationRate, 1f * parentCreatureManagementComp.m_mutationRate)
                    );
                /*----------------------------- End ----------------------------*/

                parentCreatureManagementComp.AddToOffspring(1);
            }
            //Debug.Log("REPRODUCE RAN");
        }
    }
    /*-------------------------------------*/

    IEnumerator ExtractDataIntoJsonFile(GameObject[] creatures)
    {
        yield return new WaitForSeconds(1.5f / m_timeSpeed);

        List<string> speciesList = new List<string>();

        foreach (GameObject creature in creatures)
        {
            CreatureManagement creatureManagementComp = creature.GetComponent<CreatureManagement>();

            string jsonCreatureSpecies = JsonUtility.ToJson(creatureManagementComp, true).ToString();

            //wrap values in creatureGameObjectUUID
            //string wrappedJsonCreatureSpecies = $"\"{creature.GetInstanceID().ToString()}\" : "  + jsonCreatureSpecies;

            speciesList.Add(jsonCreatureSpecies);
            //Debug.Log("EXTRACT DATA RAN");
        }

        string speciesArrayString = string.Join(",\n", speciesList);

        //wrap data in Day and array to prime for writing
        string wrappedSpeciesArrayString = "{\n" + $"\"Day_{m_dayCount}\"\n : " + "[\n" + speciesArrayString + "\n]" + "\n}";

        //lint: cannot use FromJson. Can use FromJsonOverwrite but that would overwrite the values that have already changed so yeaahh
        //CreatureManagement prelintedSpeciesArrayString = JsonUtility.FromJson<CreatureManagement>(wrappedSpeciesArrayString);
        //string lintedSpeciesArrayString = JsonUtility.ToJson(prelintedSpeciesArrayString, true);

        //Writing into a JSON file in Data folder. (this folder is in gitignore)
        File.WriteAllText(Application.dataPath + "/Data" + $"/Day_{m_dayCount}" + ".json", wrappedSpeciesArrayString);

        m_dayCalculated = true;
    }

    bool onButtonPressToggleAutoRunSim()
    {
            if (m_autoStartDay) return false;
            return true;
    }
}
