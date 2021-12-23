using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartAction : MonoBehaviour
{
    [SerializeField,Header("�X���C�h���x")] float speed;
    [SerializeField, Header("�����鎞��")] float kieruzikann;
    Text myT;
    public bool IsSet;
    float elapsed;

    private void Start()
    {
        elapsed = 0.0f;
        myT = GetComponent<Text>();
      
    }

   
    public void Set()
    {
        IsSet = true;
        SoundManager.Instance.PlaySE("chaim2_");
    }
    private void FixedUpdate()
    {
        if (IsSet)
        {
            elapsed += Time.deltaTime;
            gameObject.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
            if(elapsed >= kieruzikann)
            {
                GameManager.Instance.GameStart();
                IsSet = false;
                myT.text = null;
            }
        }
      
    }
   
}
