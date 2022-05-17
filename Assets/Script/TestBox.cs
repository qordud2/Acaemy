using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBox : MonoBehaviour
{
    float playtime = 0.0f;

    void OnCollisionEnter(Collision other)
    {
        //Destroy(gameObject);
    }

    void OnCollisionStay(Collision other)
    {

    }

    void OnCollisionExit(Collision other)
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEnter" + other.gameObject.name);
    }

    void OnTriggerStay(Collider other)
    {
        playtime += Time.deltaTime;
        if(playtime > 3.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("TriggerExit" + other.gameObject.name);
    }
}
