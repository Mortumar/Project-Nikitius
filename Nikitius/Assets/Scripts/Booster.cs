using UnityEngine;
using UnityEngine.UI;

public class Booster : MonoBehaviour
{
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Player>();
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
