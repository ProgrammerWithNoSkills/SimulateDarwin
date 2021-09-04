using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    //public GameObject food;
    //public int foodcount;

    public void OnCollisionEnter()
    {
        Destroy(this.gameObject);
    }
}
