using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    GameObject[] ActionButtons;
    public GameObject ActionMenuLayout;

    bool[] areActionButtonsActive;

    RectTransform rectTransform;

    public bool isFixed;

    private void Start()
    {
        rectTransform = ActionMenuLayout.GetComponent<RectTransform>();

        areActionButtonsActive = new bool[ActionButtons.Length];

        for (int i = 0; i < areActionButtonsActive.Length; i++)
        {
            areActionButtonsActive[i] = false;
        }
    }

    public void Reposition()
    {
        Vector2 position = Input.mousePosition;

        float pivotX = ActionMenuLayout.transform.position.x / Screen.width;
        float pivotY = ActionMenuLayout.transform.position.y / Screen.height;

        if (isFixed == false)
        {
            rectTransform.pivot = new Vector2(pivotX, pivotY);
            ActionMenuLayout.transform.position = position;
        }
    }

    public void fixScreenPosition(bool newIsFixed)
    {
        Reposition();
        isFixed = newIsFixed;
    }
    public void setMenuActive(bool active)
    {
        ActionMenuLayout.SetActive(active);

        showActiveActionButtons();
    }
    public void OnPointerClick(PointerEventData eventData) //funktioniert
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            float leftSide = rectTransform.anchoredPosition.x - rectTransform.rect.width / 2;
            float rightSide = rectTransform.anchoredPosition.x + rectTransform.rect.width / 2;
            float topSide = rectTransform.anchoredPosition.y + rectTransform.rect.height / 2;
            float bottomSide = rectTransform.anchoredPosition.y - rectTransform.rect.height / 2;

            if (eventData.position.x >= leftSide &&
                eventData.position.x <= rightSide &&
                eventData.position.y >= bottomSide &&
                eventData.position.y <= topSide)
            {} else {
                removeActions();
                setMenuActive(false);
            }
        }
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
