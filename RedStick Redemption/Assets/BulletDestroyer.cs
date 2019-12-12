using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return StartCoroutine("Wait1SecBeforeDestroy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Wait1SecBeforeDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
    }
}
