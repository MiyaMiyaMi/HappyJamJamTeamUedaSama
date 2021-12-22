using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    enum Status
    {
        Tutorial,
        Play
    }
    Status status;

    public GameObject[] CapsulePrefabs;
    [SerializeField, Header("チュートリアル用オブジェクト")] 
    GameObject TutorialGO;

    [SerializeField] GameObject Player;

    public float GameSpeed = 0f;

    private float ACTime;

    [SerializeField, Header("出現する間隔を制御")]
    private float SpawnTime;

    [SerializeField,Header("動く値")]
    private float SpawnValue;

    [SerializeField,Header("ランダムを入れるための変数")]
    private int Number;

    private void Start()
    {
        SpawnValue = SpawnTime;
        status = Status.Tutorial;
        Player.SetActive(false);
        TutorialGO.SetActive(true);
        status = Status.Play;
    }

    public void GameStart()
    {
        TutorialGO.SetActive(false);
        Player.SetActive(true);
        status = Status.Play;

    }

    // Update is called once per frame
    void Update()
    {
        switch (status) {

            case Status.Tutorial:
                break;
            case Status.Play:
            SpawnValue -= Time.deltaTime;    // SpawnTimeから時間を減らす
            if (SpawnValue <= 0.0f)          // 0秒になれば
            {
                SpawnValue = SpawnTime;           // 1秒にする

                Number = Random.Range(0, CapsulePrefabs.Length); // Random.Range(最小値、最大値)整数の場合の最大値は除外
                Instantiate(CapsulePrefabs[Number], new Vector3(10, 0, 0), Quaternion.identity);    // X座標10からランダム生成
            }
                
                ACTime += Time.deltaTime;
                if (ACTime <= 0.0f)
                {
                    ACTime = 5.0f;

                    GameSpeed += 3.0f;
                }

                break;
        }
    }
}
