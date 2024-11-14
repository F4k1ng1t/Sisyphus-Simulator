using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using PlayFab.MultiplayerModels;
//using System.Diagnostics;

public class Leaderboard : MonoBehaviour
{
    public GameObject leaderboardCanvas;
    public GameObject[] leaderboardEntries;

    public static Leaderboard instance;
    void Awake() { instance = this; }
    public void OnLoggedIn()
    {
        //leaderboardCanvas.SetActive(true);
        Debug.Log("logged in");
        DisplayLeaderboard();
    }
    public void DisplayLeaderboard()
    {
        GetLeaderboardRequest getLeaderboardRequest = new GetLeaderboardRequest
        {
            StatisticName = "HighestPoint",
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(getLeaderboardRequest,
        result => UpdateLeaderboardUI(result.Leaderboard),
        error => Debug.Log(error.ErrorMessage)
        );
    }
    void UpdateLeaderboardUI(List<PlayerLeaderboardEntry> leaderboard)
    {
        for (int x = 0; x < leaderboardEntries.Length; x++)
        {
            leaderboardEntries[x].SetActive(x < leaderboard.Count);
            if (x >= leaderboard.Count) continue;
            leaderboardEntries[x].transform.Find("PlayerName").GetComponent<TextMeshProUGUI>(
            ).text = (leaderboard[x].Position + 1) + ". " + leaderboard[x].DisplayName;
            leaderboardEntries[x].transform.Find("ScoreText").GetComponent<TextMeshProUGUI>()
            .text = ((float)leaderboard[x].StatValue).ToString() + "m";
        }
    }
    public void SetLeaderboardEntry(int newScore)
    {
        bool useLegacyMethod = false;
        if (useLegacyMethod)
        {
            ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest
            {
                FunctionName = "UpdateHighscore",
                FunctionParameter = new { score = newScore }
            };
            PlayFabClientAPI.ExecuteCloudScript(request,
            result =>
            {
                Debug.Log(result);
                //Debug.Log("SUCCESS");
                //Debug.Log(result.FunctionName);
                //Debug.Log(result.FunctionResult);
                //Debug.Log(result.FunctionResultTooLarge);
                //Debug.Log(result.Error);
                DisplayLeaderboard();
                Debug.Log(result.ToJson());
            },
            error =>
            {
                Debug.Log(error.ErrorMessage);
                Debug.Log("ERROR");
            }
            );
        }
        else
        {
            // NOTE: by default, clients can't update player statistics
            // So for the code below to succeed:
            // 1. Log into PlayFab (from your web browser)
            // 2. Select your Title.
            // 3. Select Settings from the left-menu.
            // 4. Select the API Features tab.
            // 5. Find and activate Allow client to post player statistics.
            // (source:https://learn.microsoft.com/en-us/gaming/playfab/features/data/playerdata/using-player-statistics)

            // Old Code
            //    PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
            //    {
            //        // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            //        Statistics = new List<StatisticUpdate>
            //{
            //new StatisticUpdate { StatisticName = "HighestPoint", Value = newScore },
            //}
            //    },
            //    result => { Debug.Log("User statistics updated"); },
            //    error => { Debug.LogError(error.GenerateErrorReport()); }
            //    );
            bool isHighest = false;
            //this should prevent overwriting times with worse time 
            PlayFabClientAPI.GetPlayerStatistics(new GetPlayerStatisticsRequest(),
                result =>
                {
                    foreach (var eachStat in result.Statistics)
                    {
                        if (eachStat.StatisticName == "HighestPoint")
                        {
                            isHighest = true;
                            Debug.Log("old: " + eachStat.Value + "\nNew: " + newScore);
                            Debug.Log("Mark 2: " + isHighest);
                            if (eachStat.Value < newScore) //this one is fine, it's the other that sucks
                            {
                                Debug.Log("update");
                                UpdatePlayerStatisics(newScore);
                            }
                        }
                    }
                    if (!isHighest)
                    {
                        Debug.Log("initialize");
                        UpdatePlayerStatisics(newScore);
                    }
                },
                error => { Debug.Log(error.ErrorMessage); }
            );
        }
    }
    private void UpdatePlayerStatisics(int newScore)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            // probably for adjusting players that move on the statistics
            Statistics = new List<StatisticUpdate>
                    {
                        new StatisticUpdate { StatisticName = "HighestPoint", Value = newScore },
                    }
        },
            result => { Debug.Log("User statistics updated"); },
            error => { Debug.LogError(error.GenerateErrorReport()); }
        );
    }
}
