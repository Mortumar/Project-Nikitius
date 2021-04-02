using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class EarthStrike : MonoBehaviour
{
    [SerializeField] float strikeSpeed = 10f;
    [SerializeField] float minimumRange = 0f;
    [SerializeField] float maximumRange = 100f;
    [SerializeField] float timeToDestroy = 2f;


    float size;
    // Start is called before the first frame update
    void Start()
    {
        size = minimumRange;
    }

    // Update is called once per frame
    void Update()
    {
        InitiateStrike();
    }
  
     void InitiateStrike()
    {
        size += Mathf.Lerp(minimumRange, maximumRange, strikeSpeed * Time.deltaTime);
        transform.localScale = new Vector3(size, size,10f);
        Destroy(gameObject, timeToDestroy);
    }

}
