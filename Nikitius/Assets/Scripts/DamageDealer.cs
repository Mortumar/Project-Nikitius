using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 20;

    // вместо метода можно сделать просто публичный property вот так
    // но вообще у тебя класс тут, который ничего не делает, а просто выдает значение, нужно его либо убрать и перенести значение, или 
    // или добавить функционала, или сделать scriptable objectом
    public int Damage => damage;

    public int GetDamage()
    {
        return damage;
    }

}
