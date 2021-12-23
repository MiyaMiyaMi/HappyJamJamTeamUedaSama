using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingAction : MonoBehaviour
{

    void Start()
    {
        GameObject.FindGameObjectWithTag("Fade").GetComponent<FadeDesu>().FadeOutOnlySet();
    }

 
}
