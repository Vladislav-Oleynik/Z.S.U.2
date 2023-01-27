using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelector : MonoBehaviour
{
    //public SelectedCharacter selectedCharacter;

    public Button head_accBtn;
    public Button headBtn;
    public Button torsoBtn;
    public Button handsBtn;
    public Button legsBtn;
    public Button feetBtn;
    public Button weapon1Btn;
    public Button weapon2Btn;

    //public void SelectCharacter(GameObject prefab)
    //{
    //    if (prefab)
    //    {
    //        selectedCharacter.selectedCharacterPrefab = prefab;
    //        Debug.Log($"Prefab [{prefab}] selected.");
    //    }

    //    Destroy(GameObject.FindGameObjectWithTag("Player"));

    //    Instantiate(prefab, new Vector2(5.0f,-2.0f), Quaternion.identity);

    //    //ChangeButtonListeners();
    //}

    public void ChangeButtonListeners()
    {
        StartCoroutine(buttonsLogic());
    }

    IEnumerator buttonsLogic()
    {
        ////returning 0 will make it wait 1 frame
        //yield return WaitFor.Frames(1);
        yield return null;
        //PlayerCustomizer customizer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCustomizer>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerCustomizer customizer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCustomizer>();
        Debug.Log(player);
        //PlayerCustomizer customizer = selectedCharacter.selectedCharacterPrefab.GetComponent<PlayerCustomizer>();

        head_accBtn.onClick.RemoveAllListeners();
        head_accBtn.onClick.AddListener(() => customizer.ChangeHead_accItem());

        headBtn.onClick.RemoveAllListeners();
        headBtn.onClick.AddListener(() => customizer.ChangeHeadItem());

        torsoBtn.onClick.RemoveAllListeners();
        torsoBtn.onClick.AddListener(() => customizer.ChangeTorsoItem());

        handsBtn.onClick.RemoveAllListeners();
        handsBtn.onClick.AddListener(() => customizer.ChangeHandsItem());

        legsBtn.onClick.RemoveAllListeners();
        legsBtn.onClick.AddListener(() => customizer.ChangeLegsItem());

        feetBtn.onClick.RemoveAllListeners();
        feetBtn.onClick.AddListener(() => customizer.ChangeFeetItem());

        weapon1Btn.onClick.RemoveAllListeners();
        weapon1Btn.onClick.AddListener(() => customizer.ChangeFirstWeapon());

        weapon2Btn.onClick.RemoveAllListeners();
        weapon2Btn.onClick.AddListener(() => customizer.ChangeSecondWeapon());

        player.transform.GetChild(0).gameObject.SetActive(false);
    }
}
