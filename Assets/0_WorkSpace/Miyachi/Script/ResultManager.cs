using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ResultManager : SingletonMonoBehaviour<ResultManager>
{
    const int maxStudent = 10;
    [SerializeField] float spownTime;
    [SerializeField] float Interval;
    struct StrStudent
    {
        public GameObject student;
        public bool IsSet;
    }
    private int score;
    private Transform setPoint;
    private GameObject studentPrefab;
    private List<StrStudent> students;
    private int stCount;
    private float elapsed;
    private bool IsInterval;
    void Start()
    {
        elapsed = spownTime;
        stCount = 0;
        score = 123;
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score, 0, "Ranking");
        FadeManager.Instance.FadeOutOnlySet();

        if (score != 0)
        {
            for (int i = 0; i < maxStudent; i++)
            {
                StrStudent ss = new StrStudent();
                ss.student = Instantiate(studentPrefab, setPoint.position, Quaternion.identity);
                ss.IsSet = true;
                students.Add(ss);

            }
        } 
    }
    public void OnCloseButtonClick()
    {
        SceneManager.LoadScene("Title");
    }
    // Update is called once per frame
    void Update()
    {
      
        elapsed -= Time.deltaTime;

        if (elapsed <= 0.0f)
        {

            if (IsInterval)
            {
                for (int i = 0; i < maxStudent; i++)
                {
                    if (students[i].IsSet)
                    {
                        StrStudent ss = new StrStudent();
                        ss.student = students[i].student;
                        ss.IsSet = false;
                        students[i] = ss;
                        break;
                    }
                }
                elapsed = spownTime;
                stCount++;


            }
        }
        if(stCount == score)
        {
            elapsed = Interval;
            IsInterval = true;
        }
        

       /* if (Input.GetKeyDown(KeyCode.Q))
        {
            var time = 123.4f;
        
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(time, 0,"Ranking");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            var time = 123.4f;

            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(time, 0, "RankingGetOnly");
        }*/
    }
}
