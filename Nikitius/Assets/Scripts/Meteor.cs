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
    [SerializeField] AudioClip asteroidExplosionSound;
    Rigidbody rigidBody;

    // у тебя вроде isActive и isTargetingEarth выполняют одну и ту же функцию
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
            // присваивание переменной explosion без использования ее в дальнейшем
            ParticleSystem explosion = Instantiate(explosionVfx, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(asteroidExplosionSound, Camera.main.transform.position, 1);
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
            // вот видишь ты сделал метеор на физике, нужно и корабль тоже
            rigidBody.AddForce(Vector3.left * xReflectionFactor * Time.deltaTime);
        }
        else
        {
            // отскоки тоже можно на физике сделать а не так
            // или еще можно угол посчитать тригонометрией - покажешь скилы, а это хорошо
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



