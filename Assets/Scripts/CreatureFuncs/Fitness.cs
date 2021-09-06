using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fitness : MonoBehaviour
{
    public int foodcount;
    public int offspring;
    public int fitness;
    public int curFitnessScore;

    private bool canReproduce;

    private void Awake()
    {
        foodcount = 0;
        offspring = 0;
        curFitnessScore = 0;
    }

    private void Update()
    {
        //UpdateFitness();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            foodcount++;
        }
    }

}
