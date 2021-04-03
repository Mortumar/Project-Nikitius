using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float thrust = 1f;
    [SerializeField] float rotationFactor = 10f;
    [SerializeField] float fullBoostCapacity = 2f;
    [SerializeField] static float currentBoostCapacity;
    [SerializeField] float boostSpeed = 2f;

    [SerializeField] float xRestrict = 26;
    [SerializeField] float yRestrict = 20;


    [SerializeField] float boostСonsumption = 2f;
    [SerializeField] float boostRecoveryRate = 1f;

    [SerializeField] GameObject strikePrefab;
    int energyCost = 100;

    [SerializeField] GameObject electricHit;
    Rigidbody rigidBody;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        currentBoostCapacity = fullBoostCapacity;
    }

    void Update()
    {
        ApplyRotation();
        ApplyThrust();
        ApplyBoost();
        BoostRecovery();
        CallEarthStrike();
    }
    private void ApplyRotation()
    {
        if (CrossPlatformInputManager.GetButton("Horizontal button 1"))

            transform.Rotate(Vector3.up, rotationFactor);

        else if (CrossPlatformInputManager.GetButton("Horizontal button 2"))
        {
            transform.Rotate(Vector3.down, rotationFactor);
        }
    }
    private void ApplyThrust()
    {
        if (CrossPlatformInputManager.GetButton("Jump"))
        {
            transform.Translate(Vector3.forward * thrust * Time.deltaTime);
            var xPos = transform.position.x;
            var yPos = transform.position.y;
            var newXPos = Mathf.Clamp(xPos, -xRestrict, xRestrict);
            var newYPos = Mathf.Clamp(yPos, -yRestrict, yRestrict);
            transform.position = new Vector3(newXPos, newYPos, 0);
        }

    }

    private void ApplyBoost()
    {
        if (CrossPlatformInputManager.GetButton("Fire3"))
        {
            if (currentBoostCapacity > 0)
            {
                transform.Translate(Vector3.forward * boostSpeed * Time.deltaTime);
                currentBoostCapacity -= boostСonsumption * Time.deltaTime;
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
    public void TryToApplyEarthStrike()
    {
        var earthEnergy = FindObjectOfType<EarthEnergy>();

        if (earthEnergy.HaveEnoughEnergy(energyCost))
        {
            Instantiate(strikePrefab, Vector3.zero, Quaternion.identity);
            earthEnergy.SpendEnergy(energyCost);
        }
    }
    private void CallEarthStrike()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            TryToApplyEarthStrike();
        }
        else
        {
            return;
        }
    }
    public static float GetCurrentBoostCapacity()
    {
        return currentBoostCapacity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            electricHit.SetActive(true);
        }
        else
            return;
       
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        { electricHit.SetActive(false); }
    }
}
