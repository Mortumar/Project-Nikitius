using UnityEngine;
using System.Collections;

public class Earth : MonoBehaviour
{
    public float speed = 10f;

    [SerializeField] int health = 100;
    [SerializeField] GameObject explosionFX;
    [SerializeField] AudioClip earthExplosionSound;
    // всегда уберай пустые методы
    private void Start()
    {

    }
    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        // для элегантности можно сделать вместо такого определения дамадж дилера 
        // какай нибудь класс, который бы записывал бы в hashtable всех дамадж дилеров и потом ты бы просто делал что-то вроде
        // DamageDealerDatabase.Contains(gameObject)
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    void Explode()
    {
        // дестрой лучше делать после всего этого, чтобы случайно не уничтожить раньше времени и не вызвать ошибку в одном из 
        // последующих методов
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(earthExplosionSound, Camera.main.transform.position, 1);
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        FindObjectOfType<SceneManagerScript>().LoadGameOver();

    }
    // методы лучше распологать в примерном порядке вызова, то есть ProcessHit должен быть перед Explode в файле
    private void ProcessHit(DamageDealer damageDealer)
    {
        // а что будет если у тебя сразу два processhit вызовутся один за другим?
        // могут быть обишки, потому что Destroy это не моментальный метод в Unity
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
           Explode();
        }
    }
}