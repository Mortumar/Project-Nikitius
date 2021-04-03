using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceXShuttle : MonoBehaviour
{
    [SerializeField] float speedOfRotation = 10f;
    [SerializeField] Vector3 target;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target, Vector3.forward, 20 * speedOfRotation * Time.deltaTime);
    }
}
