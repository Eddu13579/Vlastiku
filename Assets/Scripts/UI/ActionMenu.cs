using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour
{
    [SerializeField]
    GameObject ActionButton1;
    [SerializeField]
    GameObject ActionButton2;

    RectTransform rectTransform;

    public bool isFixed;

    private void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }
    private void Update()
    {
        Reposition();
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
    public void setActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void changeAction1(ActionButtonAction newAction1)
    {
        ActionButton1.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        ActionButton1.GetComponentInChildren<Button>().onClick.AddListener(newAction1.action);
        changeActionButton1Text(newAction1.actionButtonText);
    }

    public void changeAction2(ActionButtonAction newAction2)
    {
        ActionButton2.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        ActionButton2.GetComponentInChildren<Button>().onClick.AddListener(newAction2.action);
        changeActionButton2Text(newAction2.actionButtonText);
    }

    public void changeActionButton1Text(string newActionButton1Text)
    {
        ActionButton1.GetComponentInChildren<TextMeshProUGUI>().text = newActionButton1Text;
    }

    public void changeActionButton2Text(string newActionButton2Text)
    {
        ActionButton2.GetComponentInChildren<TextMeshProUGUI>().text = newActionButton2Text;
    }
}
