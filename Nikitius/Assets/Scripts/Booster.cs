using System.Collections;
using System.Collections.Generic;
// убери не использованые using
using UnityEngine;
using UnityEngine.UI;

public class Booster : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        // если есть возможность то добавь игрока в инспекторе или добавь бустер на игрока
        // чтобы можно было найти скрипт через getcomponent
        var player = FindObjectOfType<Player>();
    }

    void Update()
    {
        // у тебя в апдейте меняется UI, по правилам и чтобы было производительно нужно чтобы ui реагировал на изменения 
        // у игрока и только тогда апдейтил слайдер, так что сделай в player какой-то public event Action<float> на
        // который будет реагировать UI
        SliderValue();
    }
    void SliderValue()
    {
        float boostCapacity = Player.GetCurrentBoostCapacity();
        GetComponent<Slider>().value = boostCapacity;
    }
}
