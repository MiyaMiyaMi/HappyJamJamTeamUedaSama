using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    private const string BGM_PATH = "Sound/BGM";
    private const string SE_PATH = "Sound/SE";
    private const int BGM_SOURCE_NUM = 1;   //BGMを同時に流せる数
    private const int SE_SOURCE_NUM = 5;    //SEを同時に流せる数

    private const float FADE_OUT_SECONDO = 0.5f;
    [SerializeField,Header("BGMの音量(0～1.0f)")] private float BGM_VOLUME = 0.5f;
    [SerializeField,Header("SEの音量(0～1.0f)")] private float SE_VOLUME = 1.0f;

    private bool isFadeOut = false;
    private float fadeDeltaTime = 0f;
    private int nextSESourceNum = 0;
    private string currentBgmName = null;
    private string nextBgmName = null;


    // BGMは一つづつ鳴るが、SEは複数同時に鳴ることがある
    private AudioSource bgmSource;
    private List<AudioSource> seSourceList;
    private Dictionary<string, AudioClip> seClipDic;
    private Dictionary<string, AudioClip> bgmClipDic;



    new private void Awake()
    {
        for (int i = 0; i < SE_SOURCE_NUM + BGM_SOURCE_NUM; i++)
        {
            gameObject.AddComponent<AudioSource>();//ゲームｵﾌﾞｼﾞｪｸﾄにAudioSourceを追加
        }

        IEnumerable<AudioSource> audioSources = GetComponents<AudioSource>().Select(a =>//上で追加した自身のAudioSourceを全て(6個)取得
        { a.playOnAwake = false; a.volume = BGM_VOLUME; a.loop = true; return a; });//それら全てのパラメータをセットして代入

        bgmSource = audioSources.First();//配列の最初のaudioSourcesをセット
        seSourceList = audioSources.Skip(BGM_SOURCE_NUM).ToList();//BGM用のAudioSourceの数だけスキップした場所からToListでList型に変換して代入
        seSourceList.ForEach(a => { a.volume = SE_VOLUME; a.loop = false; });//foreachで回してパラメータをセット

        bgmClipDic = (Resources.LoadAll(BGM_PATH) as Object[]).ToDictionary(bgm => bgm.name, bgm => (AudioClip)bgm);//BGMを登録(ToDictionaryに変換(bgmの名前(キー),bgm本体))
        seClipDic = (Resources.LoadAll(SE_PATH) as Object[]).ToDictionary(se => se.name, se => (AudioClip)se);//BGMと同様に
    }


    /// <summary>
    /// 指定したファイル名のSEを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
    /// </summary>
    public void PlaySE(string sename)
    {
        if (seClipDic.ContainsKey(sename))//ListのseClipDicの中にseLabelと同じ名前のキーが登録されているかを検査
        {
            AudioSource se = seSourceList[nextSESourceNum];//seに新しいAudioSourceを代入


            se.PlayOneShot(seClipDic[sename]);//seを再生(seClipDicのseLabelキーを)

            nextSESourceNum = ++nextSESourceNum;//使用するAudioSouceを変える
            if (nextSESourceNum < SE_SOURCE_NUM) nextSESourceNum = 0;//一周したので0を使用するようにする
        }
        else//seLabelと同じ名前のキーが存在しない
        {
            Debug.LogError($"seClipDicに{sename}というKeyはありません");
        }
    }


    /// <summary>
    /// 指定したBGMを流す。すでに流れている場合はNextに予約し、流れているBGMをフェードアウトさせる
    /// </summary>
    public void PlayBGM(string bgmname)
    {
        if (!bgmSource.isPlaying)//曲を再生中じゃないなら
        {
            currentBgmName = bgmname;
            nextBgmName = null;//nextBGMにヌルを代入
            if (bgmClipDic.ContainsKey(bgmname))//ListのbgmClipDicの中にbgmLabelと同じ名前のキーが登録されているかを検査
            {
                bgmSource.clip = bgmClipDic[bgmname];//再生する曲をbgmLabelの曲に
            }
            else//bgmLabelと同じ名前のキーが存在しない
            {
                Debug.LogError($"bgmClipDicに{bgmname}というKeyはありません");
            }
            bgmSource.Play();//曲を再生
        }
        else if (currentBgmName != bgmname)//今再生している曲じゃなければ
        {
            isFadeOut = true;//フェードアウト実行
            nextBgmName = bgmname;//新しい曲をセット
            fadeDeltaTime = 0f;//フェードタイムを初期化
        }
    }

    /// <summary>
    /// BGMを止める
    /// </summary>
    public void StopSound()
    {
        bgmSource.Stop();
        seSourceList.ForEach(a => { a.Stop(); });
    }


    private void Update()
    {
        if (isFadeOut)//フェードアウト中なら
        {
            fadeDeltaTime += Time.deltaTime;
            bgmSource.volume = (1.0f - fadeDeltaTime / FADE_OUT_SECONDO) * BGM_VOLUME;//ボリュームを下げる

            if (fadeDeltaTime >= FADE_OUT_SECONDO)//フェードアウトタイムを超えたら
            {
                isFadeOut = false;//フェードアウト終了
                bgmSource.Stop();//曲を停止
            }
        }
        else if (nextBgmName != null)//新しい曲を再生するなら
        {
            bgmSource.volume = BGM_VOLUME;//曲のボリュームをデフォルトに戻す
            PlayBGM(nextBgmName);//曲を再生
        }
    }
}

