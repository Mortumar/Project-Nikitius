using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Meteor : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float spin = 10f;
    [SerializeField] float xReflectionFactor = 500f;
    [SerializeField] float yReflectionFactor = 100f;

    LivesDisplay liveDisplay;
    GameSession gameSession;

    [SerializeField] Transform target;
    [SerializeField] ParticleSystem explosionVfx;

    Rigidbody rigidBody;

    bool isTargetingEarth = true;
    public bool isActive = true;
    float currentXPos;
    float currentYPos;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        currentXPos = transform.position.x;
        currentYPos = transform.position.y;
        Spin();
        TargetEarth();

    }

    private void TargetEarth()
    {
        if (isTargetingEarth && isActive)
        { transform.position += (target.position - transform.position).normalized * speed * Time.deltaTime; }
        else
        { return; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            liveDisplay = FindObjectOfType<LivesDisplay>();
            liveDisplay.takeLive();
            ParticleSystem explosion = Instantiate(explosionVfx, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Destroyer")
        {
            print("Gotta destoy yourself");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameSession = FindObjectOfType<GameSession>();
            gameSession.AddScore();
            isActive = false;
            isTargetingEarth = false;
            rigidBody.useGravity = true;
            XReflectDirection();
            YReflectDirection();
        }

    }

    void Spin()
    {
        transform.Rotate(Vector3.up, spin * Time.deltaTime, Space.World);
    }

    void XReflectDirection()
    {
        if (transform.position.x < currentXPos)
        {
            rigidBody.AddForce(Vector3.left * xReflectionFactor * Time.deltaTime);
        }
        else
        {
            rigidBody.AddForce(Vector3.right * xReflectionFactor * Time.deltaTime);
        }
    }
     void YReflectDirection()
        {
            if (transform.position.y < currentYPos)
            {
                rigidBody.AddForce(Vector3.down * yReflectionFactor * Time.deltaTime);
            }
            else
            {
                rigidBody.AddForce(Vector3.up * yReflectionFactor * Time.deltaTime);
            }
        }

 
    }



