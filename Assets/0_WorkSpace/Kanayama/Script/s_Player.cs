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
            //tag�m�F
            if (collision.tag == "safe")
            {
                //�}�E�X�E�N���b�N�Ő���
                if (Input.GetMouseButton(1))
                {
                    check = Check.Success;
                    Debug.Log("����");
                }
                else if(Input.GetMouseButton(0))
                {
                    check = Check.Out;
                    Debug.Log("���s");
                    HP--;
                }
                
            }
            else if(collision.tag == "out")
            {
                //�}�E�X���N���b�N�Ő���
                if (Input.GetMouseButton(0))
                {
                    check = Check.Success;
                    Debug.Log("����");
                }
                else if (Input.GetMouseButton(1))
                {
                    check = Check.Out;
                    Debug.Log("���s");
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
