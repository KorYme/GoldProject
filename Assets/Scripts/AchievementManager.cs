using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] DataManager _dataManager;

    public void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        if (!enabled) return;
        _dataManager.OnStarAdded += CheckForAchievement;
        _dataManager.OnLevelComplete += Test_Perfect;
    }

    public void ShowUI(bool success)
    {
        if (success)
        {
        } 
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status != SignInStatus.Success)
        {
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
            enabled = false;
            return;
        }
        // Continue with Play Games Services
        Social.ReportProgress("CgkIhpOPlaMXEAIQAw", 100.0f, ShowUI);
    }

    void CheckForAchievement(int starAdded)
    {
        if (starAdded > 0)
        {
            PlayGamesPlatform.Instance.IncrementAchievement("CgkIhpOPlaMXEAIQAQ", starAdded, ShowUI);
        }
    }

    void Test_Perfect(int levelID, int star)
    {
        if (star != 4) return;
        if (levelID <= 0) return;
        if (_dataManager.LevelDictionnary[levelID] == 4) return;
        switch ((levelID - 1) / 10)
        {
            case 0:
                PlayGamesPlatform.Instance.IncrementAchievement("CgkIhpOPlaMXEAIQAg", 1, ShowUI);
                return;
            default:
                return;
        }
    }
}
