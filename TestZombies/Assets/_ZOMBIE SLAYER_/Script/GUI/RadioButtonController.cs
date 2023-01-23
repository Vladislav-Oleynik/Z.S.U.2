using UnityEngine;
using UnityEngine.UI;

public class RadioButtonController : MonoBehaviour
{
    public GameObject singleplayerBtn, multiplayerBtn;
    public Sprite onSprite, offSprite;

    public void SwitchGameMode(bool isSingleplayer = true)
    {
        if (isSingleplayer)
        {
            singleplayerBtn.GetComponent<Image>().sprite = onSprite;
            multiplayerBtn.GetComponent<Image>().sprite = offSprite;
        }
        else
        {
            singleplayerBtn.GetComponent<Image>().sprite = offSprite;
            multiplayerBtn.GetComponent<Image>().sprite = onSprite;
        }
    }
}
