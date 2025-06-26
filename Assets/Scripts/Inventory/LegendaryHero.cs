using UnityEngine;

[CreateAssetMenu(fileName = "New Legendary Hero", menuName = "Inventory/Legendary Hero")]
public class LegendaryHero : ScriptableObject
{
    public string itemName = "Legendary Hero";
    public int healAmount = 40;
}
