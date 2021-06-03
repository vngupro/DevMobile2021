using System;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class PlayService : MonoBehaviour
{

    public static PlayService Instance;

    private PlayGamesClientConfiguration clientConfiguration;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        try
        {

            clientConfiguration = new PlayGamesClientConfiguration.Builder().Build();

            PlayGamesPlatform.InitializeInstance(clientConfiguration);

            // recommended for debugging:
            //PlayGamesPlatform.DebugLogEnabled = true;

            // Activate the Google Play Games platform
            PlayGamesPlatform.Activate();

            Debug.Log(clientConfiguration);

            // authenticate user:
            PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
            {
                // handle results

            });

            //Debug.Log("Player autantify by google :" + PlayGamesClientFactory.GetPlatformPlayGamesClient(clientConfiguration).IsAuthenticated());
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
        }
    }

    public void ShowAchievements()
    {
        // show achievements UI
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }

    public void UnlockAchievement(String id)
    {
        // unlock achievement 
        PlayGamesPlatform.Instance.ReportProgress(id, 100.0f, (bool success) =>
        {
            // handle success or failure
        });
    }

    public void TryConnectToPlayService()
    {
        // authenticate user:
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) =>
        {
            // handle results
        });
    }

}
