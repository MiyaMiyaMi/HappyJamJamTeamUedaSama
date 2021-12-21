using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_Player : MonoBehaviour
{
    [SerializeField] int HP = 1;

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
                if (Input.GetMouseButton(1))
                {
                    check = Check.Success;
                    Debug.Log("成功");
                }
                else if(Input.GetMouseButton(0))
                {
                    check = Check.Out;
                    Debug.Log("失敗");
                    HP--;
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
                    HP--;
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
                check = Check.Null;
            }
            else
            {
                HP--;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Debug.Log("GAMEOVER");
            SceneManager.LoadScene("Result");
        }
    }

}
