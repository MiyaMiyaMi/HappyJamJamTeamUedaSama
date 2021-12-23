using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartAction : MonoBehaviour
{
    [SerializeField,Header("スライド速度")] float speed;
    Text myT;
    public bool IsSet;
    private void Start()
    {
        myT = GetComponent<Text>();
    }

   
    public void Set()
    {
        IsSet = true;
    }
    private void FixedUpdate()
    {
        if (IsSet)
        {
            gameObject.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
            if (gameObject.transform.position.x >= 1400)
            {
                GameManager.Instance.GameStart();
                myT.text = null;
            }
        }
      
    }
   
}
