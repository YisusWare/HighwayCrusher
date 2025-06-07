using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject PausePanel;

    [SerializeField] GameObject settingsPannel;

    [SerializeField] Slider MusicSlider;
    [SerializeField] Slider SFXSlider;

    private void Start()
    {

        MusicSlider.value = GameManager.instance.GetMusicVolume();

        SFXSlider.value = GameManager.instance.GetSFXVolume();
        PausePanel.transform.localScale = Vector2.zero;

        settingsPannel.transform.localScale = Vector2.zero;
    }
    public void PauseGame()
    {

        PausePanel.transform.LeanScale(Vector2.one, 0.3f)
            .setIgnoreTimeScale(true);
        AudioManager.instance.PauseMusic();
        GameManager.instance.PauseGame();
    }

    public void ResumeGame()
    {
        GameManager.instance.ResumeGame();
        PausePanel.transform.LeanScale(Vector2.zero, 0.3f)
            .setIgnoreTimeScale(true); 
        AudioManager.instance.ResumeMusic();
    }

    public void ExitGame()
    {
        GameManager.instance.ExitGame();
    }

    public void ShowSettingsPannel()
    {
        settingsPannel.transform.LeanScale(Vector2.one, 0.3f)
            .setIgnoreTimeScale(true);
    }

    public void HideSettingsPannel()
    {
        settingsPannel.transform.LeanScale(Vector2.zero, 0.3f)
            .setIgnoreTimeScale(true);
    }

    public void SaveMusicVolume()
    {
        AudioManager.instance.SetMusicVolume(MusicSlider.value);
        GameManager.instance.SetMusicVolume(MusicSlider.value);
    }

    public void SaveSFXVolume()
    {
        AudioManager.instance.SetSFXVolume(SFXSlider.value);
        GameManager.instance.SetSFXVolume(SFXSlider.value);
    }
}
