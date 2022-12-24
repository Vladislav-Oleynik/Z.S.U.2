using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    [SerializeField]
    private int _id;

    [SerializeField]
    private string _name;

    [SerializeField]
    private int _balance;

    private const string saveKey = "mainSave";

    // Start is called before the first frame update
    //void Start()
    //{
    //    //Load();
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        //TODO: добавить сохранение на нажатие клавиши S
    //        SaveButtonClicked();
    //        Debug.Log("Save");

    //    }
    //}

    public void SaveButtonClicked()
    {
        Save();
    }

    private void Save() 
    {
        CloudSaveManager.Instance.Save();
        SaveManager.Save(saveKey, GetSaveSnapshot());
    }

    public void Load(SaveData.PlayerProfile data)
    {
        if(data == null)
            data = SaveManager.Load<SaveData.PlayerProfile>(saveKey);

        _id = data.id;

        _name = data.name;

        _balance = data.balance;
    }

    public SaveData.PlayerProfile GetSaveSnapshot()
    {
        var data = new SaveData.PlayerProfile()
        {
            id = _id,
            name = _name,
            balance = _balance
        };
        return data;
    }

    public void ChangeBalance(int newBalance)
    {
        //now we use this to save scores of player
        //save only the best score
        if (newBalance > _balance)
        {
            _balance = newBalance;
            Save();
        }
    }

    public int GetBalance()
    {
        return _balance;
    }
}
