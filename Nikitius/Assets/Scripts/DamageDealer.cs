using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 20;

    public int GetDamage()
    {
        return damage;
    }

}
