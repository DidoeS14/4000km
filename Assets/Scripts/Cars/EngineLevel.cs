using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineLevel : MonoBehaviour
{
    [SerializeField, Range(1, 5)] public int engineLevel = 1;
    [SerializeField] CarController carController;

    // Update is called once per frame
    private void Awake()
    {
        carController= GetComponent<CarController>();
    }
    void Update()
    {
        switch (engineLevel)
        {
            case 1:
                carController.baseMotorPower = 10;
                carController.maxMotorPower = 200;
                break;
            case 2:
                carController.baseMotorPower = 20;
                carController.maxMotorPower = 225;
                break;
            case 3:
                carController.baseMotorPower = 30;
                carController.maxMotorPower = 250;
                break;
            case 4:
                carController.baseMotorPower = 40;
                carController.maxMotorPower = 300;
                break;
            case 5:
                carController.baseMotorPower = 50;
                carController.maxMotorPower = 325;
                break;
        }
    }
}
