using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject Button1;
    [SerializeField]
    GameObject Button2;
    [SerializeField]
    GameObject Button3;

    [SerializeField]
    GameObject backButton;

    [SerializeField]
    GameObject creditsText;

    private void Start()
    {
        creditsText.SetActive(false);
        backButton.GetComponentInChildren<TextMeshProUGUI>().text = "Credits";

        Button1.GetComponent<Button>().onClick.AddListener(showSaves);
        Button2.GetComponent<Button>().onClick.AddListener(Settings);
        Button3.GetComponent<Button>().onClick.AddListener(QuitGame);
        backButton.GetComponent<Button>().onClick.AddListener(showCredits);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Map1");
    }

    public void showMainMenu()
    {
        Button1.SetActive(true);
        Button2.SetActive(true);
        Button3.SetActive(true);

        Button1.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
        Button2.GetComponentInChildren<TextMeshProUGUI>().text = "Settings";
        Button3.GetComponentInChildren<TextMeshProUGUI>().text = "Quit";

        Button1.GetComponent<Button>().onClick.RemoveAllListeners();
        Button2.GetComponent<Button>().onClick.RemoveAllListeners();
        Button3.GetComponent<Button>().onClick.RemoveAllListeners();
        Button1.GetComponent<Button>().onClick.AddListener(showSaves);
        Button2.GetComponent<Button>().onClick.AddListener(Settings);
        Button3.GetComponent<Button>().onClick.AddListener(QuitGame);

        backButton.GetComponentInChildren<TextMeshProUGUI>().text = "Credits";
        backButton.GetComponent<Button>().onClick.RemoveAllListeners();
        backButton.GetComponent<Button>().onClick.AddListener(showCredits);

        creditsText.SetActive(false);
    }

    public void showCredits()
    {
        Button1.SetActive(false);
        Button2.SetActive(false);
        Button3.SetActive(false);

        backButton.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        backButton.GetComponent<Button>().onClick.RemoveAllListeners();
        backButton.GetComponent<Button>().onClick.AddListener(showMainMenu);

        creditsText.SetActive(true);
    }

    public void showSaves()
    {
        Button1.GetComponentInChildren<TextMeshProUGUI>().text = "Spielstand 1";
        Button2.GetComponentInChildren<TextMeshProUGUI>().text = "Spielstand 1";
        Button3.GetComponentInChildren<TextMeshProUGUI>().text = "Spielstand 1";

        Button1.GetComponent<Button>().onClick.RemoveAllListeners();
        Button2.GetComponent<Button>().onClick.RemoveAllListeners();
        Button3.GetComponent<Button>().onClick.RemoveAllListeners();
        Button1.GetComponent<Button>().onClick.AddListener(StartGame);
        Button2.GetComponent<Button>().onClick.AddListener(StartGame);
        Button3.GetComponent<Button>().onClick.AddListener(StartGame);

        backButton.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        backButton.GetComponent<Button>().onClick.RemoveAllListeners();
        backButton.GetComponent<Button>().onClick.AddListener(showMainMenu);
    }

    public void QuitGame()
    {
        if (UnityEditor.EditorApplication.isPlaying == true)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }

    }

    public void Settings()
    {

    }

}
