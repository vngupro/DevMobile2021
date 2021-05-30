using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class PlayService : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(this);

        try
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

            PlayGamesPlatform.InitializeInstance(config);

            // recommended for debugging:
            //PlayGamesPlatform.DebugLogEnabled = true;

            // Activate the Google Play Games platform
            PlayGamesPlatform.Activate();

            // authenticate user:
            PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
                // handle results
            });
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

    public void UnlockAchievement(String id)
    {
        // unlock achievement 
        Social.ReportProgress(id, 100.0f, (bool success) => {
            // handle success or failure
        });
    }

}
