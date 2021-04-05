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
    
    public bool isActive = true;
    float currentXPos;
    float currentYPos;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        currentXPos = transform.position.x;
        currentYPos = transform.position.y;
        Spin();
        TargetEarth();
    }

    private void TargetEarth()
    {
        if (isActive)
        { transform.position += (target.position - transform.position).normalized * speed * Time.deltaTime; }
        else
        { return; }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            liveDisplay = FindObjectOfType<LivesDisplay>();
            liveDisplay.TakeLive();
            Instantiate(explosionVfx, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(asteroidExplosionSound, Camera.main.transform.position, 1);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Destroyer"))
        {
            print("Gotta destoy yourself");
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameSession = FindObjectOfType<GameSession>();
            gameSession.AddScore();
            isActive = false;
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



