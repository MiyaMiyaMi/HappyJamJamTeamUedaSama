using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorBlinkAction : MonoBehaviour
{
    Image Blink;
    [SerializeField,Header("“_–Å‘¬“x")] float BlinkSP;
    float alfha;
    // Start is called before the first frame update
    void Start()
    {
        alfha = 1f;
        Blink = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        alfha = Mathf.Abs(Mathf.Sin(Time.time))*BlinkSP;
        Blink.color = new Color(1, 1, 1, alfha);
    }
}
