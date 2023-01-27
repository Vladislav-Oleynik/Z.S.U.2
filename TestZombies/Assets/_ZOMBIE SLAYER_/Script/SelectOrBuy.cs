using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectOrBuy : MonoBehaviour
{
    Button button;
    public SelectedCharacter selectedCharacter;
    public bool isSelectable = false;

    void Start()
    {
        button = transform.GetComponent<Button>();
        ChangeButtonText();
    }

    public void NextBtnClick()
    {
        selectedCharacter.IncreaseIdx();
        ChangeButtonText();
    }

    public void PrevBtnClick()
    {
        selectedCharacter.DecreaseIdx();
        ChangeButtonText();
    }

    public void ChangeButtonText()
    {
        if (selectedCharacter.allCharacters.Count <= 0 || selectedCharacter.characterSelectorIdx >= selectedCharacter.allCharacters.Count)
        {
            Debug.Log("Troubles with list or index!");
            return;
        }

        CharacterStats charStats = selectedCharacter.allCharacters[selectedCharacter.characterSelectorIdx].GetComponent<CharacterStats>();

        if(charStats.isUnlocked)
        {
            button.transform.GetChild(0).GetComponent<Text>().text = "Select";
            isSelectable = true;
        }
        else
        {
            button.transform.GetChild(0).GetComponent<Text>().text = charStats.price + "$";
            isSelectable = false;
        }
    }

    public void SelectOrBuyCharacter()
    {
        CharacterStats charStats = selectedCharacter.allCharacters[selectedCharacter.characterSelectorIdx].GetComponent<CharacterStats>();

        if (charStats.isUnlocked)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            Instantiate(selectedCharacter.allCharacters[selectedCharacter.characterSelectorIdx], new Vector2(0.87f, -2.53f), Quaternion.identity);
            ////returning 0 will make it wait 1 frame
            //yield return WaitFor.Frames(100);
            Debug.Log("New char selected");
        }
        else
        {
            if (GlobalValue.SavedCoins >= charStats.price)
            {
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                GlobalValue.SavedCoins -= charStats.price;
                SoundManager.PlaySfx(SoundManager.Instance.soundUpgrade);
                Instantiate(selectedCharacter.allCharacters[selectedCharacter.characterSelectorIdx], new Vector2(0.87f, -2.53f), Quaternion.identity);
                ////returning 0 will make it wait 1 frame
                //yield return WaitFor.Frames(100);
                Debug.Log("New char bought");

            }
            else
            {
                SoundManager.PlaySfx(SoundManager.Instance.soundNotEnoughCoin);
                Debug.Log("NotEnoughCoin");
            }
        }
    }



    //public static class WaitFor
    //{
    //    public static IEnumerator Frames(int frameCount)
    //    {
    //        if (frameCount <= 0)
    //        {
    //            Debug.Log("Cannot wait for less that 1 frame");
    //        }

    //        while (frameCount > 0)
    //        {
    //            frameCount--;
    //            yield return null;
    //        }
    //    }
    //}
}
