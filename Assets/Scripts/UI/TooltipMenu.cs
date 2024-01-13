using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipMenu : MonoBehaviour
{
    [SerializeField]
    GameObject header;
    [SerializeField]
    GameObject descriptionText;

    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }
    private void Update()
    {
        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = position;
    }
    public void setActive(bool active)
    {
        gameObject.SetActive(active);
    }
    public void changeHeaderText(string newHeaderText)
    {
        header.GetComponentInChildren<TextMeshProUGUI>().text = newHeaderText;
    }

    public void changeDescriptionText(string newDescriptionText)
    {
        descriptionText.GetComponentInChildren<TextMeshProUGUI>().text = newDescriptionText;
    }
}
