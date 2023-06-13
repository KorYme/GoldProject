using GooglePlayGames.BasicApi;
using GooglePlayGames;
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
    [SerializeField] DataManager _dataManager;
    [SerializeField] int _mickaLevelID;

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
            Handheld.Vibrate();
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
        if (DataManager.Instance.RealTotalStarNumber < 165) return;
        // INSERT PERFECTION ACHIEVEMENT
    }

    void CheckPerfectLevel(int levelID, int star)
    {
        if (star != 4) return;
        if (levelID <= 0) return;
        if (_dataManager.LevelDictionnary[levelID] == 4) return;
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
                PlayGamesPlatform.Instance.IncrementAchievement("CgkIhpOPlaMXEAIQDw", 1, AchieveSuccess);
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
        if (levelID != _mickaLevelID || star <= 0) return;
        Social.ReportProgress("CgkIhpOPlaMXEAIQBg", 100.0f, AchieveSuccess);
    }

    void CheckMovementNumber(int number)
    {
        if (number >= 999)
        {
            Social.ReportProgress("CgkIhpOPlaMXEAIQCw", 100.0f, AchieveSuccess);
        }
        else if (number >= 750)
        {
            Social.ReportProgress("CgkIhpOPlaMXEAIQCg", 100.0f, AchieveSuccess);
        }
        else if (number >= 500)
        {
            Social.ReportProgress("CgkIhpOPlaMXEAIQCQ", 100.0f, AchieveSuccess);
        }
        else if (number >= 250)
        {
            Social.ReportProgress("CgkIhpOPlaMXEAIQCA", 100.0f, AchieveSuccess);
        }
        else if (number >= 100)
        {
            Social.ReportProgress("CgkIhpOPlaMXEAIQBw", 100.0f, AchieveSuccess);
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
}

//<? xml version = "1.0" encoding = "utf-8" ?>
//< !--Google Play game services IDs. Save this file as res/values/games-ids.xml in your project.-->
//<resources>
//  <!--app_id-->
//  <string name="app_id" translatable="false">799713511814</string>
//  <!--package_name-->
//  <string name="package_name" translatable="false">com.Team501.Iridescence</string>


//  DONE
//  <!--achievement First Time Here ?-->
//  <string name="achievement_first_time_here" translatable="false">CgkIhpOPlaMXEAIQAw</string>


//  DONE
//  <!--achievement Get 50 stars-->
//  <string name="achievement_get_50_stars" translatable="false">CgkIhpOPlaMXEAIQAQ</string>
//  <!--achievement Get 100 stars-->
//  <string name="achievement_get_100_stars" translatable="false">CgkIhpOPlaMXEAIQBA</string>
//  <!--achievement Get 150 Stars-->
//  <string name="achievement_get_150_stars" translatable="false">CgkIhpOPlaMXEAIQBQ</string>


//  DONE
//  <!--achievement Perfect 1-10-->
//  <string name="achievement_perfect_110" translatable="false">CgkIhpOPlaMXEAIQAg</string>
//  <!--achievement Perfect 11-20-->
//  <string name="achievement_perfect_1120" translatable="false">CgkIhpOPlaMXEAIQDQ</string>
//  <!--achievement Perfect 21-30-->
//  <string name="achievement_perfect_2130" translatable="false">CgkIhpOPlaMXEAIQDg</string>
//  <!--achievement Perfect 31-40-->
//  <string name="achievement_perfect_3140" translatable="false">CgkIhpOPlaMXEAIQDw</string>
//  <!--achievement Perfect 41-50-->
//  <string name="achievement_perfect_4150" translatable="false">CgkIhpOPlaMXEAIQEA</string>

//  DONE
//  <!--achievement Travail bénévole-->
//  <string name="achievement_travail_bnvole" translatable="false">CgkIhpOPlaMXEAIQBg</string>

//  DONE
//  <!--achievement Not your business-->
//  <string name="achievement_not_your_business" translatable="false">CgkIhpOPlaMXEAIQDA</string>


//  DONE
//  <!--achievement Don't give up ! You can still win this !-->
//  <string name="achievement_dont_give_up__you_can_still_win_this" translatable="false">CgkIhpOPlaMXEAIQCw</string>
//  <!--achievement You got the moves !-->
//  <string name="achievement_you_got_the_moves" translatable="false">CgkIhpOPlaMXEAIQCg</string>
//  <!--achievement Don't even think about it-->
//  <string name="achievement_dont_even_think_about_it" translatable="false">CgkIhpOPlaMXEAIQCQ</string>
//  <!--achievement Almost there !-->
//  <string name="achievement_almost_there" translatable="false">CgkIhpOPlaMXEAIQCA</string>
//  <!--achievement Congratulation ! You've won nothing-->
//  <string name="achievement_congratulation__youve_won_nothing" translatable="false">CgkIhpOPlaMXEAIQBw</string>


//  DONE
//  <!--achievement Finir lvl bonus 1-->
//  <string name="achievement_finir_lvl_bonus_1" translatable="false">CgkIhpOPlaMXEAIQEQ</string>
//  <!--achievement Finir lvl bonus 2-->
//  <string name="achievement_finir_lvl_bonus_2" translatable="false">CgkIhpOPlaMXEAIQEg</string>
//  <!--achievement Finir lvl bonus 3-->
//  <string name="achievement_finir_lvl_bonus_3" translatable="false">CgkIhpOPlaMXEAIQEw</string>
//  <!--achievement Finir lvl bonus 4-->
//  <string name="achievement_finir_lvl_bonus_4" translatable="false">CgkIhpOPlaMXEAIQFA</string>
//  <!--achievement Finir lvl bonus 5-->
//  <string name="achievement_finir_lvl_bonus_5" translatable="false">CgkIhpOPlaMXEAIQFQ</string>
//</resources>