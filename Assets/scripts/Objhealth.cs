using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objhealth : MonoBehaviour
{
    public float objecthealth = 10000f;

    public void damage(float amount) {
        objecthealth -= amount;
        if(objecthealth <= 0f)
        {
            die();
        }
    }
    void die()
    {
        Destroy(gameObject);
    }
    
        
    
}
