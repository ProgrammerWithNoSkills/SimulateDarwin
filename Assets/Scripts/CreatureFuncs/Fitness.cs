using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fitness : MonoBehaviour
{
    public int foodcount;
    public int offspring;
    public int fitness;

    private void Awake()
    {
        foodcount = 0;
        offspring = 1;
    }

    private void Update()
    {
        UpdateFitness();
    }

    void UpdateFitness()
    {
        fitness = foodcount * offspring;
    }
}
