using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTheCamera : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    void Update()
    {
        Quaternion cameraRotation = Camera.main.transform.rotation;
        canvas.transform.rotation = cameraRotation;
    }
}
