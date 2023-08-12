using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    [SerializeField] private Vector3 Offset;
    [SerializeField] private Transform target;
    [SerializeField] private float translationspeed;
    [SerializeField] private float rotationspeed;

    private void FixedUpdate()
    {
        HandleTranslation();
        HandleRot();
    }

    public void HandleTranslation()
    {
        Vector3 targetPosition = target.TransformPoint(Offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translationspeed * Time.deltaTime);

    }

    private void HandleRot()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationspeed * Time.deltaTime);
    }


}
