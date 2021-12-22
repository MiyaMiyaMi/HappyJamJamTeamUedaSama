using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAction : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    [SerializeField,Header("スライド速度")] float speed;


    private void OnDestroy()
    {
        gameManager.GetComponent<GameManager>().GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(1,0,0) * Time.deltaTime * speed;
        if(gameObject.transform.position.x >= 1300)
        {
            Destroy(gameObject);
        }
    }
}
