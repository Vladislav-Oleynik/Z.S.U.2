using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "SelectedCharacter", menuName = "ScriptableObjects/SelectedCharacter", order = 4)]
public class SelectedCharacter : ScriptableObject
{
    public GameObject selectedCharacterPrefab;
    public List<GameObject> allCharacters;
    public int characterSelectorIdx = 0;

    public void IncreaseIdx()
    {
        characterSelectorIdx++;
    }

    public void DecreaseIdx()
    {
        if(characterSelectorIdx > 0)
            characterSelectorIdx--;
    }
}
