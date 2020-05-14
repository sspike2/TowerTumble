
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;



public class Leaderboards : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        ILeaderboard leaderboard = Social.CreateLeaderboard();
        leaderboard.id = "Leaderboard01";
        leaderboard.LoadScores(result =>
        {
            Debug.Log("Received " + leaderboard.scores.Length + " scores");
            foreach (IScore score in leaderboard.scores)
                Debug.Log(score);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
