using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : LocalSingleton<GameManager>
{
    public Transform fruidCameraPos;
    public TextMeshPro moneyText;
    public int levelMoney;
    public Material[] grassMaterial;
    public void Money(int addMoney)
    {
        levelMoney += addMoney;
        moneyText.text = levelMoney.ToString() + "$";
    }

}
