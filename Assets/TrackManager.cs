using Unity;
using UnityEngine;
public class TrackManager : MonoBehaviour
{
    public InventoryManager inventoryManager; // Reference to the InventoryManager

    void Start()
    {
        if (inventoryManager != null)
        {
            inventoryManager.OnTextCompleted += EndTrack;
        }
    }

    void EndTrack()
    {
        Debug.Log("Track Ended.");
        // Your logic to end the track
    }
}
