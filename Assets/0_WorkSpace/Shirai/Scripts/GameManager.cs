using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : SingletonMonoBehaviour<GameManager>
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
    [SerializeField, Header("max")]
    public float maxGameSpeed;
    public float GameSpeed = 1.0f;

    [SerializeField,Header("何秒ごとにゲームスピードが上がるか")]
    private float ACTime;
    private float acTimeValue;
    [SerializeField, Header("出現する間隔を制御")]
    private float SpawnTime;

    [SerializeField, Header("動く値")]
    private float SpawnValue;

    [SerializeField, Header("ランダムを入れるための変数")]
    private int Number;

    private float  animSpeed;

    private void Start()
    {
        animSpeed = Player.GetComponent<Animator>().speed;
        acTimeValue = ACTime;
        SoundManager.Instance.PlayBGM("play1");
        SpawnValue = SpawnTime;
        status = Status.Tutorial;
        Player.SetActive(false);
        TutorialGO.SetActive(true);
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
        switch (status)
        {

            case Status.Tutorial:
                break;
            case Status.Play:
                SpawnValue -= Time.deltaTime * GameSpeed;    // SpawnTimeから時間を減らす
                if (SpawnValue <= 0.0f)          // 0秒になれば
                {
                    SpawnValue = SpawnTime;           // 1秒にする

                    Number = Random.Range(0, CapsulePrefabs.Length); // Random.Range(最小値、最大値)整数の場合の最大値は除外
                    Instantiate(CapsulePrefabs[Number], new Vector3(10, 0, 0), Quaternion.identity);    // X座標10からランダム生成
                }
                if (GameSpeed < maxGameSpeed)
                {
                    acTimeValue -= Time.deltaTime;
                    if (acTimeValue <= 0.0f)
                    {
                        acTimeValue = ACTime;

                        GameSpeed += 0.1f;
                        Player.GetComponent<Animator>().speed = animSpeed * GameSpeed;
                    }

                }
                break;
        }
    }
}
