#if UNITY_ANDROID
using GooglePlayGames.BasicApi;
using GooglePlayGames;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using System;
using UnityEngine.UIElements;

public class AchievementManager : MonoBehaviour
{
#if UNITY_ANDROID
    [SerializeField] DataManager _dataManager;
    [SerializeField] int _mickaLevelID;
    [SerializeField] int _emileLevelID;

    public Action<int> OnMovementAdded
    {
        get; set;
    }


    public static AchievementManager Instance;
    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
    }

    public void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        if (!enabled) return;
        _dataManager.OnStarAdded += CheckForAchievementWithStars;
        _dataManager.OnLevelComplete += CheckPerfectLevel;
        _dataManager.OnLevelComplete += CheckBonusLevel;
        _dataManager.OnLevelComplete += CheckVolunteerWork;
        OnMovementAdded += CheckMovementNumber;
    }

    private void OnDestroy()
    {
        if (!enabled) return;
        _dataManager.OnStarAdded -= CheckForAchievementWithStars;
        _dataManager.OnLevelComplete -= CheckPerfectLevel;
        _dataManager.OnLevelComplete -= CheckBonusLevel;
        _dataManager.OnLevelComplete -= CheckVolunteerWork;
        OnMovementAdded -= CheckMovementNumber;
    }
    public void AchieveSuccess(bool success)
    {
        if (success)
        {
            //Handheld.Vibrate();
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
        Social.ReportProgress("CgkIhpOPlaMXEAIQAw", 100.0f, AchieveSuccess);
    }

    void CheckForAchievementWithStars(int starAdded)
    {
        if (starAdded <= 0) return;
        PlayGamesPlatform.Instance.IncrementAchievement("CgkIhpOPlaMXEAIQAQ", Mathf.Clamp(starAdded, 1, 3), AchieveSuccess);
        PlayGamesPlatform.Instance.IncrementAchievement("CgkIhpOPlaMXEAIQBA", Mathf.Clamp(starAdded, 1, 3), AchieveSuccess);
        PlayGamesPlatform.Instance.IncrementAchievement("CgkIhpOPlaMXEAIQBQ", Mathf.Clamp(starAdded, 1, 3), AchieveSuccess);
    }

    void CheckPerfectLevel(int levelID, int star)
    {
        if (star != 4) return;
        if (levelID <= 0) return;
        if (_dataManager.LevelDictionnary[levelID] == 4) return;
        if (_dataManager.RealTotalStarNumber == 220)
        {
            PlayGamesPlatform.Instance.ReportProgress("CgkIhpOPlaMXEAIQFg", 100.0f, AchieveSuccess);
        }
        switch ((levelID - 1) / 10)
        {
            case 0:
                PlayGamesPlatform.Instance.IncrementAchievement("CgkIhpOPlaMXEAIQAg", 1, AchieveSuccess);
                return;
            case 1:
                PlayGamesPlatform.Instance.IncrementAchievement("CgkIhpOPlaMXEAIQDQ", 1, AchieveSuccess);
                return;
            case 2:
                PlayGamesPlatform.Instance.IncrementAchievement("CgkIhpOPlaMXEAIQDg", 1, AchieveSuccess);
                return;
            case 3:
                PlayGamesPlatform.Instance.IncrementAchievement("CgkIhpOPlaMXEAIQGQ", 1, AchieveSuccess);
                return;
            case 4:
                PlayGamesPlatform.Instance.IncrementAchievement("CgkIhpOPlaMXEAIQEA", 1, AchieveSuccess);
                return;
            default:
                return;
        }
    }

    void CheckBonusLevel(int levelID, int star)
    {
        if (levelID >= 0 || star <= 0) return;
        if(_dataManager.LevelDictionnary[levelID] !!= 0) return;
        switch (-levelID)
        {
            case 1:
                Social.ReportProgress("CgkIhpOPlaMXEAIQEQ", 100.0f, AchieveSuccess);
                return;
            case 2:
                Social.ReportProgress("CgkIhpOPlaMXEAIQEg", 100.0f, AchieveSuccess);
                return;
            case 3:
                Social.ReportProgress("CgkIhpOPlaMXEAIQEw", 100.0f, AchieveSuccess);
                return;
            case 4:
                Social.ReportProgress("CgkIhpOPlaMXEAIQFA", 100.0f, AchieveSuccess);
                return;
            case 5:
                Social.ReportProgress("CgkIhpOPlaMXEAIQFQ", 100.0f, AchieveSuccess);
                return;
            default:
                return;
        }
    }

    void CheckVolunteerWork(int levelID, int star)
    {
        if (levelID == _mickaLevelID && star > 0)
        {
            Social.ReportProgress("CgkIhpOPlaMXEAIQBg", 100.0f, AchieveSuccess);
        }
        else if (levelID == _emileLevelID && star == 4)
        {
            Social.ReportProgress("CgkIhpOPlaMXEAIQDw", 100.0f, AchieveSuccess);
        }
    }

    void CheckMovementNumber(int number)
    {
        if (number >= 999)
        {
            Social.ReportProgress("CgkIhpOPlaMXEAIQBw", 100.0f, AchieveSuccess);
        }
        else if (number >= 750)
        {
            Social.ReportProgress("CgkIhpOPlaMXEAIQCA", 100.0f, AchieveSuccess);
        }
        else if (number >= 500)
        {
            Social.ReportProgress("CgkIhpOPlaMXEAIQCQ", 100.0f, AchieveSuccess);
        }
        else if (number >= 250)
        {
            Social.ReportProgress("CgkIhpOPlaMXEAIQCg", 100.0f, AchieveSuccess);
        }
        else if (number >= 100)
        {
            Social.ReportProgress("CgkIhpOPlaMXEAIQCw", 100.0f, AchieveSuccess);
        }
    }

    public void NotYourBusiness()
    {
        Social.ReportProgress("CgkIhpOPlaMXEAIQDA", 100.0f, AchieveSuccess);
    }

    public void DisplayAchievementUI()
    {
        Social.ShowAchievementsUI();
    }
#endif
}
