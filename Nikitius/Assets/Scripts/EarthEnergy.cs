using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EarthEnergy : MonoBehaviour
{
    [SerializeField] int energy = 100;
    int maxEnergy = 100;
    // всегда пользуйся textmeshpro так как он уже давно стандарт в Unity, а это считается легаси уже
    Text energyText;
    

    void Start()
    {
        energyText = GetComponent<Text>();
        UpdateDisplay();
    }

    private void Update()
    {
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        // опять же UI должен реагировать а не в апдейте менятся
        // почитай про delegate pattern
        
        // а также не канкатинируй строки а сделай $"{energy}%" - это стандарт
        energyText.text = energy.ToString() + "%"; 
    }
    public bool HaveEnoughEnergy(int amount)
    {
        return energy >= amount;
    }
    public void AddEnergy(int amount)
    {
        // тут баг из за отсуствия отступа,
        // вот поэтому в if всегда нужно делать скобки вне зависимости от количество строк внутри
        if (energy<maxEnergy)
        energy += amount;
        UpdateDisplay();
    }

    public void SpendEnergy(int amount)
    {
        if (energy >= amount)
        {
            energy -= amount;
            UpdateDisplay();
        }
    }
   
}
