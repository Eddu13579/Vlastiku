using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverworldUIManager : MonoBehaviour
{
    [SerializeField]
    Slider healthSlider;
    [SerializeField]
    Image healthbarHintergrund;
    [SerializeField]
    Text healthText;
    [SerializeField]
    Gradient healthbarFillGradient = new Gradient();

    [SerializeField]
    GameObject dialogBox, dialogText, ShopSlots;
    [SerializeField]
    GameObject[] dialogActionButtons;

    [SerializeField]
    public GameObject TooltipMenu;
    [SerializeField]
    public GameObject ActionMenuCanvas;

    [SerializeField]
    GameObject resumeButton, settingsButton, retryButton, exitButton, pauseFilter;

    [SerializeField]
    GameObject Maininventory;

    [SerializeField]
    GameObject actionText;

    Player playerScript;
    SettingsUIManager SettingsUIManager;

    DialogLine[] currentDialog;
    int currenDialogCount;

    static bool isDialogShown = false;
    static public bool isGamePaused = false;
    static bool IsInventoryShown = false;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        SettingsUIManager = GameObject.FindGameObjectWithTag("SettingsUIManager").GetComponent<SettingsUIManager>();

        resumeButton.GetComponentInChildren<Button>().onClick.AddListener(Resume);
        settingsButton.GetComponentInChildren<Button>().onClick.AddListener(Settings);
        retryButton.GetComponentInChildren<Button>().onClick.AddListener(Retry);
        exitButton.GetComponentInChildren<Button>().onClick.AddListener(Exit);

        resumeButton.SetActive(false);
        settingsButton.SetActive(false);
        retryButton.SetActive(false);
        exitButton.SetActive(false);

        pauseFilter.SetActive(false);

        Maininventory.SetActive(false);

        actionText.SetActive(false);

        for (int i = 0; i < dialogActionButtons.Length; i++)
        {
            dialogActionButtons[i].SetActive(false);
        }

            TooltipMenu.SetActive(false);
        ActionMenuCanvas.GetComponent<ActionMenu>().ActionMenuLayout.SetActive(false);

        healthbarHintergrund.GetComponent<Image>().sprite = null;

        showDialog(false);
    }

    void Update()
    {
        healthBarUpdate(); //eigentlich unnötig, kann nur bei jedem schaden geupdatet werden
    }

    public void showInventory()
    {
        IsInventoryShown = !IsInventoryShown;
        Maininventory.SetActive(IsInventoryShown);
    }

    public void showPauseScreen()
    {
        if (!isGamePaused)
        {
            Pause();
        } else
        {
            Resume();
        }
    }

    public void showDialog(bool newIsDialogShown)
    {
        isDialogShown = newIsDialogShown;

        dialogBox.SetActive(isDialogShown);
        dialogText.SetActive(isDialogShown);
    }

    public void startDialog(DialogLine[] newDialog)
    {
        currentDialog = newDialog;
        currenDialogCount = 0;
        updateDialogUI();

        showDialog(true);
    }

    public void nextDialog()
    {
        currenDialogCount++;
        updateDialogUI();
    }

    public void updateDialogUI()
    {
        dialogText.GetComponentInChildren<TextMeshProUGUI>().text = currentDialog[currenDialogCount].text;

        int currentAction = 0;

        for (int i = 0; i < dialogActionButtons.Length; i++)
        {
            dialogActionButtons[i].SetActive(false);
            if (currentDialog[currenDialogCount].actions[currentAction] != null)
            {
                dialogActionButtons[i].SetActive(true);
                dialogActionButtons[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                dialogActionButtons[i].GetComponentInChildren<Button>().onClick.AddListener(currentDialog[currenDialogCount].actions[currentAction].action);
                dialogActionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentDialog[currenDialogCount].actions[currentAction].dialogActionText;
                currentAction++;
            }
        }
    }

    public void endDialog()
    {
        showDialog(false);
    }

    public void showActionText(bool isActionTextShown)
    {
        if (actionText != null)
        {
            actionText.SetActive(isActionTextShown);
        }
    }

    public void changeActionText(string newActionText)
    {
        actionText.GetComponentInChildren<TextMeshProUGUI>().text = newActionText;
    }

    void healthBarUpdate()
    {
        healthText.text = ("  ") + playerScript.currentHealth + ("/") + playerScript.maximumHealth;

        healthSlider.maxValue = playerScript.maximumHealth;
        healthSlider.value = playerScript.currentHealth;

        healthbarHintergrund.color = healthbarFillGradient.Evaluate(healthSlider.normalizedValue);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isGamePaused = true;
        playerScript.isEnabled = false;

        resumeButton.SetActive(true);
        settingsButton.SetActive(true);
        retryButton.SetActive(true);
        exitButton.SetActive(true);

        pauseFilter.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        playerScript.isEnabled = true;

        resumeButton.SetActive(false);
        settingsButton.SetActive(false);
        retryButton.SetActive(false);
        exitButton.SetActive(false);

        pauseFilter.SetActive(false);
    }

    public void Settings()
    {
        resumeButton.SetActive(false);
        settingsButton.SetActive(false);
        retryButton.SetActive(false);
        exitButton.SetActive(false);

        SettingsUIManager.showSettings();
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        playerScript.isEnabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}