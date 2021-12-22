using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_Player : MonoBehaviour
{
    [SerializeField] int HP = 1;

    Animator AnimP;
    GameObject heart;


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
        
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(check == Check.Null)
        {
            //tagmF
            if (collision.tag == "safe")
            {
                //}EXENbNΕ¬χ
                if (Input.GetMouseButton(1))
                {
                    check = Check.Success;
                    Debug.Log("¬χ");
                }
                else if(Input.GetMouseButton(0))
                {
                    check = Check.Out;
                    Debug.Log("Έs");
                }
                
            }
            else if(collision.tag == "out")
            {
                //}EXΆNbNΕ¬χ
                if (Input.GetMouseButton(0))
                {
                    check = Check.Success;
                    Debug.Log("¬χ");
                }
                else if (Input.GetMouseButton(1))
                {
                    check = Check.Out;
                    Debug.Log("Έs");
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
                //¨½½«
                AnimP.SetTrigger("tap.trg");
                //JjOj~
                if (check == Check.Success)
                {
                    AnimP.SetTrigger("success.trg");
                  //  Debug.Log("¬χ");
                }
                //λR
                else if (check == Check.Out)
                {
                    AnimP.SetTrigger("bad.trg");
                    SoundManager.Instance.PlaySE("bad");
                  //  Debug.Log("Έs");
                    HP--;
                }
            }
            else
            {
                //©¦΅
                if (Input.GetMouseButton(1))
                {
                    AnimP.SetTrigger("miss.trg");
                    Debug.Log("©¦΅");
                }

                
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


