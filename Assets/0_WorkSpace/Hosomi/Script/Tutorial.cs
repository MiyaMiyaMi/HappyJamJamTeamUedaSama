﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : SingletonMonoBehaviour<Tutorial>
{
    enum TutorialCheck
    {
        Null,
        Cunning,    //カンニングしている
        Through     //カンニングしていない
    }
    TutorialCheck tutorial;
    [SerializeField, Header("txtStart")] GameObject txtStart;
    [SerializeField, Header("速度を戻すときの値")] float speed = 3;
    int StartCnt;                   //開始までのカウント　２になるとPlay開始
    bool StartFrg;
    [SerializeField, Header("0:Mouse 1:MouseLeft 2:MouseRight")] GameObject[] tutorialMouse;
    [SerializeField, Header("0:TutorialOut 1:TutorialSafe 2:StratSlide")] GameObject[] tutorialObjects;
    GameObject objectSpawn;         //接触したオブジェクトの保存  
    GameObject outS;
    Animator myAnim;
    [SerializeField, Header("�X�|�[�����x")] float spSpeed;
    [SerializeField] Vector2 spSPos;
    float e;
    bool IsOutS;
    GameObject outG;
    GameObject safeG;
    int stop;
    // Start is called before the first frame update
    void Start()
    {
        stop = 1;
        e = 0.0f;
        tutorial = TutorialCheck.Null;
        myAnim = GetComponent<Animator>();
        StartCnt = 0;
        StartFrg = false;

        for (int i = 0; i < tutorialObjects.Length; i++)
        {
            tutorialMouse[i].SetActive(false);
        }
        outG = Instantiate(tutorialObjects[0], spSPos, Quaternion.identity);
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
            stop = 0;
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
        if (!IsOutS)
        {
            e += Time.deltaTime * stop;
            if (e > spSpeed)
            {
                safeG = Instantiate(tutorialObjects[1], spSPos, Quaternion.identity);
               //safeG.GetComponent<ObjectSpawn>().Speed = 0;
                IsOutS = true;
            }
        }
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
                        myAnim.speed = 1;
                        stop = 1; 
                        outG.tag = "Untagged";
                        StartCnt++;
                        foreach (var b in GameObject.FindGameObjectsWithTag("back"))
                        {
                            b.GetComponent<BackScrolling>().IsStop = 1;
                        }
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
                        outS.GetComponent<ObjectSpawn>().Speed = speed;
                        myAnim.speed = 1;
                        safeG.tag = "Untagged";
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
                txtStart.GetComponent<StartAction>().Set();
                StartFrg = true;
            }
        }
    }
}
