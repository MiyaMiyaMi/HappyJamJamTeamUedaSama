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
        Ranking
    };
    Situation situation;

    [SerializeField] GameObject Display;
    [SerializeField] GameObject Cancel;
    [SerializeField,Header("�ő�y�[�W��")]int MaxPage;   
    [SerializeField,Header("�\������摜")]Sprite[] sprites;
    int PageCnt;        //�y�[�W�J�E���g�p


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
            Debug.Log("h�����ꂽ");
            situation = Situation.HowToPlay;
            PageCnt = 0;
            Display.GetComponent<Image>().sprite = sprites[PageCnt];
        }

    }

    public void PushRanking()
    {
        Debug.Log("h�����ꂽ");

        if (situation == Situation.Title)
            situation = Situation.Ranking;
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
                    if(PageCnt >= MaxPage)
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
