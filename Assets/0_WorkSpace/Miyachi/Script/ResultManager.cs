using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ResultManager : SingletonMonoBehaviour<ResultManager>
{
    const int maxSetStudent = 10;
    [SerializeField] float spownTime;
    [SerializeField] float interval;
    [SerializeField] float stSpeed;
    public struct StrStudent
    {
        public GameObject student;
        public bool IsSet;

        public StrStudent(GameObject a, bool b) { student = a; IsSet = b; }
    }
    private int score;
    [SerializeField] private Transform setPoint;
    [SerializeField] private GameObject studentPrefab;
    private List<StrStudent> students;
    private int stCount;
    private float timeValue;
    private bool IsInterval;
    void Start()
    {
        SoundManager.Instance.PlaySE("chaim2_");
        students = new List<StrStudent>();
        timeValue = spownTime;
        stCount = 0;
        score = 10;// DataManager.Instance.scoreD;
       
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score, 0, "Ranking");
       
        if (score != 0)
        {
            for (int i = 0; i < maxSetStudent; i++)
            {
                StrStudent ss  = new StrStudent();
                ss.student = Instantiate(studentPrefab, setPoint.position, Quaternion.identity);
                ss.IsSet = true;
                students.Add(ss);

            }
        } 
    }
    public void OnCloseButtonClick()
    {
        FadeManager.Instance.FadeSceneChange("Title");
        //SceneManager.LoadScene("Title");
    }
    bool MoveStudent(StrStudent st)
    {
        st.student.transform.position -= Vector3.left *  Time.deltaTime * stSpeed;
        if(st.student.transform.position.x < -12)
        {
            st.student.transform.position = setPoint.position;
          //  st.IsSet = true;
            return true;
        }
        return false;
    }
    private void FixedUpdate()
    {
        if (score != 0)
        {
            for (int i = 0; i < maxSetStudent; i++)
            {
                if (!students[i].IsSet)
                {
                    if (MoveStudent(students[i]))
                    {
                        StrStudent ss = new StrStudent();
                        ss.student = students[i].student;
                        ss.IsSet = true;
                        students[i] = ss;
                    }
                }
            }
        }
    }
    void Update()
    {
        if (score != 0)
        {
            timeValue -= Time.deltaTime;

            if (timeValue <= 0.0f)
            {
                if (!IsInterval)
                {
                    for (int i = 0; i < maxSetStudent; i++)
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
                    timeValue = spownTime;
                    stCount++;
                }
                else
                {
                    IsInterval = false;
                    timeValue = spownTime;

                }

            }
            if (stCount == score)
            {
                timeValue = interval;
                IsInterval = true;
                stCount = 0;
            }


        }
    }
}
