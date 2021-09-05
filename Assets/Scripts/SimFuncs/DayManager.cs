using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayManager : MonoBehaviour
{
    private static TMP_Text m_textComponent;
    private static int m_dayCount;

    private static bool m_simStarted = false;
    private static bool m_dayEnded = false;

    void Awake()
    {
        m_textComponent = GetComponent<TMP_Text>();
        m_dayCount = 0;
    }

    private void Update()
    {

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_simStarted == false)
            {
                BeginSim();
            }
            else if (m_dayEnded == false)
            {
                EndDay();
            }
            else
            {
                BeginNextDay();
            }
        }*/

        if (GameObject.FindGameObjectsWithTag("Food").Length < 1)
        {
            EndDay();
        }

    }

    public void BeginSim()
    {
        m_dayCount = 1;
        m_textComponent.text = $" Day {m_dayCount}";
        m_simStarted = true;
    }

    public bool EndDay()
    {

        Spawning.EndOfDayTPCreaturesToEdge();

        m_dayCount += 1;
        m_textComponent.text = $" Day {m_dayCount}";
        m_dayEnded = true;
        return true;
    }

    public bool BeginNextDay()
    {
        m_dayCount += 1;
        m_textComponent.text = $" Day {m_dayCount}";
        m_dayEnded = false;
        return true;
    }

    
}
