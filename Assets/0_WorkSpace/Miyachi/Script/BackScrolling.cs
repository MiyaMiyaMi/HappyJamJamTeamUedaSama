using UnityEngine;

public class BackScrolling : MonoBehaviour
{
    [SerializeField] private float scrollSpeed; //�w�i���X�N���[��������X�s�[�h
    [SerializeField] private float startLine;//�w�i�̃X�N���[�����J�n����ʒu
    [SerializeField] private float deadLine; //�w�i�̃X�N���[�����I������ʒu
    [SerializeField] bool IsResult;
    public int IsStop;
    void Start()
    {
        IsStop = 1;
    }
    
    private void FixedUpdate()
    {
        if (IsResult)
        {
            transform.Translate(scrollSpeed * Time.deltaTime * IsStop , 0, 0); //x���W��scrollSpeed��������

        }
        else
        {
            transform.Translate(scrollSpeed * Time.deltaTime * IsStop * GameManager.Instance.GameSpeed, 0, 0); //x���W��scrollSpeed��������
        }
      

        if (transform.position.x < deadLine) //�����w�i��x���W���deadLine���傫���Ȃ�����
        {
            float xs = transform.position.x - deadLine;
            transform.position = new Vector3(startLine + xs, 0, 0);//�w�i��startLine�܂Ŗ߂�
        }

    }
 
}
