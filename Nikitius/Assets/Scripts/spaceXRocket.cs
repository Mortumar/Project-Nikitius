using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceXRocket : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float rotationSpeed = 10f;

    Meteor meteor;


    List<Meteor> asteroids = new List<Meteor>();



    // Start is called before the first frame update
    void Start()
    {
        meteor = FindObjectOfType<Meteor>();

    }

    // Update is called once per frame
    void Update()
    {
        targetAsteroids();
        if (meteor != null && meteor.isActive)
        {
            asteroids.Add(meteor);
            Debug.Log(asteroids.Count);
        }
    }

    private void targetAsteroids()
    {


        {
                for (int i = 0; i < asteroids.Count; i++)
                {
                     Vector3 targetVector = asteroids[i].transform.position - gameObject.transform.position;
                     transform.up = Vector3.Slerp(transform.up, targetVector, rotationSpeed * Time.deltaTime);
                     transform.position += (asteroids[i].transform.position - transform.position).normalized * speed * Time.deltaTime;
                }
            }

    }
}


