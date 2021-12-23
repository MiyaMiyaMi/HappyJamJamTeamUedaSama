using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeartManager : SingletonMonoBehaviour<HeartManager>
{
    [SerializeField] Image[] hearts;
    [SerializeField] float brinkTime;
    private int hp;
    private int oldHp;
    private bool IsBrink;
    private bool aflg;
    private float aValue;
    private Image brinkImage;
    void Start()
    {
        hp = 3;
        oldHp = hp;
        foreach (var h in hearts)
        {
            h.color = Vector4.one;
        }
    }
    public void HpDown()
    {
        aValue = brinkTime;
        if (hp != 0)
        {
            hp -= 1;
        }
    }
    public void HPUp()
    {
        hp += 1;
    }
    bool Brink(Image h)
    {
        if (aValue > 0)
        {
            h.color = new Vector4(h.color.r, h.color.g, h.color.b,
                (Mathf.FloorToInt(aValue * 100) % 2 == 0) ? 1 : 0);
            aValue -= Time.deltaTime;
            if (aValue <= 0)
            {
                h.color = new Vector4(h.color.r, h.color.g, h.color.b, 0);
                return true;
            }
        }
        return false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            HpDown();
        }
        if (brinkImage != null)
        {
            if (Brink(brinkImage))
            {
                brinkImage = null;
            }
        }
        if (hp != oldHp)
        {
            brinkImage = hearts[oldHp-1];
            oldHp = hp;
        }
    }
}
