using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : SingletonMonoBehaviour<Tutorial>
{
    enum TutorialCheck
    {
        Null,
        Cunning,    //�J���j���O���Ă���
        Through     //�J���j���O���Ă��Ȃ�
    }
    TutorialCheck tutorial;

    [SerializeField, Header("���x��߂��Ƃ��̒l")] float speed = 3;
    int StartCnt;                   //�J�n�܂ł̃J�E���g�@�Q�ɂȂ��Play�J�n
    bool StartFrg;
    [SerializeField, Header("0:Mouse 1:MouseLeft 2:MouseRight")] GameObject[] tutorialMouse;
    [SerializeField, Header("0:TutorialOut 1:TutorialSafe 2:StratSlide")] GameObject[] tutorialObjects;
    GameObject objectSpawn;         //�ڐG�����I�u�W�F�N�g�̕ۑ�  
    GameObject outS;
    Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {

        tutorial = TutorialCheck.Null;
        myAnim = GetComponent<Animator>();
        StartCnt = 0;
        StartFrg = false;

        for (int i = 0; i < tutorialObjects.Length; i++)
        {
            tutorialMouse[i].SetActive(false);
        }
        Instantiate(tutorialObjects[0], tutorialObjects[0].transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "out")
        {
            outS = collision.gameObject;
            objectSpawn = collision.gameObject;
            objectSpawn.GetComponent<ObjectSpawn>().Speed = 0;
            tutorialMouse[0].SetActive(true);
            tutorialMouse[1].SetActive(true);
            tutorial = TutorialCheck.Cunning;
            myAnim.speed = 0;
            foreach (var b in GameObject.FindGameObjectsWithTag("back"))
            {
                b.GetComponent<BackScrolling>().IsStop = 0;
            }
        }
        if(collision.tag == "safe")
        {
            outS.GetComponent<ObjectSpawn>().Speed = 0;
            objectSpawn = collision.gameObject;
            objectSpawn.GetComponent<ObjectSpawn>().Speed = 0;
            tutorialMouse[0].SetActive(true);
            tutorialMouse[2].SetActive(true);
            tutorial = TutorialCheck.Through;
            myAnim.speed = 0;
            foreach (var b in GameObject.FindGameObjectsWithTag("back"))
            {
                b.GetComponent<BackScrolling>().IsStop = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (StartCnt < 2)
        {
            switch (tutorial)
            {
                case TutorialCheck.Null:
                    break;
                case TutorialCheck.Cunning:
                    //�}�E�X���N���b�N�Ő���
                    if (Input.GetMouseButtonDown(0))
                    {
                        tutorial = TutorialCheck.Null;
                        objectSpawn.GetComponent<ObjectSpawn>().Speed = speed;
                        tutorialMouse[0].SetActive(false);
                        tutorialMouse[1].SetActive(false);
                        myAnim.speed = 1;
                        Instantiate(tutorialObjects[1], tutorialObjects[1].transform.position, Quaternion.identity);
                        StartCnt++;
                        foreach (var b in GameObject.FindGameObjectsWithTag("back"))
                        {
                            b.GetComponent<BackScrolling>().IsStop = 1;
                        }
                    }
             
                    break;
                case TutorialCheck.Through:
                    //�}�E�X�E�N���b�N�Ő���
                    if (Input.GetMouseButtonDown(1))
                    {
                        tutorial = TutorialCheck.Null;
                        tutorialMouse[0].SetActive(false);
                        tutorialMouse[2].SetActive(false);
                        objectSpawn.GetComponent<ObjectSpawn>().Speed = speed;
                        outS.GetComponent<ObjectSpawn>().Speed = speed;
                        myAnim.speed = 1;
                        StartCnt++;
                        foreach (var b in GameObject.FindGameObjectsWithTag("back"))
                        {
                            b.GetComponent<BackScrolling>().IsStop = 1;
                        }
                    }
                    break;
            }
        }
        else 
        {
            if (!StartFrg)
            {
                Instantiate(tutorialObjects[2], tutorialObjects[2].transform.position, Quaternion.identity);
                StartFrg = true;
            }
        }
    }
}
