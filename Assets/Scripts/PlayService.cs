using System;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class PlayService : MonoBehaviour
{

    public static PlayService Instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        try
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

            PlayGamesPlatform.InitializeInstance(config);

            // recommended for debugging:
            //PlayGamesPlatform.DebugLogEnabled = true;

            // Activate the Google Play Games platform
            PlayGamesPlatform.Activate();

            // authenticate user:
            PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
            {
                // handle results
            });
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
        }
    }

    public void ShowAchievements()
    {
        // show achievements UI
        Social.ShowAchievementsUI();
    }

    public void ShowLeaderboard()
    {
        // show leaderboard UI
        Social.ShowLeaderboardUI();
    }

    public void UnlockAchievement(String id)
    {
        // unlock achievement 
        Social.ReportProgress(id, 100.0f, (bool success) =>
        {
            // handle success or failure
        });
    }

}
