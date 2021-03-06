using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    [SerializeField, Header("消すX座標")] float DletePosX;

    [Header("Objectのスピード")]
    public float Speed = 5;        // 生徒の動きの速さ
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (gameObject.tag == "out")
        {
            anim.SetTrigger("out");
        }
        else
        {
            anim.SetTrigger("safe");
        }
        

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
      
    }

    // Update is called once per frame
    void FixedUpdate()  
    {
        transform.position -= new Vector3(Time.deltaTime * Speed * GameManager.Instance.GameSpeed, 0,0);
        if(transform.position.x <= DletePosX)
        {
            Destroy(gameObject);
        }
    }
}
