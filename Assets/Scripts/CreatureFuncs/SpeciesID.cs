using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeciesID : MonoBehaviour
{
    public int UUID;
    void Awake()
    {
        UUID = Random.Range(int.MinValue, int.MaxValue);
        Debug.Log(UUID);
    }
}