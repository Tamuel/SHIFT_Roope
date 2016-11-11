using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public GameObject BgmManager;
    public GameObject EfxManager;
    public AudioClip mainMenuBgm;

    // Use this for initialization
    void Awake()
    {
        if (!BackgroundMusicManager.instance)
        {
            Instantiate(BgmManager);
        }
        if (!EffectMusicManager.instance)
        {
            Instantiate(EfxManager);
        }

        BackgroundMusicManager.instance.PlaySingle(mainMenuBgm);
    }
}
