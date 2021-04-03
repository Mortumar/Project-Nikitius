using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Booster : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        var player = FindObjectOfType<Player>();
    }

    void Update()
    {
        SliderValue();
    }
    void SliderValue()
    {
        float boostCapacity = Player.GetCurrentBoostCapacity();
        GetComponent<Slider>().value = boostCapacity;
    }
}
