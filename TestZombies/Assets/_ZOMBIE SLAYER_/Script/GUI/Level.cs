/// <summary>
/// The UI Level, check the current level
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{
    public int world = 1;
    public int level = 1;
    public bool isUnlock = false;
    public TextMeshProUGUI numberTxt;
    public GameObject lockContainer, openingContainer, passedContainer;

    public int tmplocklvl = 10;

    void Start()
    {
        Debug.LogWarning("GlobalValue.LevelPass: "+GlobalValue.LevelPass);

        var openLevel = isUnlock ? true : GlobalValue.LevelPass + 1 >= level;

        lockContainer.SetActive(false);
        openingContainer.SetActive(false);
        passedContainer.SetActive(false);

        numberTxt.text = level + "";

        if (level >= tmplocklvl)
        {
            lockContainer.SetActive(true);
            openingContainer.SetActive(true);
            GetComponent<Button>().interactable = false;
            return;
        }

        if (openLevel)
        {
          
            if (GlobalValue.LevelPass + 1 == level)
            {
                openingContainer.SetActive(true);
                FindObjectOfType<MapControllerUI>().SetCurrentWorld(world);
            }else
                passedContainer.SetActive(true);

        }
        else
        {
            lockContainer.SetActive(true);
            openingContainer.SetActive(true);
        }

        GetComponent<Button>().interactable = openLevel;
    }

    public void Play()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCustomizer>().SavePlayerLoadout();
        GlobalValue.levelPlaying = level;
        SoundManager.Click();
        MainMenuHomeScene.Instance.ShowChooseWeapon(true);
        MainMenuHomeScene.Instance.LoadScene();
    }
    private void OnDrawGizmosSelected()
    {
        gameObject.name = "Level " + level;
    }
}
