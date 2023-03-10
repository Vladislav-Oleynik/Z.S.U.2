using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuHomeScene : MonoBehaviour {
	public static MainMenuHomeScene Instance;

    public GameObject player;

    public GameObject StartUI;
    public GameObject MapUI;
    public GameObject Loading;
    //   public GameObject Shop;
    public GameObject Settings;
    public GameObject TestOption;
    //   public GameObject ChooseWeapon;
    //   public GameObject ExitUI;

    public GameObject MainMenu;
    public GameObject ShopMenu;
    public GameObject CharactersMenu;
    //public GameObject SettingsMenu;
    public GameObject MissionsMenu;
    public GameObject GameModePopup;
    public GameObject ExitPopup;


    public string facebookLink;
    public string twitterLink = "https://twitter.com/";

    //public Text[] coinTxt;

    [Header("Sound and Music")]
    public Image soundImage;
    public Image musicImage;
    public Sprite soundImageOn, soundImageOff, musicImageOn, musicImageOff;
    public TextMeshProUGUI moneyText;

    void Awake(){
		Instance = this;
  //      StartUI.SetActive(true);
  //      if (Loading != null)
		//	Loading.SetActive (false);
		//if (MapUI != null)
  //          MapUI.SetActive (false);
        if (Settings)
            Settings.SetActive(false);
        //Shop.SetActive(false);
        //ChooseWeapon.SetActive(false);
        //ExitUI.SetActive(false);

        MainMenu.SetActive(true);
        ShopMenu.SetActive(false);
        CharactersMenu.SetActive(false);
        //SettingsMenu.SetActive(false);
        MissionsMenu.SetActive(false);

        UpdateMoneyText();

        if (GameMode.Instance)
            TestOption.SetActive(GameMode.Instance.showTestOption);
    }


    
    public void UpdateMoneyText()
    {
        moneyText.text = GlobalValue.SavedCoins.ToString();
    }

    public void OpenShopMenu()
    {
        SoundManager.Click();
        MainMenu.SetActive(false);
        ShopMenu.SetActive(true);
        CharactersMenu.SetActive(false);
        //SettingsMenu.SetActive(false);
        MissionsMenu.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OpenMainMenu()
    {
        SoundManager.Click();
        MainMenu.SetActive(true);
        ShopMenu.SetActive(false);
        CharactersMenu.SetActive(false);
        //SettingsMenu.SetActive(false);
        MissionsMenu.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OpenCharactersMenu()
    {
        SoundManager.Click();
        MainMenu.SetActive(false);
        ShopMenu.SetActive(false);
        CharactersMenu.SetActive(true);
        //SettingsMenu.SetActive(false);
        MissionsMenu.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(false);
    }

    //public void OpenSettingsMenu()
    //{
    //    SoundManager.Click();
    //    MainMenu.SetActive(false);
    //    ShopMenu.SetActive(false);
    //    CharactersMenu.SetActive(false);
    //    //SettingsMenu.SetActive(true);
    //    MissionsMenu.SetActive(false);
    //    GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(false);
    //}

    public void OpenMissionsMenu()
    {
        SoundManager.Click();
        MainMenu.SetActive(false);
        ShopMenu.SetActive(false);
        CharactersMenu.SetActive(false);
        //SettingsMenu.SetActive(false);
        MissionsMenu.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(false);
    }

    public void GameModeChange(bool open)
    {
        SoundManager.Click();
        GameModePopup.SetActive(open);
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(!open);
    }

    public void Exit(bool close)
    {
        SoundManager.Click();
        ExitPopup.SetActive(close);
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(!close);
        
    }

    public void OpenShop(bool open)
    {
        SoundManager.Click();
        //Shop.SetActive(open);
    }

    public void CloseStartup()
    {
        SoundManager.Click(); 
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OpenExitGame(bool show)
    {
        SoundManager.Click();
        //ExitUI.SetActive(show);
    }

    public void QuitGame()
    {
        SoundManager.Click();
        Application.Quit();
    }

    public void ShowChooseWeapon(bool open)
    {
        SoundManager.Click();
        //ChooseWeapon.SetActive(open);
    }

    public void OpenMenuScene()
    {
        SoundManager.Click();
        StartUI.SetActive(false);
    }

    public void LoadScene(){
		if (Loading != null)
			Loading.SetActive (true);
        
        StartCoroutine(LoadAsynchronously("Playing"));
    }

    public void LoadScene(string sceneNamage)
    {
        if (Loading != null)
            Loading.SetActive(true);

        StartCoroutine(LoadAsynchronously(sceneNamage));
    }
    
	IEnumerator Start () {
		CheckSoundMusic();
        //if (GlobalValue.isFirstOpenMainMenu)
        //{
            //Debug.Log("GlobalValue.isFirstOpenMainMenu = " + GlobalValue.isFirstOpenMainMenu);
            GlobalValue.isFirstOpenMainMenu = false;
            SoundManager.Instance.PauseMusic(true);
            SoundManager.PlaySfx(SoundManager.Instance.beginSoundInMainMenu);
            yield return new WaitForSeconds(SoundManager.Instance.beginSoundInMainMenu.length);
            SoundManager.Instance.PauseMusic(false);
            SoundManager.PlayMusic(SoundManager.Instance.musicsGame);
        //}
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        CheckSoundMusic();

        //foreach (var ct in coinTxt)
        //{
        //    ct.text = GlobalValue.SavedCoins + "";
        //}
	}

    public void OpenMap(bool open)
    {
        SoundManager.Click();
        StartCoroutine(OpenMapCo(open));
    }

    IEnumerator OpenMapCo(bool open)
    {
        yield return null;
        BlackScreenUI.instance.Show(0.2f);
        MapUI.SetActive(open);
        BlackScreenUI.instance.Hide(0.2f);
    }

	public void Facebook(){
        SoundManager.Click();
		Application.OpenURL (facebookLink);
	}

    public void Twitter()
    {
        SoundManager.Click();
        Application.OpenURL(twitterLink);
    }

    public void ExitGame()
    {
        SoundManager.Click();
        Application.Quit();
    }

    public void Setting(bool open)
    {
        SoundManager.Click();
        Settings.SetActive(open);
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(!open);
    }

    #region Music and Sound

    public void TurnSound()
    {
        GlobalValue.isSound = !GlobalValue.isSound;
        soundImage.sprite = GlobalValue.isSound ? soundImageOn : soundImageOff;
        SoundManager.SoundVolume = GlobalValue.isSound ? 1 : 0;
    }

    //public void Setting(bool open)
    //{
    //    SoundManager.Click();
    //    Settings.SetActive(open);
    //    GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(!open);
    //}

    //#region Music and Sound
    //public void TurnSound()
    //{
    //    GlobalValue.isSound = !GlobalValue.isSound;
    //    soundImage.sprite = GlobalValue.isSound ? soundImageOn : soundImageOff;

    //    SoundManager.SoundVolume = GlobalValue.isSound ? 1 : 0;
    //}

    public void TurnMusic()
    {
        GlobalValue.isMusic = !GlobalValue.isMusic;
        musicImage.sprite = GlobalValue.isMusic ? musicImageOn : musicImageOff;

        SoundManager.MusicVolume = GlobalValue.isMusic ? SoundManager.Instance.musicsGameVolume : 0;
    }
    #endregion

    private void CheckSoundMusic(){
        soundImage.sprite = GlobalValue.isSound ? soundImageOn : soundImageOff;
        musicImage.sprite = GlobalValue.isMusic ? musicImageOn : musicImageOff;
        SoundManager.SoundVolume = GlobalValue.isSound ? 1 : 0;
        SoundManager.MusicVolume = GlobalValue.isMusic ? SoundManager.Instance.musicsGameVolume : 0;
    }

    public void Tutorial(){
		SoundManager.Click ();
		SceneManager.LoadScene ("Tutorial");
	}

    public Slider slider;
    public Text progressText;
    IEnumerator LoadAsynchronously(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = (int)progress * 100f + "%";
            yield return null;
        }
    }

    public void ResetData()
    {
        if (GameMode.Instance)
            GameMode.Instance.ResetDATA();
    }

    public void SetMaxCoin()
    {
        GlobalValue.SavedCoins = 99999;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void UnlockAll()
    {
        GlobalValue.LevelPass = 99999;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }
}
