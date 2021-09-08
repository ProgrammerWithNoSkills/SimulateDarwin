using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeciesColour : MonoBehaviour
{
    public GameObject creatureBody;
    void Awake()
    {
        creatureBody = this.gameObject;
        creatureBody.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
    }
}