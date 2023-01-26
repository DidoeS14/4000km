using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCar : MonoBehaviour
{
    [SerializeField]private Camera camera;
    [SerializeField]private GameObject car;
    [SerializeField]private Canvas canvasUI;
    [SerializeField]private ParticleSystem gasParticles;
    void Start()
    {
        camera= Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Input.GetKey(KeyCode.E))
            {
                camera.GetComponent<CameraFollow>().target = car.transform;
                car.GetComponent<CarController>().enabled = true;

                CarController car_controller = FindObjectOfType<CarController>();

                car_controller.playerIn = true;
                car_controller.StartCoroutine(car_controller.Consume());
                car_controller.StartCoroutine(car_controller.Damage());

                Destroy(GameObject.FindGameObjectWithTag("Player"));
                canvasUI.enabled = true;
                gasParticles.Play();

            }
        }
    }
}
