using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    [SerializeField,Header("Object�̃X�s�[�h")]
    float Speed = 5;        // ���k�̓����̑���

    // Start is called before the first frame update
    void Start()
    {

        

    }

    // Update is called once per frame
    void FixedUpdate()  
    {
        transform.position -= new Vector3(Time.deltaTime * Speed, 0,0);
        if(transform.position.x <= -10)
        {
            Destroy(gameObject);
        }
    }
}