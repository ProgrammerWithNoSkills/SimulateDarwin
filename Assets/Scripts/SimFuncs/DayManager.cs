using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayManager : MonoBehaviour
{
    private static TMP_Text m_textComponent;
    private static int m_dayCount;

    public static bool m_isSimStarted;

    private static bool m_dayEnded;

    void Awake()
    {
        m_textComponent = GetComponent<TMP_Text>();
        m_textComponent.text = " Day 0";
        m_dayCount = 0;
        m_isSimStarted = false;
        m_dayEnded = true;
    }

    private void Update()
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

        Spawning.SpawnFood(15);

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
        UpdateFitnessAndReproduceOrDie(GameObject.FindGameObjectsWithTag("Creature"));
        //and this
        StartCoroutine(Spawning.EndOfDayTPCreaturesToEdge(GameObject.FindGameObjectsWithTag("Creature")));
    }
    
    //End of day functions to run.
    /*-------------------------------------*/
    private bool UpdateFitnessAndReproduceOrDie(GameObject[] creatures)
    {
         foreach (GameObject creature in creatures)
        {
            Fitness creatureFitness = creature.GetComponent<Fitness>();

            creature.GetComponent<PathfindingAI>().UpdateMoveSpeed(0f);//update current creature speed to 0 aka freeze them

            if (creatureFitness.foodcount < 1)
            {
                //Debug.Log($"I, {creature.GetComponent<SpeciesID>().UUID} die");
                creature.GetComponent<DieAnim>().isDead = true;
                continue;
            }
            else if (creatureFitness.foodcount > 1)
            {
                Vector3 spawnPos = new Vector3(0, 3, 0) + creature.transform.position;//place child to the side of parent
                GameObject newCreature = Instantiate(Spawning.m_Creature, spawnPos, Quaternion.identity);//instantiate child

                PathfindingAI parentPathfindingAIComp = creature.GetComponent<PathfindingAI>();//get parent pathfinding ai component

                /*------------- Inherit Traits from Parent -----------*/
                newCreature.GetComponent<PathfindingAI>().SetMoveSpeed(parentPathfindingAIComp.m_moveSpeed);//Set child movespeed to parent's move speed
                /*------------------------ End -----------------------*/

                creatureFitness.offspring++;
            }

            creatureFitness.curFitnessScore += creatureFitness.foodcount + creatureFitness.offspring;
            creatureFitness.foodcount = 0;
        }
        return true;
    }
    /*-------------------------------------*/
}
