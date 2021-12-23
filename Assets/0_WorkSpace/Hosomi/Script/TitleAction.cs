using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleAction : MonoBehaviour
{
    enum Situation
    {
        Title,
        HowToPlay,
        Credit,
        Ranking
    };
    Situation situation;

    
    [SerializeField] GameObject Display;
    [SerializeField] GameObject Cancel;
    [SerializeField,Header("ルールの最大ページ数")]int RMaxPage;
    [SerializeField, Header("クレジットの最大ページ数")] int CMaxPage;
    [SerializeField,Header("表示する画像")]Sprite[] sprites;
    int PageCnt;        //ページカウント用


    // Start is called before the first frame update
    void Start()
    {
        Ready();
    }

    void Ready()
    {
        situation = Situation.Title;
        Display.SetActive(false);
        Cancel.SetActive(false);
        PageCnt = 0;
    }

    public void PushStart()
    {
        if(situation == Situation.Title)
        SceneManager.LoadScene("Main");
    }

    public void PushHowToPlay()
    {
        if (situation == Situation.Title)
        {
            situation = Situation.HowToPlay;
            PageCnt = 0;
            Display.GetComponent<Image>().sprite = sprites[PageCnt];
            Display.SetActive(true);
            Cancel.SetActive(true);
        }

    }

    public void PushRanking()
    {

        if (situation == Situation.Title)
            situation = Situation.Ranking;
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(0, 0, "RankingGetOnly");
    }

    public void PushCredit()
    {
        if (situation == Situation.Title)
        {
            situation = Situation.Credit;
            PageCnt += RMaxPage;
            Display.GetComponent<Image>().sprite = sprites[PageCnt];
            Display.SetActive(true);
            Cancel.SetActive(true);
        }

    }

    public void PushCancel()
    {
        Debug.Log("aaa");
        if(situation != Situation.Title)
        {
            situation = Situation.Title;
            Display.SetActive(false);
            Cancel.SetActive(false);
            PageCnt = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

     
        switch (situation)
        {
            case Situation.Title:
                break;
            case Situation.HowToPlay:
                if (Input.GetMouseButtonDown(0))
                {
                    PageCnt++;
                    if(PageCnt >= RMaxPage)
                    {
                        PushCancel();
                    }
                    Display.GetComponent<Image>().sprite = sprites[PageCnt];
                }
                break;
            case Situation.Credit:
                if (Input.GetMouseButtonDown(0))
                {
                    PageCnt++;
                    if (PageCnt >= RMaxPage + CMaxPage)
                    {
                        PushCancel();
                    }
                    Display.GetComponent<Image>().sprite = sprites[PageCnt];
                }
                break;
            case Situation.Ranking:
                break;
        }
    }
}
