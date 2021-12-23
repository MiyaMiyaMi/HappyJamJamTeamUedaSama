using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class FadeDesu : MonoBehaviour
{

    [SerializeField, Header("フェードスピード")] float fadeSpeed;
    [SerializeField, Header("フェードイメージ")] Image fadeImage;
    private bool IsFadeOut;
    private bool IsFadeIn;
    private bool IsFade;
    private bool IsFadeOutOnly;
    private string sceneName;
    private float fadeInValue;
    private float fadeOutValue;

    void Start()
    {
        IsFadeOut = false;
        IsFadeIn = false;
        IsFade = false;
        sceneName = null;
        fadeInValue = 0.0f;
        fadeOutValue = 1.0f;
    }
    public void FadeInSet()
    {
        sceneName = null;
        IsFadeOut = false;
        IsFadeIn = true;
        IsFade = true;
        fadeInValue = 0.0f;
        fadeOutValue = 1.0f;
    }
    public void FadeOutSet()
    {
        sceneName = null;
        IsFadeOut = true;
        IsFadeIn = false;
        IsFade = true;
        fadeInValue = 0.0f;
        fadeOutValue = 1.0f;
    }
    public void FadeOutOnlySet()
    {
        IsFadeOutOnly = true;
        sceneName = null;
        IsFadeOut = true;
        IsFadeIn = false;
        IsFade = true;
        fadeInValue = 0.0f;
        fadeOutValue = 1.0f;
    }
    public void FadeSceneChange(string sceneName)
    {
        if (!IsFadeIn)
        {
            IsFadeOut = false;
            IsFadeIn = true;
            IsFade = true;
            fadeInValue = 0.0f;
            fadeOutValue = 1.0f;
            this.sceneName = sceneName;
        }
    }

    private bool FadeIn()
    {
        fadeInValue += fadeSpeed * Time.deltaTime;

        fadeImage.color = new Vector4(0, 0, 0, fadeInValue);
        if (fadeInValue > 1.0f)
        {
            return true;
        }
        return false;
    }
    private bool FadeOut()
    {
        fadeOutValue -= fadeSpeed * Time.deltaTime;

        fadeImage.color = new Vector4(0, 0, 0, fadeOutValue);

        if (fadeOutValue < 0.0f)
        {
            return true;
        }
        return false;
    }
    void Update()
    {
        if (IsFade)
        {
            if (IsFadeIn)
            {
                if (FadeIn())
                {
                    IsFadeIn = false;
                    if (sceneName != null)
                    {
                        Debug.Log("シーンを移動");
                        SceneManager.LoadScene(sceneName);
                    }
                }

            }
            if (IsFadeOut)
            {
                if (FadeOut())
                {
                    if (IsFadeOutOnly)
                    {
                        IsFade = false;
                    }
                    else
                    {
                        IsFadeOut = false;
                        IsFadeIn = true;
                    }
                }
            }
            if (!IsFadeOut && !IsFadeIn)
            {
                IsFade = false;

            }
        }
    }
}


