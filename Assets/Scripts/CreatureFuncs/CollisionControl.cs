using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("I have touched thee");
        if (collision.gameObject.tag == "Food")
        {
            Debug.Log("The food has been eaten");
            this.transform.parent.gameObject.GetComponent<Fitness>().foodcount++;
        }
    }
}
