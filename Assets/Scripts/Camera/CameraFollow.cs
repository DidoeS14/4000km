using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpead = 2f;
    public Transform target;
    [SerializeField] private float offset = 1f;
    [SerializeField]float maxDist = 1.4f;
    [SerializeField]float minDist = 0.4f;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x,target.position.y+offset/2,target.position.z-offset);
        transform.position = Vector3.Slerp(transform.position,newPos,followSpead*Time.deltaTime);

        offset -= Input.GetAxis("Mouse ScrollWheel");
        if(offset > maxDist) offset = maxDist;
        if(offset< minDist) offset = minDist;
    }
}
