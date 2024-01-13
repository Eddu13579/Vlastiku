using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    OverworldUIManager OverworldUIManager;
    [SerializeField]
    GameObject SettingsUI;

    [SerializeField]
    GameObject SettingsBackButton;
  
    [SerializeField]
    GameObject GraphicsSelectionButton;
    [SerializeField]
    GameObject AudioSelectionButton;
    [SerializeField]
    GameObject ControlsSelectionButton;

    [SerializeField]
    GameObject GraphicsContentUI;
    [SerializeField]
    GameObject AudioContentUI;
    [SerializeField]
    GameObject ControlContentUI;

    void Start()
    {
        OverworldUIManager = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>();

        SettingsBackButton.GetComponent<Button>().onClick.AddListener(goBackToPauseMenu);

        GraphicsSelectionButton.GetComponent<Button>().onClick.AddListener(showGraphicsSettings);
        AudioSelectionButton.GetComponent<Button>().onClick.AddListener(showAudioSettings);
        ControlsSelectionButton.GetComponent<Button>().onClick.AddListener(showControlsSettings);

        SettingsUI.SetActive(false);

        GraphicsContentUI.SetActive(false);
        AudioContentUI.SetActive(false);
        ControlContentUI.SetActive(false);
    }

    public void showSettings()
    {
        SettingsUI.SetActive(true);

        showGraphicsSettings();
    }

    public void showGraphicsSettings()
    {
        GraphicsContentUI.SetActive(true);
        AudioContentUI.SetActive(false);
        ControlContentUI.SetActive(false);
    }

    public void showAudioSettings()
    {
        GraphicsContentUI.SetActive(false);
        AudioContentUI.SetActive(true);
        ControlContentUI.SetActive(false);
    }

    public void showControlsSettings()
    {
        GraphicsContentUI.SetActive(false);
        AudioContentUI.SetActive(false);
        ControlContentUI.SetActive(true);
    }
    public void goBackToPauseMenu()
    {
        SettingsUI.SetActive(false);

        OverworldUIManager.isGamePaused = false; //damit OverworldUIManager wieder ins Pausemenü geht als direkt ins Game weil gameIsPaused umgedreht war
        OverworldUIManager.showPauseScreen();
    }
}
