using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour
{
    [SerializeField]
    GameObject[] ActionButtons;

    bool[] areActionButtonsActive;

    RectTransform rectTransform;

    public bool isFixed;

    private void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();

        areActionButtonsActive = new bool[ActionButtons.Length];

        for (int i = 0; i < areActionButtonsActive.Length; i++)
        {
            areActionButtonsActive[i] = false;
        }
    }

    public void Reposition()
    {
        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        if (isFixed == false)
        {
            rectTransform.pivot = new Vector2(pivotX, pivotY);
            transform.position = position;
        }
    }

    public void fixScreenPosition(bool newIsFixed)
    {
        Reposition();
        isFixed = newIsFixed;
    }
    public void setMenuActive(bool active)
    {
        gameObject.SetActive(active);

        showActiveActionButtons();
    }

    public void addAction(ActionButtonAction newAction)
    {
        for (int i = 0; i < ActionButtons.Length; i++)
        {
            if (areActionButtonsActive[i] == false)
            {
                ActionButtons[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                ActionButtons[i].GetComponentInChildren<Button>().onClick.AddListener(newAction.action);
                ActionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = newAction.actionButtonText;
                areActionButtonsActive[i] = true;
                ActionButtons[i].SetActive(areActionButtonsActive[i]);
                break;
            }
        }
    }
    public void removeActions()
    {
        for (int i = 0; i < ActionButtons.Length; i++)
        {
            ActionButtons[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            areActionButtonsActive[i] = false;
        }
    }
    public void showActiveActionButtons() //unnötig?
    {
        for (int i = 0; i < ActionButtons.Length; i++)
        {
            ActionButtons[i].SetActive(areActionButtonsActive[i]);
        }
    }
}
