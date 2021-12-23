using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    [Header("Object�̃X�s�[�h")]
    public float Speed = 5;        // ���k�̓����̑���

    // Start is called before the first frame update
    void Start()
    {

        

    }

    // Update is called once per frame
    void FixedUpdate()  
    {
        transform.position -= new Vector3(Time.deltaTime * Speed * GameManager.Instance.GameSpeed, 0,0);
        if(transform.position.x <= -12)
        {
            Destroy(gameObject);
        }
    }
}
