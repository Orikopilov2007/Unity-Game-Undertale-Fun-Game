using UnityEngine;

[CreateAssetMenu(fileName = "New Snowman Piece", menuName = "Inventory/Snowman Piece")]
public class SnowmanPiece : ScriptableObject
{
    public string itemName = "Snowman Piece";
    public int healAmount = 45;
}
