using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SweatAction : MonoBehaviour
{
    GameObject player;
    [SerializeField] Vector3 spPos;
    [SerializeField] float speed;
    SpriteRenderer sr;
    private bool IsSweat;
    private bool IsSet;
    private float a;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
        transform.parent = player.transform;
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Vector4(1, 1, 1, 0);
        IsSweat = false;
        IsSet = false;
    }
    void Sweat()
    {
        IsSweat = true;
        a = 0.0f;
        sr.color = new Vector4(1, 1, 1, a);
        transform.localPosition = spPos;
        IsSet = false;
       
    }
    private void FixedUpdate()
    {
        if (IsSweat && IsSet)
        {
            transform.localPosition += Vector3.down * speed * Time.deltaTime * GameManager.Instance.GameSpeed; ;
        }
    }
    private void Update()
    {
        if (IsSweat)
        {
            if (!IsSet)
            {
                a += Time.deltaTime * GameManager.Instance.GameSpeed;
                sr.color = new Vector4(1, 1, 1, a);
                if(a >= 1.0f)
                {
                    a = 1.0f;
                    IsSet = true;
                }
            }
        }
    }

}
