using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CarController : MonoBehaviour
{
    public bool isInCar = false;

    [Header("Control and physics")]
    public  WheelCollider[] wheels;
    public float maxMotorPower = 200; //min 200  max 400
    public float baseMotorPower = 10; //min 10  max 50  - the power when there is no hill
    public float steerPower = 100;
    public float breakPower = 100;
    public float maxSpeed = 10f;
    public float motorPower = 100;// changable based on uphill
    public float trargetMotorPower;

    private float terrainAngle;
    private float maxAngle = 45f;

    public int direction = -1;

    private bool brakeIsOn = false;

    [SerializeField] private Transform [] wheelSprites;
    [SerializeField] private Transform car;
    [SerializeField] public Rigidbody car_rig;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject headlight;
    [SerializeField] private ParticleSystem gasParticles;
    [SerializeField] private Canvas canvasUI;

    private bool four_wheel_drive = false;
    private bool four_wheel_drive_low = false;
    private bool two_wheel_drive = false;

    [Header("Status and consumption")] 
    [SerializeField] private OpenHood hood;
    [SerializeField] private GameObject warningLight; //yellow
    [SerializeField] private GameObject errorLight; // red

    private float extraFuelConsumption  = 0;

    public float gasAmount; //liters
    public float oil; //% v
    public float water;

    [SerializeField , Range(0.5f,2.5f)]//0.5 to 2.5 || the more the better
    public float consumeRate = 0.5f;

    [Header("Parts and their health")]
    public int damageRate = 5;

    public bool hasFuelPump = false;
    public bool hasOilContainer = false;
    public bool hasCoolant= false;
    public bool hasBattery= false;

    [SerializeField] private int fuelPumpHealth = 100;
    [SerializeField] private int oilContainerHealth = 100;
    [SerializeField] private int coolantHealth = 100;

    public float batteryHealth;

    public float engineHealth;

    public bool playerIn = false;


    private void Awake()
    {
        canvasUI.enabled = false;
        gasParticles.Stop();
    }
    private void Start()
    {
        //StartCoroutine(Consume());
    }
    private void Update()
    {
        //gets the angle of the terrain in order to calculate grip and power
        RaycastHit hit;
        if(Physics.Raycast(transform.position,-transform.up,out hit))
        {
            terrainAngle = Vector3.Angle(hit.normal, Vector3.up);
        }

        EngineConditions(); //breaks engine when no consumatives
        IncreaseGasConsumptionRate();

        gasAmount = Mathf.Clamp(gasAmount, 0, 40);
        oil = Mathf.Clamp(oil, 0, 100);
        water = Mathf.Clamp(water, 0, 100);
        batteryHealth = Mathf.Clamp(batteryHealth, 0, 100);
        fuelPumpHealth = Mathf.Clamp(fuelPumpHealth, 0, 100);
        oilContainerHealth = Mathf.Clamp(oilContainerHealth, 0, 100);
        coolantHealth = Mathf.Clamp(coolantHealth, 0, 100);
        engineHealth = Mathf.Clamp(engineHealth, 0, 100); 
    }
    private void FixedUpdate()  //physics
    {
        //giving more power if angle is more steep to simulate grip
        trargetMotorPower = (terrainAngle / maxAngle * maxMotorPower)+baseMotorPower;
        motorPower = Mathf.Lerp(motorPower, trargetMotorPower,0.1f);

        if(car_rig.velocity.magnitude > 1)// does not allow to go more than  certain speed
        {
            motorPower = Mathf.Clamp(motorPower, 0, 200);
            wheels[0].brakeTorque = 100;
            wheels[1].brakeTorque = 100;
        }
        else
        {
            wheels[0].brakeTorque = 0;
            wheels[1].brakeTorque = 0;
        }
        

        //modes
        if(four_wheel_drive) FourWheelDrive();
        if(four_wheel_drive_low) FourWheelDriveLow();
        if(two_wheel_drive) TwoWheelsDrive();

        if(car_rig.velocity.magnitude > maxSpeed)
        {
            car_rig.velocity = Vector3.ClampMagnitude(car_rig.velocity, maxSpeed);
        }

        //steering
        Steer(direction);

        // brake on - off
        if (brakeIsOn)
        {
            wheels[0].brakeTorque = breakPower;
            wheels[1].brakeTorque = breakPower;
        }
        if(!brakeIsOn)
        {
            wheels[0].brakeTorque = 0;
            wheels[1].brakeTorque = 0;
        }

        //flip - improve when you add the ui, the flip will be with r but only when handbrake is on
        if (Input.GetKeyDown(KeyCode.R)&& brakeIsOn == true)
        {
            car.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * -1);
            headlight.transform.Rotate(new Vector3(0, 180, 0));
            direction = direction * -1;
            Debug.Log(car.localScale);
        }
        // spining the actual wheel models
        for (int i = 0; i < wheels.Length; i++)
        {
            UpdateWheel(wheels[i], wheelSprites[i]);
        }

        //exit car
        if (Input.GetKeyDown(KeyCode.E) && brakeIsOn == true)
        {
            ExitCar();
        }

    }
    private void Steer(int factor)//steering
    {
        foreach (WheelCollider wheel in wheels)
        {
            wheel.steerAngle = Input.GetAxis("Vertical") * (factor*steerPower / 4);
        }
    }
    private void UpdateWheel(WheelCollider col, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);
        trans.position = position;
        trans.rotation = rotation;
    }
    private void FourWheelDrive()
    {
        foreach (var wheel in wheels)
        {
            wheel.motorTorque = Input.GetAxis("Horizontal") * -motorPower/2;
        }
    }
    private void FourWheelDriveLow()
    {
        foreach (var wheel in wheels)
        {
            wheel.motorTorque = Input.GetAxis("Horizontal") * -motorPower;
        }
    }
    private void TwoWheelsDrive()
    {
        wheels[1].motorTorque = Input.GetAxis("Horizontal") * -motorPower;
    }
    private void ExitCar()
    {
        var offset = new Vector3(0.15f*-direction, 0f, 0f);
        var playerPosition = car.transform.position + car.transform.TransformDirection(offset);
        var player_go = Instantiate(player,playerPosition,Quaternion.Euler(0,0,0));
        var camera = Camera.main;
        camera.GetComponent<CameraFollow>().target = player_go.transform;
        playerIn = false;
        canvasUI.enabled = false;
        gasParticles.Stop();
        this.enabled = false;
    }

    private void EngineConditions()
    {
        engineHealth = (oil + water + batteryHealth + fuelPumpHealth + oilContainerHealth + coolantHealth) / 6;

        if (engineHealth < 15) hood.toMuchDamage = true;
        if (engineHealth > 15) hood.toMuchDamage = false;
        if (engineHealth < 35) warningLight.SetActive(true);
        if (engineHealth < 15) errorLight.SetActive(true);
        if (engineHealth > 15) errorLight.SetActive(false);
        if (engineHealth > 35) warningLight.SetActive(false);
        if (gasAmount == 0 || hasOilContainer == false || hasFuelPump == false || hasCoolant == false || hasBattery == false)
        {
            gasParticles.Stop();

            trargetMotorPower = 0;
            maxMotorPower = 0;
            motorPower = 0;
            baseMotorPower = 0;

            headlight.SetActive(false);
            warningLight.SetActive(false);
            errorLight.SetActive(false);
        }
        if (gasAmount < 10)
        {
            //start damaging fuel pump
        }
        if (gasAmount > 0 && gasParticles.isStopped) gasParticles.Play();
        if (oil == 0)
        {
            //TODO increase damage per time by 2
            //start damaging oil engine aka oil container
            motorPower = motorPower / 2;
        }
        if (oil < 15)
        {
            motorPower = motorPower / 1.5f;
        }
        if (water < 30)
        {
            //increase damage per time by 2
            //start damaging the coolant
        }
    }
    private void IncreaseGasConsumptionRate()
    {
        var dash = FindObjectOfType<Dashboard>();
        extraFuelConsumption = dash.rpm * 0.01f;
        
    }
    public IEnumerator Consume()
    {
        while (playerIn)
        {

            if(hasOilContainer) oil -= 0.2f;
            if(hasCoolant) water -= 0.3f ;
            if(hasFuelPump) gasAmount -= (0.01f + extraFuelConsumption); 

            yield return new WaitForSeconds(consumeRate);
        }
    }
    public IEnumerator Damage()
    {
        while (playerIn)
        {
            if (hasOilContainer && hasFuelPump && hasCoolant && hasBattery)
            {
                batteryHealth -= 0.1f;
                coolantHealth -= 3;
                fuelPumpHealth -= 1;
                oilContainerHealth -= 2;
                batteryHealth -= 0.1f;
            }

            yield return new WaitForSeconds(damageRate);
        }
    }
    public void SetForuWheelDrive()
    {
         four_wheel_drive = true;
         four_wheel_drive_low = false;
         two_wheel_drive = false;
    }
    public void SetForuWheelDriveLow()
    {
        four_wheel_drive = false;
        four_wheel_drive_low = true;
        two_wheel_drive = false;
    }
    public void SetTwoWheelDrive()
    {
        four_wheel_drive = false;
        four_wheel_drive_low = false;
        two_wheel_drive = true;
    }
    public void GearChange(float value)
    {
        if(value < 0.33)
        {
            SetTwoWheelDrive();
        }
        if(value > 0.33 && value < 0.66)
        {
            SetForuWheelDrive();
        }
        if(value > 0.66)
        {
            SetForuWheelDriveLow();
        }
    }
    public void SetBrake(bool on_off)
    {
            brakeIsOn = on_off;
    }
    public void SetHeadlights(bool on_off)
    {
        headlight.SetActive(on_off);
    }
}
