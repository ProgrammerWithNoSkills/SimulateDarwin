using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayManager : MonoBehaviour
{
    private TMP_Text textComponent;
    void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        Debug.Log(textComponent.text);
    }

    void Update()
    {
        
    }
}
