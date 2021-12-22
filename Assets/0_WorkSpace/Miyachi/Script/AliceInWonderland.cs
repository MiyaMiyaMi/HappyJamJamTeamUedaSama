using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AliceInWonderland : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 
    private bool IsSelect;
    Vector2 oldScale;
    Vector2 scaleValue;
    Vector2 calcValue;
    private bool IsDown;
    RectTransform rt;
    [SerializeField, Header("�؂�ւ��^�C�~���O")] float switchValue;
    [SerializeField, Header("�X�s�[�h?")] float speed;
    public void OnPointerEnter(PointerEventData eventData)
    {
        rt = GetComponent<RectTransform>();
        oldScale = rt.localScale;
        scaleValue = oldScale;
        calcValue = oldScale / speed;
        
        IsSelect = true;
        IsDown = false;
    
    }
   

    public void OnPointerExit(PointerEventData eventData)
    {
        IsSelect = false;
    }

    private void Update()
    {
        if (IsSelect)
        {
            if (IsDown)
            {
                scaleValue -= calcValue * Time.deltaTime;
               
                if((oldScale.x - (calcValue.x * switchValue))  >  scaleValue.x){
                    IsDown = false;
                    Debug.Log("�؂�ւ�");
                }
            }
            else
            {
                //Debug.Log(scaleValue);
                Debug.Log((oldScale.x + (calcValue.x * switchValue)));
                scaleValue += calcValue * Time.deltaTime;
                if ((oldScale.x + (calcValue.x * switchValue)) < scaleValue.x)
                {
                    IsDown = true;
                }
            }
            rt.localScale = scaleValue;
        }
        else if(oldScale != scaleValue)
        {
            scaleValue = oldScale;
            rt.localScale = scaleValue;
        }
    }
}