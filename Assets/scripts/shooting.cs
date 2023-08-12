using UnityEngine;
using System.Collections;

public class shooting : MonoBehaviour
{

    public Rigidbody projectile;
    public Transform shootarea;
    public float speed = 80;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody instantiatedProjectile = Instantiate(projectile,
                                                           shootarea.transform.position,
                                                           shootarea.transform.rotation)
                as Rigidbody;
            Debug.Log("fire");

            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
            Destroy(instantiatedProjectile, 3f);

        }
    }
}