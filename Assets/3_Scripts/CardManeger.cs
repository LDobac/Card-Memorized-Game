using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManeger : MonoBehaviour
{
    public int cardNumMin;
    public int cardNumMax;

    private bool _canFlip = false;

    private Card[] cards;

    private void Awake()
    {
        cards = new Card[16];
    }

    private void Start()
    {
        for (int index = 0; index < 16; index++)
        {
            cards[index] = transform.GetChild(index).GetComponent<Card>();
        }
    }

    bool CompareNumInArray(ref int[] array,int num)
    {
        foreach(var element in array)
        {
            if (element == num) return true;
        }

        return false;
    }

    public void SetCardsNumber()
    {
        int[] tempNumArr = Enumerable.Repeat(-1, 16).ToArray();
        
        for (int index = 0; index < 16; index++)
        {
            int randNum = Random.Range(cardNumMin, cardNumMax);
            if (CompareNumInArray(ref tempNumArr,randNum))
            {
                index--;
                continue;
            }
            tempNumArr[index] = randNum;
            cards[index].cardNum = randNum;
        }
    }

    public void ShowCardsNumber()
    {
        for (int index = 0; index < 16; index++)
        {
            cards[index].Show();
        }
    }

    public void HideCardsNumber()
    {
        for (int index = 0; index < 16; index++)
        {
            cards[index].Hide();
        }
    }

    public bool canFlip
    {
        get
        {
            return _canFlip;
        }
        set
        {
            _canFlip = value;
        }
    }

    public Card[] GetCards()
    {
        return cards;
    }
}
