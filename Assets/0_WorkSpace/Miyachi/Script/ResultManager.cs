using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var time = 123.4f;
        
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(time, 0,"Ranking");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            var time = 123.4f;

            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(time, 0, "RankingGetOnly");
        }
    }
}
