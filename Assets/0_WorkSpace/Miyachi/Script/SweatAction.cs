using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SweatAction : MonoBehaviour
{
    GameObject player;
    [SerializeField] Vector3 spPos;
    [SerializeField] float speed;
    [SerializeField, Header("è¡Ç¶ÇÈà íu")] float destroyPosY;

    SpriteRenderer sr;
    private bool IsSweat;
    private bool IsSet;
    private float a;
    void Start()
    {
      
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Vector4(1, 1, 1, 0);
        IsSweat = false;
        IsSet = false;
    }
    public void Sweat()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            transform.position = player.transform.position;
            transform.parent = player.transform;
        }
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
            transform.localPosition += Vector3.down * speed * Time.deltaTime * GameManager.Instance.GameSpeed; 
            if(destroyPosY < transform.position.y)
            {
                IsSweat = false;
            }
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
        if(!IsSweat && IsSet)
        {
            a -= Time.deltaTime * GameManager.Instance.GameSpeed;
            sr.color = new Vector4(1, 1, 1, a);
            if (a <= 0.0f)
            {
                a = 0.0f;
                
            }
        }
    }

}
