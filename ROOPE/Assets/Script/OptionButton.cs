using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionButton : MonoBehaviour {
    public Sprite soundOnImage;
    public Sprite soundOffImage;
    private Button thisButton;

    void Start()
    {
        thisButton = GetComponent<Button>();

        bool muted = BackgroundMusicManager.instance.player.mute && EffectMusicManager.instance.player.mute;

        if (muted)
        {
            thisButton.image.sprite = soundOffImage;
        }
        else
        {
            thisButton.image.sprite = soundOnImage;
        }
    } 

    public void onSoundButtonClicked()
    {
        bool muted = BackgroundMusicManager.instance.player.mute && EffectMusicManager.instance.player.mute;

        if (muted)
        {
            BackgroundMusicManager.instance.player.mute = false;
            EffectMusicManager.instance.player.mute = false;
            thisButton.image.sprite = soundOnImage;
        }
        else
        {
            BackgroundMusicManager.instance.player.mute = true;
            EffectMusicManager.instance.player.mute = true;
            thisButton.image.sprite = soundOffImage;
        }
    } 

    public void onTutorialButtonClicked()
    {
        SceneManager.LoadScene("TutorialMenu");
    }
}
