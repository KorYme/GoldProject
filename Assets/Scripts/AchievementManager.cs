using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.UIElements;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] DataManager _dataManager;

    public void Start()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        _dataManager.OnStarAdded += CheckForAchievement;
        _dataManager.OnLevelComplete += Test_Perfect_1_2;
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

        //PlayGamesPlatform.Instance.IncrementAchievement("CgkIhpOPlaMXEAIQAw", );
        Social.ReportProgress("CgkIhpOPlaMXEAIQAw", 100.0f, x => {  });
        Social.ShowAchievementsUI();
    }

    void CheckForAchievement(int starAdded)
    {
        if (!enabled) return;
        if (starAdded > 0)
        {
            PlayGamesPlatform.Instance.IncrementAchievement("CgkIhpOPlaMXEAIQAQ", starAdded, x => { });
            Social.ShowAchievementsUI();
        }
    }

    void Test_Perfect_1_2(int levelID, int star)
    {
        if (star != 4) return;
        if (levelID <= 0 || levelID >= 3) return;
        if (_dataManager.LevelDictionnary[levelID] != 4)
        {
            PlayGamesPlatform.Instance.IncrementAchievement("CgkIhpOPlaMXEAIQAg", 1, x => { });
            Social.ShowAchievementsUI();
        }
    }
}
