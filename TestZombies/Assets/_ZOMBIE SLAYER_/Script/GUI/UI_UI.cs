using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_UI : MonoBehaviour
{
    [Header("GUN UI")]
    public Image gunIcon;
    public Text bulletLeft;
    [Space]
    public TextMeshProUGUI coinTxt;

    private void Update()
    {
        //healthSlider.value = Mathf.Lerp(healthSlider.value, healthValue, lerpSpeed * Time.deltaTime);
        
        coinTxt.text = GlobalValue.SavedCoins + "";
        bulletLeft.text = GameManager.Instance.Player.currentWeapon.bullet + "";
        gunIcon.sprite = GameManager.Instance.Player.currentWeapon.icon;
    }

    public void NextGun()
    {
        GunManager.Instance.NextGun();
    }
   
}
