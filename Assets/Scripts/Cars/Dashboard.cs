using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dashboard : MonoBehaviour
{
    [SerializeField] private CarController car;

    private const float MAX_SPEED_ANGLE = -273f;
    private const float MIN_SPEED_ANGLE = 7f;

    private const float MAX_RPM_ANGLE = -273f;
    private const float MIN_RPM_ANGLE = 15f;

    private const float MAX_FUEL_ANGLE = -273f;
    private const float MIN_FUEL_ANGLE = 273f;

    private const float MAX_OIL_ANGLE = -33.2f;
    private const float MIN_OIL_ANGLE = 105.4f;

    private const float MAX_WATER_ANGLE = 110.28f;
    private const float MIN_WATER_ANGLE = -54f;

    [SerializeField] private Transform speedArrow;
    [SerializeField] private Transform rpmArrow;
    [SerializeField] private Transform fuelArrow;
    [SerializeField] private Transform oilArrow;
    [SerializeField] private Transform waterArrow;

    [SerializeField] private int maxSpeedOnDash = 10;
    [SerializeField] private int maxRpmOnDash = 130;

     public float rpm = 0;
     private float speed;
     private float fuel;
     private float oil;
     private float water;

    private void Awake()
    {
        car = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        AccelerateVisualy();
        CalculateSpeed();
        CalculateRPM();
        CalculateFuel();
        CalculateOil();
        CalculateWater();
    }
    private void CalculateRPM()
    {
        //rpm = Mathf.Abs(car.wheels[1].motorTorque);
       /* rpm = Mathf.Lerp((rpm / car.trargetMotorPower) * 100, 100, Time.deltaTime * 10f);
        rpm = Mathf.Clamp(rpm, 0, 100);*/
        rpmArrow.eulerAngles = new Vector3(0, 0, GetRPMRotation(rpm, maxRpmOnDash));
    }
    private void CalculateSpeed()
    {
        speed = Mathf.Abs(car.car_rig.velocity.magnitude);
        speedArrow.eulerAngles = new Vector3(0,0, GetSpeedRotation(speed, maxSpeedOnDash));
    }
    private void CalculateFuel()
    {
        fuel = car.gasAmount;
        fuelArrow.eulerAngles = new Vector3(0, 0, GetFuelRotation(fuel, 100));
    }
    private void CalculateOil()
    {
        oil = car.oil;
        oilArrow.eulerAngles = new Vector3(0, 0, GetOilRotation(oil, 283));
    }
    private void CalculateWater()
    {
        water = car.water;
        waterArrow.eulerAngles = new Vector3(0,0,GetWaterRotation(water,491));
    }
    private float GetSpeedRotation(float speed, int maxSpeed)
    {
        float totalAngleSize = MIN_SPEED_ANGLE - MAX_SPEED_ANGLE;
        float speedNormalized = speed - maxSpeed;
        return (MIN_SPEED_ANGLE - speedNormalized * totalAngleSize)/10;
    }
    private float GetRPMRotation(float rpm, int maxRpm)
    {
        float totalAngleSize = MIN_RPM_ANGLE - MAX_RPM_ANGLE;
        float rpmNormalized = rpm - maxRpm;
        return (MIN_RPM_ANGLE - rpmNormalized * totalAngleSize) / 10;
    }
    private float GetFuelRotation(float gas, int maxGas)
    {
        float totalAngleSize = MIN_FUEL_ANGLE- MAX_FUEL_ANGLE;
        float fuelNormalized = gas - maxGas;
        return (MIN_FUEL_ANGLE - fuelNormalized * totalAngleSize) / 200;
    }
    private float GetOilRotation(float oil, int maxOil)
    {
        float totalAngleSize = MIN_OIL_ANGLE - MAX_OIL_ANGLE;
        float Normalized = oil - maxOil;
        return (MIN_OIL_ANGLE - Normalized * totalAngleSize) / -99; //number magic
    }
    private float GetWaterRotation(float water, int maxWater)
    {
        float totalAngleSize = MIN_WATER_ANGLE - MAX_WATER_ANGLE;
        float Normalized = water - maxWater;
        return (MIN_WATER_ANGLE - Normalized * totalAngleSize) /221;
    }
    private void AccelerateVisualy()
    {
        if(car.gasAmount > 0 && car.batteryHealth >0)
        {
            if (car.direction == -1)
            {
                if (Input.GetKey(KeyCode.D))
                {
                    if (car.car_rig.velocity.x > 0) // check if car is moving backwards
                    {
                        float increment = 5f;
                        rpm += increment * Time.deltaTime;
                    }
                    else
                    {
                        float deceleration = 10f;
                        rpm -= deceleration * Time.deltaTime;
                    }
                }
                if (Input.GetKey(KeyCode.A))
                {
                    if (car.car_rig.velocity.x < 0) // check if car is moving forward
                    {
                        float increment = 5f;
                        rpm += increment * Time.deltaTime;
                    }
                    else
                    {
                        float deceleration = 10f;
                        rpm -= deceleration * Time.deltaTime;
                    }
                }
                else
                {
                    float deceleration = 2f;
                    rpm -= deceleration * Time.deltaTime;
                }
            }
            if (car.direction == 1)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    if (car.car_rig.velocity.x < 0) // check if car is moving backwards
                    {
                        float increment = 5f;
                        rpm += increment * Time.deltaTime;
                    }
                    else
                    {
                        float deceleration = 10f;
                        rpm -= deceleration * Time.deltaTime;
                    }
                }
                if (Input.GetKey(KeyCode.D))
                {
                    if (car.car_rig.velocity.x > 0) // check if car is moving forward
                    {
                        float increment = 5f;
                        rpm += increment * Time.deltaTime;
                    }
                    else
                    {
                        float deceleration = 10f;
                        rpm -= deceleration * Time.deltaTime;
                    }
                }
                else
                {
                    float deceleration = 2f;
                    rpm -= deceleration * Time.deltaTime;
                }
            }
        }
        

        rpm = Mathf.Clamp(rpm, 0f, 10);
    }
}
