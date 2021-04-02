using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perk : MonoBehaviour
{

    [SerializeField] float speed = 10f;
    [SerializeField] float spin = 10f;

    [SerializeField] Transform target;

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
        if (other.gameObject.tag == "Player")
        {
            earthEnergy.AddEnergy(EnergyToAdd);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Finish")
        {
            Debug.Log("Hello, Earth!");
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Asteroid")
        {
            Destroy(gameObject);
        }
    }
}
