using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipMenu : MonoBehaviour
{
    public Item itemToDisplay;

    [SerializeField]
    GameObject TooltipNameText;
    [SerializeField]
    GameObject TooltipDescriptionText;
    [SerializeField]
    GameObject[] TooltipEffectTexts;
    [SerializeField]
    GameObject TooltipTypeText;

    bool[] areTooltipEffectsTextActive;

    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();

        areTooltipEffectsTextActive = new bool[TooltipEffectTexts.Length];

        setTooltipEffectsTextActive(false);
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

        rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = position;
    }

    public void changeDisplayedItem(Item newItemToDisplay)
    {
        itemToDisplay = newItemToDisplay;

        TooltipNameText.GetComponentInChildren<TextMeshProUGUI>().text = itemToDisplay.name;
        TooltipDescriptionText.GetComponentInChildren<TextMeshProUGUI>().text = itemToDisplay.descriptionText;

        setTooltipEffectsTextActive(false);

        switch (itemToDisplay.type)
        {
            case ItemType.Consumable:
                Consumable itemToDisplayConsumable = (Consumable)itemToDisplay;
                if (itemToDisplayConsumable.effectOnConsume != null)
                {
                    for (int i = 0; i < itemToDisplayConsumable.effectOnConsume.Length; i++)
                    {
                        if (itemToDisplayConsumable.effectOnConsume[i] != null)
                        {
                            if (itemToDisplayConsumable.effectOnConsume[i].isShownInTooltip == true)
                            {
                                for(int i2 = 0; i2 < areTooltipEffectsTextActive.Length; i2++)
                                {
                                    if (areTooltipEffectsTextActive[i2] == false)
                                    {
                                        areTooltipEffectsTextActive[i2] = true;
                                        TooltipEffectTexts[i2].GetComponentInChildren<TextMeshProUGUI>().text = itemToDisplayConsumable.effectOnConsume[i].TooltipDescription;
                                        TooltipEffectTexts[i2].SetActive(true);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                break;

            case ItemType.Armor:
                Armor itemToDisplayArmor = (Armor)itemToDisplay;
                if (itemToDisplayArmor.effectWhenWearing != null)
                {
                    for (int i = 0; i < itemToDisplayArmor.effectWhenWearing.Length; i++)
                    {
                        if (itemToDisplayArmor.effectWhenWearing[i] != null)
                        {
                            if (itemToDisplayArmor.effectWhenWearing[i].isShownInTooltip == true)
                            {
                                for (int i2 = 0; i2 < areTooltipEffectsTextActive.Length; i2++)
                                {
                                    if (areTooltipEffectsTextActive[i2] == false)
                                    {
                                        areTooltipEffectsTextActive[i2] = true;
                                        TooltipEffectTexts[i2].GetComponentInChildren<TextMeshProUGUI>().text = itemToDisplayArmor.effectWhenWearing[i].TooltipDescription;
                                        TooltipEffectTexts[i2].SetActive(true);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                break;
        }

        TooltipTypeText.GetComponentInChildren<TextMeshProUGUI>().text = itemToDisplay.type.ToString();

        setActive(true);
    }
    public void setActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void setTooltipEffectsTextActive(bool newAreTooltipEffectsTextActive)
    {
        for (int i = 0; i < TooltipEffectTexts.Length; i++)
        {
            areTooltipEffectsTextActive[i] = newAreTooltipEffectsTextActive;
            TooltipEffectTexts[i].SetActive(newAreTooltipEffectsTextActive);
        }
    }
}
