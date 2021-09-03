using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseFood : MonoBehaviour
{
    public GameObject foodPrefab;

    void Start()
    {
        Instantiate(foodPrefab, new Vector3(8, 1, 8), Quaternion.identity);
    }
}
