using UnityEngine;
using UnityEngine.UI;

public class EarthEnergy : MonoBehaviour
{
    [SerializeField] int energy = 100;
    int maxEnergy = 100;
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
        energyText.text = $"{energy}%"; 
    }
    public bool HaveEnoughEnergy(int amount)
    {
        return energy >= amount;
    }
    public void AddEnergy(int amount)
    {
        if (energy < maxEnergy)
        {
            energy += amount;
            UpdateDisplay();
        }
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
