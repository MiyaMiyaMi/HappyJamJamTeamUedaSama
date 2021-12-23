using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sprite = Resources.Load<Sprite>("");
        image = GetComponent<Image>();
        image.sprite = sprite;
    }
}
