using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    enum TutorialCheck
    {
        Null,
        Cunning,    //カンニングしている
        Through     //カンニングしていない
    }
    TutorialCheck tutorial;

    [SerializeField, Header("速度を戻すときの値")] float speed = 3;
    int StartCnt;                   //開始までのカウント　２になるとPlay開始
    [SerializeField, Header("0:Mouse 1:MouseLeft 2:MouseRight")] GameObject[] tutorialMouse;
    [SerializeField, Header("0:TutorialOut 1:TutorialSafe 2:StratSlide")] GameObject[] tutorialObjects;
    GameObject objectSpawn;         //接触したオブジェクトの保存  
    // Start is called before the first frame update
    void Start()
    {
        tutorial = TutorialCheck.Null;
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
            objectSpawn = collision.gameObject;
            objectSpawn.GetComponent<ObjectSpawn>().Speed = 0;
            tutorialMouse[0].SetActive(true);
            tutorialMouse[1].SetActive(true);
            tutorial = TutorialCheck.Cunning;
        }
        if(collision.tag == "safe")
        {
            objectSpawn = collision.gameObject;
            objectSpawn.GetComponent<ObjectSpawn>().Speed = 0;
            tutorialMouse[0].SetActive(true);
            tutorialMouse[2].SetActive(true);
            tutorial = TutorialCheck.Through;
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
                    //マウス左クリックで成功
                    if (Input.GetMouseButtonDown(0))
                    {
                        tutorial = TutorialCheck.Null;
                        objectSpawn.GetComponent<ObjectSpawn>().Speed = speed;
                        tutorialMouse[0].SetActive(false);
                        tutorialMouse[1].SetActive(false);
                        Instantiate(tutorialObjects[1], tutorialObjects[1].transform.position, Quaternion.identity);
                        StartCnt++;
                    }

                    break;
                case TutorialCheck.Through:
                    //マウス右クリックで成功
                    if (Input.GetMouseButtonDown(1))
                    {
                        tutorial = TutorialCheck.Null;
                        tutorialMouse[0].SetActive(false);
                        tutorialMouse[2].SetActive(false);
                        objectSpawn.GetComponent<ObjectSpawn>().Speed = speed;
                        StartCnt++;
                    }
                    break;
            }
        }
        else
        {
            Instantiate(tutorialObjects[2], tutorialObjects[2].transform.position, Quaternion.identity);
        }
    }
}
