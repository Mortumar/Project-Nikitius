using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float thrust = 1f;
    [SerializeField] float rotationFactor = 10f;
    [SerializeField] float fullBoostCapacity = 2f;
    [SerializeField] float currentBoostCapacity;
    [SerializeField] float boostSpeed = 2f;
    
    [SerializeField] float boostСonsumption = 2f;
    [SerializeField] float boostRecoveryRate = 1f;

    [SerializeField] GameObject earthStrike;

    Rigidbody rigidBody;
    
    

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        currentBoostCapacity = fullBoostCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        ApplyRotation();
        ApplyThrust();
        ApplyBoost();
        BoostRecovery();
        ApplyEarthStrike();

    }
      private void ApplyRotation()
     {
        if (Input.GetKey(KeyCode.A))
         {
             transform.Rotate(Vector3.up, rotationFactor);
         }
         else if (Input.GetKey(KeyCode.D))
         {
             transform.Rotate(Vector3.down, rotationFactor);
         }
     }
    /*  private void ApplyRotation()
   {
       float zRotation = CrossPlatformInputManager.GetAxis("Horizontal");
       float zOffset = zRotation * rotationFactor * Time.deltaTime;
       float newZRotation = transform.rotation.z + zOffset;

      Quaternion endZRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, newZRotation);
      Quaternion startZRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
       if (newZRotation > 0 || newZRotation < 0 )
      transform.rotation = Quaternion.Lerp(startZRotation, endZRotation, rotationFactor * Time.deltaTime);
       else { return; }
      transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, newZRotation);
  } */

    private void ApplyThrust()
    {
        if (CrossPlatformInputManager.GetButton("Jump"))
        {
           transform.Translate(Vector3.forward * thrust * Time.deltaTime);
        }
    }

    private void ApplyBoost()
    {
        if (CrossPlatformInputManager.GetButton("Fire3"))
        {
            if (currentBoostCapacity > 0)
            { transform.Translate(Vector3.forward * boostSpeed * Time.deltaTime);
              currentBoostCapacity -=  boostСonsumption * Time.deltaTime;
            }
            else
            { return; }
        }
    }

    private void BoostRecovery()
    {
        if (currentBoostCapacity != fullBoostCapacity)
        {
            currentBoostCapacity += boostRecoveryRate * Time.deltaTime;
        }
    }

    private void ApplyEarthStrike()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            Instantiate(earthStrike, Vector3.zero, Quaternion.identity);
           // FindObjectOfType<EarthStrike>().InitiateStrike();
        }
    }

   }
