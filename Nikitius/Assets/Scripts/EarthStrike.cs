using UnityEngine;
public class EarthStrike : MonoBehaviour
{
    [SerializeField] float strikeSpeed = 10f;
    [SerializeField] float minimumRange = 0f;
    [SerializeField] float maximumRange = 100f;
    [SerializeField] float timeToDestroy = 2f;

    public static int energyCost = 100;
    float size;
    void Start()
    {
        size = minimumRange;
    }
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
     public static int GetEnergyCost()
    {
        return energyCost;
    }
}
