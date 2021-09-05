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
        m_dayEnded = false;
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
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Food"))
        {
            if (i)
            {
                Destroy(i);
            } 
        }

        m_isSimStarted = false;
        m_dayEnded = true;

        Spawning.EndOfDayTPCreaturesToEdge();
    }
    
}
