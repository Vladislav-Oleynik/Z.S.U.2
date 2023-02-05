using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class MapControllerUI : MonoBehaviour {
	public RectTransform BlockLevel;
	public int howManyBlocks = 3;
	public float step = 720f;
	private float newPosX = 0;
	int currentPos = 0;
	public AudioClip music;

    public Button nextBtn, preBtn;

    public Sprite emptyCell;

    public List<Image> allCells;

    public TextMeshProUGUI missionText;

	void Start () {
        SetWorldNumber();
        if (allCells.Count != 0)
            allCells[0].transform.GetChild(0).gameObject.SetActive(true);
    }

    void SetWorldNumber()
    {
      
    }

    void Update()
    {
        nextBtn.interactable = (currentPos < howManyBlocks - 1);
        preBtn.interactable = (currentPos > 0);
    }

    void OnEnable(){
		SoundManager.PlayMusic (music);
		Debug.LogWarning ("ON ENALBE");

	}

	void OnDisable(){
		SoundManager.PlayMusic (SoundManager.Instance.musicsGame);
	}

    public void SetCurrentWorld(int world)
    {
        currentPos += (world - 1);

        newPosX -= step * (world - 1);
        newPosX = Mathf.Clamp(newPosX, -step * (howManyBlocks - 1), 0);

        SetMapPosition();

        SetWorldNumber();
    }

    public void SetMapPosition()
    {
        BlockLevel.anchoredPosition = new Vector2(newPosX, BlockLevel.anchoredPosition.y);
    }

    bool allowPressButton = true;
    public void Next()
    {
        if (allowPressButton)
        {
            StartCoroutine(NextCo());
        }
    }

    IEnumerator NextCo()
    {
        allowPressButton = false;

        SoundManager.Click();

        if (newPosX != (-step * (howManyBlocks - 1)))
        {
            currentPos++;

            newPosX -= step;
            newPosX = Mathf.Clamp(newPosX, -step * (howManyBlocks - 1), 0);
            
        }
        else
        {
            allowPressButton = true;
            yield break;

            //currentPos = 0;

            //newPosX = 0;
            //newPosX = Mathf.Clamp(newPosX, -step * (howManyBlocks - 1), 0);


        }

        BlackScreenUI.instance.Show(0.15f);

        yield return new WaitForSeconds(0.15f);
        SetMapPosition();
        BlackScreenUI.instance.Hide(0.15f);

        SetWorldNumber();

        DrawCells();

        ChangeMissionText();

        allowPressButton = true;

    }

    private void ChangeMissionText()
    {
        missionText.text = "Текст місії";
    }

    private void DrawCells()
    {
        for (int i = 0; i < allCells.Count; i++)
        {
            if (i == currentPos)
            {
                allCells[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                allCells[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void Pre()
    {
        if (allowPressButton)
        {
            StartCoroutine(PreCo());
        }
    }

    IEnumerator PreCo()
    {
        allowPressButton = false;
        SoundManager.Click();
        if (newPosX != 0)
        {
            currentPos--;

            newPosX += step;
            newPosX = Mathf.Clamp(newPosX, -step * (howManyBlocks - 1), 0);


        }
        else
        {
            allowPressButton = true;
            yield break;
            //currentPos = howManyBlocks - 1;

            //newPosX = -999999;
            //newPosX = Mathf.Clamp(newPosX, -step * (howManyBlocks - 1), 0);

        }

        BlackScreenUI.instance.Show(0.15f);

        yield return new WaitForSeconds(0.15f);
        SetMapPosition();
        BlackScreenUI.instance.Hide(0.15f);

        SetWorldNumber();

        DrawCells();

        ChangeMissionText();

        allowPressButton = true;

    }

	public void UnlockAllLevels(){
		GlobalValue.LevelPass = (GlobalValue.LevelPass + 1000);
		UnityEngine.SceneManagement.SceneManager.LoadScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex);
		SoundManager.Click ();
	}
}
