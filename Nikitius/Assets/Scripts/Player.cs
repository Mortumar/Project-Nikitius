using System.Collections;
using System.Collections.Generic;
// Не забудь удалить не использованые using 
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // даже когда используешь serializefield всегда добавляй private/public/protected и так далее. Так проще читать, и чище
    [SerializeField] float thrust = 1f;
    [SerializeField] float rotationFactor = 10f;
    [SerializeField] float fullBoostCapacity = 2f;
    // когда у тебя не меняется через инспектор значение, то не нужно его делать serializefield, а можно просто сделать private
    // кстати, ставь все переменные по группам - от самых вседоступных (public const) до самых "закрытых" внизу - private
    [SerializeField] static float currentBoostCapacity;
    [SerializeField] float boostSpeed = 2f;

    [SerializeField] float xRestrict = 26;
    [SerializeField] float yRestrict = 20;


    [SerializeField] float boostСonsumption = 2f;
    [SerializeField] float boostRecoveryRate = 1f;

    [SerializeField] GameObject strikePrefab;
    [SerializeField] AudioClip strikeSound;
    int energyCost = 100;

    [SerializeField] GameObject electricHit;
    [SerializeField] AudioClip electricHitSound;
    [SerializeField] AudioClip mainEngine;

    Rigidbody rigidBody;
    AudioSource audioSource;

    void Start()
    {
        electricHit.SetActive(false);
        rigidBody = GetComponent<Rigidbody>();
        currentBoostCapacity = fullBoostCapacity;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ApplyRotation();
        ApplyThrust();
        ApplyBoost();
        BoostRecovery();
        CallEarthStrike();
    }
    private void ApplyRotation()
    {
        // если не лень то воспользуйся новым юнитевским input managerом. это покажет, что ты можешь осваивать новые технологии
        // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/QuickStartGuide.html
        if (CrossPlatformInputManager.GetButton("Horizontal button 1"))
            transform.Rotate(Vector3.up, rotationFactor);
        // используй скобки вместо отступов даже на 1 строку, проще читать так

        else if (CrossPlatformInputManager.GetButton("Horizontal button 2"))
        {
            transform.Rotate(Vector3.down, rotationFactor);
        }
    }
    private void ApplyThrust()
    {
        if (CrossPlatformInputManager.GetButton("Jump"))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            // сделай полет через физику, чтобы у тебя не было супер быстрых поворотов при вращении на месте
            // и супер медленных при вращении  в движении. да и инерция это прикольно - покажет, что с физикой умеешь работать с юнитевской
            transform.Translate(Vector3.forward * thrust * Time.deltaTime);
            // старайся не пользоваться var потому что в гите если смотришь код, то сложнее его читать и понять что за тип будет
            var xPos = transform.position.x;
            var yPos = transform.position.y;
            var newXPos = Mathf.Clamp(xPos, -xRestrict, xRestrict);
            var newYPos = Mathf.Clamp(yPos, -yRestrict, yRestrict);
            transform.position = new Vector3(newXPos, newYPos, 0);
        }
        else
            audioSource.Stop();
        // такие однострочнын else это очень не по правилам :)
    }

    private void ApplyBoost()
    {
        if (CrossPlatformInputManager.GetButton("Fire3"))
        {
            if (currentBoostCapacity > 0)
            {
                // у тебя буст и thrust оба транслируют корабль, а должны влиять на перменную скорости, которая уже будет в апдейте считываться и применяться к кораблю
                // так будет логичнее
                transform.Translate(Vector3.forward * boostSpeed * Time.deltaTime);
                currentBoostCapacity -= boostСonsumption * Time.deltaTime;
            }
        }
    }

    private void BoostRecovery()
    {
        // рекавери наверное должен быть только в том случае если у тебя не зажат буст
        if (currentBoostCapacity != fullBoostCapacity)
        {
            currentBoostCapacity += boostRecoveryRate * Time.deltaTime;
        }
    }
    public void TryToApplyEarthStrike()
    {
        // любые методы findobjectoftype/getcomponent в update - это очень не производительно
        // просто закешируй этот earthenergy на старте
        var earthEnergy = FindObjectOfType<EarthEnergy>();

        // у тебя получается, что цена абилки хранится в переменной на игроке
        // это не правильно с точки зрения разделения отвественностей
        // абилки скорее должны про свою цену знать, а игрок про нынешнюю энергию
        if (earthEnergy.HaveEnoughEnergy(energyCost))
        {
            // не забудь этот префаб удалить как-то потом
            Instantiate(strikePrefab, Vector3.zero, Quaternion.identity);
            // Camera.main под копотом вроде как вызывает FindObjectByTag что жутко не производительно, так что
            // просто найди камеру на старте и закешируй
            AudioSource.PlayClipAtPoint(strikeSound, Camera.main.transform.position, 1);
            earthEnergy.SpendEnergy(energyCost);
        }
    }
    private void CallEarthStrike()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            TryToApplyEarthStrike();
        }
        else
        {
            // ретерн тут не нужен, так как если кнопка не нажата, все равно программа выдет из метода
            return;
        }
    }
    public static float GetCurrentBoostCapacity()
    {
        // статический метод здесь лучше не делать, ведь у тебя буст капасити на этом конкретном игроке, а не на всех игроках один
        return currentBoostCapacity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // пользуйся comparetag методом вместо сравнения по строке, так как он быстрее
        if (collision.gameObject.tag == "Asteroid" && electricHit != null)
        {
            electricHit.SetActive(true);
            AudioSource.PlayClipAtPoint(electricHitSound, Camera.main.transform.position, 1);
        }       
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        { electricHit.SetActive(false); } 
        // ставь скобки на отдельные строки
    }
}
