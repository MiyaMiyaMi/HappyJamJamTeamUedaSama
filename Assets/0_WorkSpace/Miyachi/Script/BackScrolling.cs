using UnityEngine;

public class BackScrolling : MonoBehaviour
{
    [SerializeField] private float scrollSpeed; //背景をスクロールさせるスピード
    [SerializeField] private float startLine;//背景のスクロールを開始する位置
    [SerializeField] private float deadLine; //背景のスクロールが終了する位置
    public int IsStop;
    void Start()
    {
        IsStop = 1;
    }
    
    private void FixedUpdate()
    {
        transform.Translate(scrollSpeed * Time.deltaTime * IsStop , 0, 0); //x座標をscrollSpeed分動かす

        if (transform.position.x < deadLine) //もし背景のx座標よりdeadLineが大きくなったら
        {
            transform.position = new Vector3(startLine, 0, 0);//背景をstartLineまで戻す
        }

    }
 
}
