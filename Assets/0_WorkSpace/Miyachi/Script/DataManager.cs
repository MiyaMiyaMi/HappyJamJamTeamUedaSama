using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class DataManager : SingletonMonoBehaviour<DataManager>
{
    public int scoreD;

    void Start()
    {
        DontDestroyOnLoad(gameObject);//シーンをまたいで存在する
    }
    //プロパティ
    public int GetSetScore
    {
        get { return scoreD; }
        set { scoreD = value; }
    }


}
