using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float thrust = 1f;
    [SerializeField] float rotationFactor = 10f;
    float fullBoostCapacity = 2f;
    [SerializeField] public static float currentBoostCapacity;
    [SerializeField] float boostSpeed = 2f;

    [SerializeField] float xRestrict = 16;
    [SerializeField] float yRestrict = 12;

    [SerializeField] float boostСonsumption = 2f;
    float boostRecoveryRate = 1f;

    [SerializeField] GameObject strikePrefab;
    [SerializeField] GameObject electricHit;
    [SerializeField] AudioClip electricHitSound;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip strikeSound;

    Rigidbody rigidBody;
    AudioSource audioSource;
    EarthEnergy earthEnergy;
    void Start()
    {
        electricHit.SetActive(false);
        rigidBody = GetComponent<Rigidbody>();
        currentBoostCapacity = fullBoostCapacity;
        audioSource = GetComponent<AudioSource>();
        earthEnergy = FindObjectOfType<EarthEnergy>();
    }
    void Update()
    {
        ApplyRotation();
        ApplyBoost();
        ApplyThrust();
        BoostRecovery();
        CallEarthStrike();
    }
    private void ApplyRotation()
    {
        if (CrossPlatformInputManager.GetButton("Horizontal button 1"))
        {
            // transform.Rotate(Vector3.up, rotationFactor);
            rigidBody.AddTorque(Vector3.forward * rotationFactor * Time.deltaTime);
        }
        else if (CrossPlatformInputManager.GetButton("Horizontal button 2"))
        {
            //transform.Rotate(Vector3.down, rotationFactor);
            rigidBody.AddTorque(Vector3.back * rotationFactor * Time.deltaTime);
        }
    }
    private void ApplyThrust()
    {
        if (CrossPlatformInputManager.GetButton("Jump"))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            rigidBody.AddRelativeForce(Vector3.forward * thrust  * Time.deltaTime);
            float xPos = transform.position.x;
            float yPos = transform.position.y;
            float newXPos = Mathf.Clamp(xPos, -xRestrict, xRestrict);
            float newYPos = Mathf.Clamp(yPos, -yRestrict, yRestrict);
            transform.position = new Vector3(newXPos, newYPos, 0);
        }
        else
            audioSource.Stop();
    }
    private void ApplyBoost()
    {
        if (CrossPlatformInputManager.GetButton("Fire3"))
        {
            if (currentBoostCapacity > 0)
            {
                rigidBody.AddRelativeForce(Vector3.forward * boostSpeed * Time.deltaTime);
                float xPos = transform.position.x;
                float yPos = transform.position.y;
                float newXPos = Mathf.Clamp(xPos, -xRestrict, xRestrict);
                float newYPos = Mathf.Clamp(yPos, -yRestrict, yRestrict);
                transform.position = new Vector3(newXPos, newYPos, 0);
                currentBoostCapacity -= boostСonsumption * Time.deltaTime;
            }
        }
    }
    private void BoostRecovery()
    {
        if (!CrossPlatformInputManager.GetButton("Fire3") && currentBoostCapacity <= fullBoostCapacity)
        {
            currentBoostCapacity += boostRecoveryRate * Time.deltaTime;
        }
    }
    public void TryToApplyEarthStrike()
    {
        int energyCost = EarthStrike.GetEnergyCost();
        if (earthEnergy.HaveEnoughEnergy(energyCost))
        {
            Instantiate(strikePrefab, Vector3.zero, Quaternion.identity);
            AudioSource.PlayClipAtPoint(strikeSound, Camera.main.transform.position, 1);
            earthEnergy.SpendEnergy(energyCost);
        }
    }
    private void CallEarthStrike()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            TryToApplyEarthStrike();
        }
    }
    public static float GetCurrentBoostCapacity()
    {
        return currentBoostCapacity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag ("Asteroid") && electricHit != null)
        {
            electricHit.SetActive(true);
            AudioSource.PlayClipAtPoint(electricHitSound, Camera.main.transform.position, 1);
        }       
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        { 
            electricHit.SetActive(false);
        }
    }
}
