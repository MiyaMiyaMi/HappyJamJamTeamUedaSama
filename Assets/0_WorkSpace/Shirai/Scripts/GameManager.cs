using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] CapsulePrefabs;

    public float GameSpeed = 0;

    [SerializeField,Header("�o������Ԋu�𐧌�")]
    private float SpawnTime;

    [SerializeField,Header("�����_�������邽�߂̕ϐ�")]
    private int Number;


    // Update is called once per frame
    void Update()
    {
        SpawnTime -= Time.deltaTime;    // SpawnTime���玞�Ԃ����炷
        if (SpawnTime <= 0.0f)          // 0�b�ɂȂ��
        {
            SpawnTime = 1.0f;           // 1�b�ɂ���

            Number = Random.Range(0, CapsulePrefabs.Length); // Random.Range(�ŏ��l�A�ő�l)�����̏ꍇ�̍ő�l�͏��O
            Instantiate(CapsulePrefabs[Number], new Vector3(10, 0, 0), Quaternion.identity);    // X���W10���烉���_������
        }
    }
}