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
    [SerializeField, Header("�`���[�g���A���p�I�u�W�F�N�g")] 
    GameObject TutorialGO;

    [SerializeField] GameObject Player;

    public float GameSpeed = 0;

    [SerializeField, Header("�o������Ԋu�𐧌�")]
    private float SpawnTime;

    [SerializeField,Header("�����l")]
    private float SpawnValue;

    [SerializeField,Header("�����_�������邽�߂̕ϐ�")]
    private int Number;

    private void Start()
    {
        SpawnValue = SpawnTime;
<<<<<<< HEAD
        status = Status.Tutorial;
        Player.SetActive(false);
        TutorialGO.SetActive(true);
=======
        status = Status.Play;
>>>>>>> 26bfc1f9a5b995080ca1bef0d9e9119d40458dcc
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
            SpawnValue -= Time.deltaTime;    // SpawnTime���玞�Ԃ����炷
            if (SpawnValue <= 0.0f)          // 0�b�ɂȂ��
            {
                SpawnValue = SpawnTime;           // 1�b�ɂ���

                Number = Random.Range(0, CapsulePrefabs.Length); // Random.Range(�ŏ��l�A�ő�l)�����̏ꍇ�̍ő�l�͏��O
                Instantiate(CapsulePrefabs[Number], new Vector3(10, 0, 0), Quaternion.identity);    // X���W10���烉���_������
//                    Debug.Log("aaa");
            }
                break;
        }
    }
}
