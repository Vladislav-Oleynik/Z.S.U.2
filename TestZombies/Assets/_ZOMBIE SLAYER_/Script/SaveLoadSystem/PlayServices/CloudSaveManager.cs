using System.Runtime.Serialization.Formatters.Binary;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System.IO;
using System;
using UnityEngine;

public class CloudSaveManager : MonoBehaviour
{
    public static CloudSaveManager Instance { get; private set; }

    public static SaveData.CloudSaveData SaveData { get; private set; }

    [SerializeField]
    private DataSource _dataSource;

    [SerializeField]
    private ConflictResolutionStrategy _conflicts;

    [SerializeField, Space]
    private PlayerProfile _profile;

    private string saveName = "save_0";

    private BinaryFormatter _formatter;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _formatter = new BinaryFormatter();
        SaveData = new SaveData.CloudSaveData();
    }

    public void Save()
    {
        if (Authentication.authenticated)
            SaveToCloud();
    }

    public void Load()
    {
        if (Authentication.authenticated)
            LoadFromCloud();
    }

    public void UseLocalData()
    {
        _profile.Load(null);
    }

    public void ApplyCloudData(SaveData.CloudSaveData data, bool dataExists)
    {
        if (!dataExists || data == null)
        {
            UseLocalData();
            return;
        }

        _profile.Load(data.profile);
    }

    private SaveData.CloudSaveData CollectAllData()
    {
        var data = new SaveData.CloudSaveData()
        {
            profile = _profile.GetSaveSnapshot()
        };

        return data;
    }

    private void SaveToCloud()
    {
        OpenCloudSave(OnSaveResponse);
    }

    private void OnSaveResponse(SavedGameRequestStatus status, ISavedGameMetadata metadata)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            var rawData = CollectAllData();
            if (rawData == null)
                return;

            var data = SerializeSaveData(rawData);
            if (data == null)
                return;

            var update = new SavedGameMetadataUpdate.Builder().Build();
            Authentication.platform.SavedGame.CommitUpdate(metadata, update, data, SaveCallback);
        }
        else
        {
            Debug.LogError("OnSaveResponse error!");
        }
    }

    private void SaveCallback(SavedGameRequestStatus status, ISavedGameMetadata metadata)
    {
        if (status == SavedGameRequestStatus.Success)
            Debug.Log("Data saved successfully!");
        else
            Debug.Log("Data is not saved because of some error!");

    }

    private void LoadFromCloud()
    {
        OpenCloudSave(OnLoadResponse);
    }

    private void OnLoadResponse(SavedGameRequestStatus status, ISavedGameMetadata metadata)
    {
        if (status == SavedGameRequestStatus.Success)
            Authentication.platform.SavedGame.ReadBinaryData(metadata, LoadCallback);
        else
            UseLocalData();
    }

    private void LoadCallback(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
            ApplyCloudData(DeserializeSaveData(data), data.Length > 0);
        else
            UseLocalData();
    }

    private void OpenCloudSave(Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
    {
        if (!Social.localUser.authenticated ||
            !PlayGamesClientConfiguration.DefaultConfiguration.EnableSavedGames ||
            string.IsNullOrEmpty(saveName))
        {
            Debug.LogError("OpenCloud Save Error!");
        }

        Authentication.platform.SavedGame.OpenWithAutomaticConflictResolution(
            saveName, _dataSource, _conflicts, callback);
    }

    private byte[] SerializeSaveData(SaveData.CloudSaveData data)
    {
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                _formatter.Serialize(ms, data);
                return ms.GetBuffer();
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return null;
        }
    }

    private SaveData.CloudSaveData DeserializeSaveData(byte[] bytes)
    {
        if (bytes == null || bytes.Length == 0)
            return null;

        try
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return (SaveData.CloudSaveData)_formatter.Deserialize(ms);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return null;
        }
    }
}
