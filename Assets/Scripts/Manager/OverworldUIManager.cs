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
    GameObject dialogBox;
    [SerializeField]
    GameObject dialogText;
    [SerializeField]
    GameObject dialogActionButton1;
    [SerializeField]
    GameObject dialogActionButton2;

    [SerializeField]
    public GameObject TooltipMenu;
    [SerializeField]
    public GameObject ActionMenuCanvas;

    [SerializeField]
    GameObject resumeButton;
    [SerializeField]
    GameObject settingsButton;
    [SerializeField]
    GameObject retryButton;
    [SerializeField]
    GameObject exitButton;
    [SerializeField]
    GameObject pauseFilter;

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
        dialogActionButton1.SetActive(isDialogShown);

        //dialogActionButton2.SetActive(isDialogShown);
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

        if (currentDialog[currenDialogCount].action1 != null)
        {
            dialogActionButton1.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            dialogActionButton1.GetComponentInChildren<Button>().onClick.AddListener(currentDialog[currenDialogCount].action1.action);
            dialogActionButton1.GetComponentInChildren<TextMeshProUGUI>().text = currentDialog[currenDialogCount].action1.dialogActionText;
        }

        if (currentDialog[currenDialogCount].action2 != null)
        {
            dialogActionButton2.SetActive(true);
            dialogActionButton2.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            dialogActionButton2.GetComponentInChildren<Button>().onClick.AddListener(currentDialog[currenDialogCount].action2.action);
            dialogActionButton2.GetComponentInChildren<TextMeshProUGUI>().text = currentDialog[currenDialogCount].action2.dialogActionText;
        } else
        {
            dialogActionButton2.SetActive(false);
        }
    }

    public void endDialog()
    {
        showDialog(false);
    }

    public void showActionText(bool isActionTextShown)
    {
        actionText.SetActive(isActionTextShown);
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