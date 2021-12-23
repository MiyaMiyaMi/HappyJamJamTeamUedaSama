using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class s_Player : MonoBehaviour
{
    [SerializeField] int HP = 1;
    private int oldHp;

    Animator AnimP;
    GameObject heart;
    [SerializeField] Text zyoutai;
    [SerializeField] Text txtScore;
    [SerializeField] private Image image;
    [SerializeField] private Sprite sprite;

    private int score;

    enum Check
    {
        Null,
        Success,
        Out
    };

    Check check;

    // Start is called before the first frame update
    void Start()
    {
        AnimP = gameObject.GetComponent<Animator>();

        oldHp = HP;
        score = 0;
        txtScore.text = "SCORE : " + score.ToString("D5");

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (check == Check.Null)
        {
            //tag確認
            if (collision.tag == "safe")
            {
                //マウス右クリックで成功
                if (Input.GetMouseButton(1))
                {
                    check = Check.Success;
                    Debug.Log("成功");
                }
                else if (Input.GetMouseButton(0))
                {
                    check = Check.Out;
                    Debug.Log("失敗");
                    sprite = Resources.Load<Sprite>("Image_Student_cheating_raugh");
                    image = GetComponent<Image>();
                    image.sprite = sprite;
                }

            }
            else if (collision.tag == "out")
            {
                //マウス左クリックで成功
                if (Input.GetMouseButton(0))
                {
                    check = Check.Success;
                    Debug.Log("成功");
                    sprite = Resources.Load<Sprite>("Image_Student_cheating_raugh");
                    image = GetComponent<Image>();
                    image.sprite = sprite;
                }
                else if (Input.GetMouseButton(1))
                {
                    check = Check.Out;
                    Debug.Log("失敗");

                }
            }

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "safe")
        {
            if (check != Check.Null)
            {
                //見逃し
                if (check == Check.Success)
                {
                    score++;
                    txtScore.text = "SCORE : " + score.ToString("D5");
                }
                //肩たたき(誤審)
                else if (check == Check.Out)
                {
                    //肩たたき
                    Debug.Log("肩たたき誤審");
                    SoundManager.Instance.PlaySE("SE_Check");
                    AnimP.SetTrigger("tap.trg");
                    SoundManager.Instance.PlaySE("SE_Minus");
                    AnimP.SetTrigger("bad.trg");
                    HP--;

                }
            }
            else//見逃し
            {
                //肩たたき
                SoundManager.Instance.PlaySE("SE_Check");
                AnimP.SetTrigger("tap.trg");
                SoundManager.Instance.PlaySE("SE_Minus");
                AnimP.SetTrigger("bad.trg");
                HP--;
            }
        }

        if (collision.gameObject.tag == "out")
        {

            if (check != Check.Null)
            {
                //カンニング阻止
                if (check == Check.Success)
                {
                    //肩たたき
                    SoundManager.Instance.PlaySE("SE_Check");
                    AnimP.SetTrigger("tap.trg");
                    SoundManager.Instance.PlaySE("SE_Plus2");
                    AnimP.SetTrigger("success.trg");
                    score++;
                    txtScore.text = "SCORE : " + score.ToString("D5");

                }
                //見逃し
                else if (check == Check.Out)
                {
                    SoundManager.Instance.PlaySE("SE_Minus");
                    AnimP.SetTrigger("miss.trg");
                    HP--;
                }

            }
            else
            {
                SoundManager.Instance.PlaySE("SE_Minus");
                AnimP.SetTrigger("miss.trg");
                Debug.Log("見逃し");
                HP--;
            }
        }
        check = Check.Null;
        if (oldHp != HP)
        {
            if (SceneManager.GetActiveScene().name == "Main")
            {
                HeartManager.Instance.HpDown();

            }
            oldHp = HP;
        }



    }

    // Update is called once per frame
    void Update()
    {
        zyoutai.text = check.ToString();


        if (HP <= 0)
        {
            Debug.Log("GAMEOVER");
            DataManager.Instance.scoreD = score;
            SceneManager.LoadScene("Result");
        }
    }

}


