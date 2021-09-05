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

        GameObject[] creatures = GameObject.FindGameObjectsWithTag("Creature");

        UpdateFitnessAndReproduceOrDie(creatures);
        Spawning.EndOfDayTPCreaturesToEdge(creatures);
    }
    
    //End of day functions to run.
    /*-------------------------------------*/
    private bool UpdateFitnessAndReproduceOrDie(GameObject[] creatures)
    {
         foreach (GameObject creature in creatures)
        {
            Fitness creatureFitness = creature.GetComponent<Fitness>();

            if (creatureFitness.foodcount < 1)
            {
                Debug.Log($"I, {creature.GetComponent<SpeciesID>().UUID} die");
                creature.GetComponent<DieAnim>().isDead = true;
                Destroy(creature);
                continue;
            }

            creatureFitness.curFitnessScore += creatureFitness.foodcount + creatureFitness.offspring;
            creatureFitness.foodcount = 0;
        }
        return true;
    }
    /*-------------------------------------*/
}
