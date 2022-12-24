using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames;

public class Authentication : MonoBehaviour
{
    public static bool authenticated { get; private set; }

    public static PlayGamesPlatform platform { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

    private void Login()
    {
        if (platform == null)
            platform = BuildPlatform();

        PlayGamesPlatform.Instance.Authenticate(success =>
        {
            authenticated = success;
            OnAuthenticationSucceded();
        });
    }

    private void OnAuthenticationSucceded()
    {
        if (authenticated)
            CloudSaveManager.Instance.Load();
        else
            CloudSaveManager.Instance.UseLocalData();
    }

    private PlayGamesPlatform BuildPlatform()
    {
        var builder = new PlayGamesClientConfiguration.Builder();
        builder.EnableSavedGames();

        //builder.RequestServerAuthCode(false); // this line is not needed

        PlayGamesPlatform.InitializeInstance(builder.Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        return PlayGamesPlatform.Activate();
    }
}
