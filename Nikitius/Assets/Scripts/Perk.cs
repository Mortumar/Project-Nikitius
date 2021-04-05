using UnityEngine;

public class Perk : MonoBehaviour
{
    [SerializeField] ParticleSystem energyVFX;
    [SerializeField] ParticleSystem bloodVFX;
    [SerializeField] float speed = 10f;
    [SerializeField] float spin = 10f;

    [SerializeField] Transform target;

    [SerializeField] AudioClip pickUpSound;
    [SerializeField] AudioClip deathSound;

    EarthEnergy earthEnergy;
    [SerializeField] int EnergyToAdd = 25;
    // Start is called before the first frame update
    void Start()
    {
        earthEnergy = FindObjectOfType<EarthEnergy>();
    }

    // Update is called once per frame
    void Update()
    {
        TargetEarth();
        Spin();
    }
    private void TargetEarth()
    {
         transform.position += (target.position - transform.position).normalized * speed * Time.deltaTime; 
    }
    void Spin()
    {
        transform.Rotate(Vector3.up, spin * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.left, spin * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(energyVFX, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(pickUpSound, Camera.main.transform.position, 1);
            earthEnergy.AddEnergy(EnergyToAdd);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Hello, Earth!");
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Asteroid"))
        {
            Instantiate(bloodVFX, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, 1);
            Destroy(gameObject);
        }
    }
}
