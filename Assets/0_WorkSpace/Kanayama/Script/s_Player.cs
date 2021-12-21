using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_Player : MonoBehaviour
{
    //tag受け取り用変数

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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(check == Check.Null)
        {
            //tag確認
            if (collision.tag == "safe")
            {
                //マウス右クリックで成功
                if (Input.GetMouseButtonDown(1))
                {
                    check = Check.Success;
                    Debug.Log("成功");
                }
                else if(Input.GetMouseButtonDown(0))
                {
                    check = Check.Out;
                    Debug.Log("失敗");
                }
                
            }
            else if(collision.tag == "out")
            {
                //マウス左クリックで成功
                if (Input.GetMouseButtonDown(0))
                {
                    check = Check.Success;
                    Debug.Log("成功");
                }
                if (Input.GetMouseButtonDown(1))
                {
                    check = Check.Out;
                    Debug.Log("失敗");
                }
            }
            
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        check = Check.Null;
    }

    // Update is called once per frame
    void Update()
    {
    }

}
