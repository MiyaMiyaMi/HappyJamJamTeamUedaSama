using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] CapsulePrefabs;

    public float GameSpeed = 0;

    [SerializeField,Header("出現する間隔を制御")]
    private float SpawnTime;

    [SerializeField,Header("ランダムを入れるための変数")]
    private int Number;


    // Update is called once per frame
    void Update()
    {
        SpawnTime -= Time.deltaTime;    // SpawnTimeから時間を減らす
        if (SpawnTime <= 0.0f)          // 0秒になれば
        {
            SpawnTime = 1.0f;           // 1秒にする

            Number = Random.Range(0, CapsulePrefabs.Length); // Random.Range(最小値、最大値)整数の場合の最大値は除外
            Instantiate(CapsulePrefabs[Number], new Vector3(10, 0, 0), Quaternion.identity);    // X座標10からランダム生成
        }
    }
}
