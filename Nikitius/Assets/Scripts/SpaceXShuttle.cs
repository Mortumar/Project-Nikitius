using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceXShuttle : MonoBehaviour
{
    [SerializeField] float speedOfRotation = 10f;
    [SerializeField] Vector3 target;
    // Start is called before the first frame update
    // ненужные каменты удалять нужно

    // Update is called once per frame
    void Update()
    {
        // а че за двадцатка без переменной?
        transform.RotateAround(target, Vector3.forward, 20 * speedOfRotation * Time.deltaTime);
    }
}
