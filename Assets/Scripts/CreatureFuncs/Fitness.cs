using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fitness : MonoBehaviour
{
    public int foodcount;
    public int offspring;
    public int fitness; //end of day fitness(used for total species fitness)
    public int curFitnessScore; //fitness during day(readability and seperate individual score)

    private bool canReproduce;

    private void Awake()
    {
        foodcount = 0;
        offspring = 0;
        curFitnessScore = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            this.foodcount++;
        }
    }
}
