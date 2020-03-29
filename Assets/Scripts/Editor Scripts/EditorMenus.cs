using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UI;

public class EditorMenus : MonoBehaviour
{
    [Header("Editor Menus Properties")]
    public int maxStarRadius = 10;
    public int maxSquareDim = 9;

    [Header("Editor Menus Misc")]
    public TextMeshProUGUI starText;
    public TextMeshProUGUI squareTextX;
    public TextMeshProUGUI squareTextY;

    public void ChangeStarText(int type)
    {
        if(type == 0)
        {
            int num = int.Parse(starText.text);
            num++;
            num = Mathf.Clamp(num, 1, maxStarRadius);
            starText.text = num.ToString();
        }
        else
        {
            int num = int.Parse(starText.text);
            num--;
            num = Mathf.Clamp(num, 1, maxStarRadius);
            starText.text = num.ToString();
        }
    }

    public int GetStarRadius()
    {
        return int.Parse(starText.text);
    }

    public void ChangeSquareTextX(int type)
    {
        if(type == 0)
        {
            int num = int.Parse(squareTextX.text);
            num+=2;
            num = Mathf.Clamp(num, 1, maxSquareDim);
            squareTextX.text = num.ToString();
        }
        else
        {
            int num = int.Parse(squareTextX.text);
            num -= 2;
            num = Mathf.Clamp(num, 1, maxSquareDim);
            squareTextX.text = num.ToString();
        }
    }

    public void ChangeSquareTextY(int type)
    {
        if (type == 0)
        {
            int num = int.Parse(squareTextY.text);
            num += 2;
            num = Mathf.Clamp(num, 1, maxSquareDim);
            squareTextY.text = num.ToString();
        }
        else
        {
            int num = int.Parse(squareTextY.text);
            num -= 2;
            num = Mathf.Clamp(num, 1, maxSquareDim);
            squareTextY.text = num.ToString();
        }
    }

    public int[] GetSquareDimensions()
    {
        return new int[] { int.Parse(squareTextX.text), int.Parse(squareTextY.text)};
    }
}
