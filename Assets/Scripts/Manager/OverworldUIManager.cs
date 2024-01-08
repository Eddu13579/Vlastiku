using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
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
    GameObject selectedWeaponText;

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
    Text actionText;

    Player playerScript;

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

        actionText.text = string.Empty;

        healthbarHintergrund.GetComponent<Image>().sprite = null;
    }

    void Update()
    {
        healthBarUpdate();

        selectedWeaponUpdate();

        showActionText();

        
         if (Input.GetKeyDown(KeyCode.Tab))
         {
            if (IsInventoryShown==false)
            {
                IsInventoryShown = true;
                Maininventory.SetActive(true);
            }
            else if (IsInventoryShown==true)
            {
                IsInventoryShown = false;
                Maininventory.SetActive(false);
            }
         }
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
                resumeButton.SetActive(false);
                settingsButton.SetActive(false);
                retryButton.SetActive(false);
                exitButton.SetActive(false);

                pauseFilter.SetActive(false);
            }
            else
            {
                Pause();
                resumeButton.SetActive(true);
                settingsButton.SetActive(true);
                retryButton.SetActive(true);
                exitButton.SetActive(true);

                pauseFilter.SetActive(true);
            }
        }
    }

    void healthBarUpdate()
    {
        healthText.text = playerScript.currentHealth + ("/") + playerScript.maximumHealth;

        healthSlider.maxValue = playerScript.maximumHealth;
        healthSlider.value = playerScript.currentHealth;

        healthbarHintergrund.color = healthbarFillGradient.Evaluate(healthSlider.normalizedValue);
    }

    void selectedWeaponUpdate()
    {
        if (playerScript.currentlyHoldingWeapon.name != null)
        {
            selectedWeaponText.GetComponentInChildren<TextMeshProUGUI>().text = playerScript.currentlyHoldingWeapon.name;
        }
    }

    void showActionText()
    {
        if(playerScript.isTalkable == true)
        {
            actionText.text = "Press 'E' to talk";
        }
        else
        {
            actionText.text = string.Empty;
        }

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