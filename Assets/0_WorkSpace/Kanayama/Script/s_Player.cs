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
      
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(check == Check.Null)
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
                else if(Input.GetMouseButton(0))
                {
                    check = Check.Out;
                    Debug.Log("失敗");
                }
                
            }
            else if(collision.tag == "out")
            {
                //マウス左クリックで成功
                if (Input.GetMouseButton(0))
                {
                    check = Check.Success;
                    Debug.Log("成功");
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




        if (collision.gameObject.tag == "out" || collision.gameObject.tag == "safe")
        {

            if (check != Check.Null)
            {
         
                //肩たたき
                AnimP.SetTrigger("tap.trg");
                //カンニング阻止
                if (check == Check.Success)
                {
                    AnimP.SetTrigger("success.trg");
                    //  Debug.Log("成功");
                }
                //誤審
                else if (check == Check.Out)
                {
                    AnimP.SetTrigger("bad.trg");
                    SoundManager.Instance.PlaySE("bad");
                    //  Debug.Log("失敗");
                    HP--;
                }
                check = Check.Null;
            }
            else
            {
                //見逃し
                if (Input.GetMouseButton(1))
                {
                    AnimP.SetTrigger("miss.trg");
                    Debug.Log("見逃し");
                }


                HP--;
            }
        }
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
            SceneManager.LoadScene("Result");
        }
    }

}


