using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class PlayService : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);

        try
        {
            PlayGamesClientConfiguration configuration = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(configuration);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate((bool success) => { });
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
        }

    }

    public void ShowAchivments()
    {
        // show achievements UI
        Social.ShowAchievementsUI();
    }

    public void ShowLeaderboard()
    {
        // show leaderboard UI
        Social.ShowLeaderboardUI();
    }

    public void UnlockAchivments(String Id)
    {
        
    }
}
