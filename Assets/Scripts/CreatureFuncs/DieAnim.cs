using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAnim : MonoBehaviour
{
    public bool isDead;

    float scale = 1f;
    float minScale = 0.5f;
    float scaleSpeed = 0.008f;

    private void Awake()
    {
        isDead = false;
    }
    void FixedUpdate()
    {
        if (isDead)
        {
           //StartCoroutine(deathAnimation());
        }
    }
    
    IEnumerator deathAnimation()
    {
        for (float i = 0f; i > minScale; i += scaleSpeed)
        {
            scale -= scaleSpeed * Time.deltaTime;

            this.gameObject.transform.localScale = new Vector3(scale, scale, scale);
            if (scale < minScale)
            {
                Destroy(this.gameObject);
                yield return null;
                break;
            }
            yield return null;
        }
    }
}
