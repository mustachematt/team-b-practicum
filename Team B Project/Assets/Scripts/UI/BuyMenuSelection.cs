using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyMenuSelection : MonoBehaviour
{
    public BuyMenuButton _selectedButton;

    public Text _shipName;
    public Text _shipDesc;
    public Image _shipImage;
    public Slider attackSlider;
    public Slider defenseSlider;
    public Slider moveSpeedSlider;
    public Slider atkSpeedSlider;
    public Slider rangeSlider;

    public void UpdateSelection(BuyMenuButton selectedButton)
    {
        _selectedButton = selectedButton;

        _shipName.text = selectedButton.shipName;
        _shipDesc.text = selectedButton.shipDesc;
        _shipImage.sprite = selectedButton.shipImage;

        attackSlider.value = selectedButton.attackPts;
        atkSpeedSlider.value = selectedButton.attackPts;
        rangeSlider.value = selectedButton.attackPts;
        defenseSlider.value = selectedButton.defensePts;
        moveSpeedSlider.value = selectedButton.moveSpeedPts;
    }
}
