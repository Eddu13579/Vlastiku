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

    static bool isDialogShown = false;
    static bool GameIsPaused = false;
    static bool IsInventoryShown = false;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

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

        actionText.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;

        healthbarHintergrund.GetComponent<Image>().sprite = null;

        showDialog(false);
    }

    void Update()
    {
        healthBarUpdate();

        showActionText();
    }

    public void showInventory()
    {
        IsInventoryShown = !IsInventoryShown;
        Maininventory.SetActive(IsInventoryShown);
    }

    public void showPause()
    {
        if (!GameIsPaused)
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
        dialogActionButton2.SetActive(isDialogShown);
    }

    public void changeDialogText(string newDialogText)
    {
        dialogText.GetComponentInChildren<TextMeshProUGUI>().text = newDialogText;
    }

    public void showActionText()
    {
        if(playerScript.isTalkable == true)
        {
            actionText.GetComponentInChildren<TextMeshProUGUI>().text = "Press 'E' to talk";
        }
        else
        {
            actionText.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        }

    }

    public void changeActionText(string newActionText)
    {
        actionText.GetComponentInChildren<TextMeshProUGUI>().text = newActionText;
    }

    void healthBarUpdate()
    {
        healthText.text = playerScript.currentHealth + ("/") + playerScript.maximumHealth;

        healthSlider.maxValue = playerScript.maximumHealth;
        healthSlider.value = playerScript.currentHealth;

        healthbarHintergrund.color = healthbarFillGradient.Evaluate(healthSlider.normalizedValue);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;

        resumeButton.SetActive(true);
        settingsButton.SetActive(true);
        retryButton.SetActive(true);
        exitButton.SetActive(true);

        pauseFilter.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;

        resumeButton.SetActive(false);
        settingsButton.SetActive(false);
        retryButton.SetActive(false);
        exitButton.SetActive(false);

        pauseFilter.SetActive(false);
    }

    public void Settings()
    {

    }

    public void Retry()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}