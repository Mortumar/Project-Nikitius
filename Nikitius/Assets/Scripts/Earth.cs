using UnityEngine;
using System.Collections;

public class Earth : MonoBehaviour
{
    public float speed = 10f;

    [SerializeField] int health = 100;
    [SerializeField] GameObject explosionFX;
    [SerializeField] AudioClip earthExplosionSound;
    private void Start()
    {

    }
    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    void Explode()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(earthExplosionSound, Camera.main.transform.position, 1);
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        FindObjectOfType<SceneManagerScript>().LoadGameOver();

    }
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
           Explode();
        }
    }
}