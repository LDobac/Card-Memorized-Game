using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Text text = null;

    private int _cardNum = -1;
    private bool _onShow = false;

    private bool fuc = false; 

    private Animator flipingAnimation = null;
    private GameManeger gameManeger = null;

    private void Start()
    {
        flipingAnimation = GetComponent<Animator>();
        gameManeger = GameObject.FindGameObjectWithTag("Game Maneger").GetComponent<GameManeger>();
    }

    private void OnMouseEnter()
    {
        Debug.Log(_cardNum);
    }

    private void OnMouseDown()
    {
        if (transform.parent.GetComponent<CardManeger>().canFlip)
        {
            if (!onShow)
            {
                Show();
                gameManeger.OnClickedCard(GetComponent<Card>());
            }
        }
    }

    public void Show()
    {
        _onShow = true;
        flipingAnimation.SetBool("Show",true);

        text.text = _cardNum.ToString();
        text.gameObject.SetActive(true);
        text.GetComponent<Animator>().SetBool("Show", true);
    }

    public void Hide()
    {
        _onShow = false;
        flipingAnimation.SetBool("Show", false);

        text.GetComponent<Animator>().SetBool("Show", false);
        fuc = true;
    }
    private void Update()
    {
        if (text.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CardNumHideAnimation") && fuc)
        {
            fuc = false;
            text.gameObject.SetActive(false);
        }
    }

    public int cardNum
    {
        get
        {
            return _cardNum;
        }
        set
        {
            _cardNum = value;
        }
    }

    public bool onShow
    {
        get
        {
            return _onShow;
        }
    }
}
