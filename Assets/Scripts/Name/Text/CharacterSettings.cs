using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Character Settings", menuName = "Settings/Character Settings")]
public class CharacterSettings : ScriptableObject
{
    public TMP_FontAsset sansFont;  // Font for Sans
    public TMP_FontAsset papyrusFont;  // Font for Papyrus
    public TMP_FontAsset charaFont;  // Font for Chara
}
