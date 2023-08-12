using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healing : MonoBehaviour
{
    
   
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        player_movement p = other.GetComponent<player_movement>();
        rifle r = other.GetComponent<rifle>();
        if (p != null)
        {
            Debug.Log("heal");
            p.currentphealth = p.phealth;
           

        }
    }
}
