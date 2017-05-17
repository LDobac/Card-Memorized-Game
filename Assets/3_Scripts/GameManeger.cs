using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManeger : MonoBehaviour
{
    public float originRememberTime = 0.0f;
    public float originAnswerTime = 0.0f;
    public float seeingTimeRate = 0.0f;

    public Button stageStartButton = null;
    public Button nextStageButton = null;
    public GameObject gameoverUI = null;
    public GameObject answerInformation = null;
    public Text levelText = null;
    public Text answerCountText = null;
    public Text informationText = null;

    private int maxAnswer = 1;
    private int _level = 1;
    private int answer = -1;
    private int answerCount = 0;

    private bool[] displayAnswer;

    private bool isSeeingtime = false;
    private bool isAnswerTime = false;

    private CardManeger cardManeger = null;
    private Timer timer = null;

    private void Awake()
    {
        displayAnswer = new bool[16];
        System.Array.Clear(displayAnswer, 0, displayAnswer.Length);
    }

    private void Start()
    {
        cardManeger = GameObject.FindGameObjectWithTag("Card Maneger").GetComponent<CardManeger>();
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();

        informationText.text = "아래의 버튼을 클릭하면 시작합니다.";
    }

    private void Update()
    {
        if(isSeeingtime)
        {
            if(timer.isDone)
            {
                isSeeingtime = false;
                cardManeger.HideCardsNumber();
                answerInformation.SetActive(true);
                answerInformation.transform.GetChild(0).GetComponent<Text>().text = 
                    "이번 레벨에서는 " + (originAnswerTime - (seeingTimeRate * (level - 1))).ToString() + "초 만큼의 정답 시간이 주어집니다.";

                informationText.text = "준비";
            }
        }

        if(isAnswerTime)
        {
            if(timer.isDone)
            {
                isAnswerTime = false;
                StageFailed();
            }
        }
    }

    public void StageStart()
    {
        isSeeingtime = true;
        isAnswerTime = false;

        answerCountText.text = "0 / " + maxAnswer.ToString();

        informationText.text = "각 카드의 숫자를 외우십시오.";
        levelText.text = _level.ToString() + " Level";

        cardManeger.SetCardsNumber();
        cardManeger.ShowCardsNumber();

        float seeingTime = originRememberTime - ((_level - 1) * seeingTimeRate);
        if (seeingTime <= 0.7f) seeingTime = 0.7f;
        timer.StartTimer(seeingTime);
    }

    public void ResetStage()
    {
        _level++;
        maxAnswer++;

        isSeeingtime = false;
        isAnswerTime = false;

        cardManeger.canFlip = false;
        cardManeger.HideCardsNumber();

        for(int index = 0; index < 16; index++)
        {
            displayAnswer[index] = false;
        }

        answer = -1;
        answerCount = 0;

        stageStartButton.gameObject.SetActive(true);
        timer.Resume();
        timer.StartTimer(0.0f);

        informationText.text = "아래의 버튼을 클릭하면 시작합니다.";
    }

    public void StartAnswerTime()
    {
        float time = originAnswerTime - (seeingTimeRate * (level - 1));

        isAnswerTime = true;
        cardManeger.canFlip = true;

        timer.StartTimer(time);

        SetAnswer();
    }

    public void SetAnswer()
    {
        for (;;)
        {
            int rand = Random.Range(0, 15);
            if (displayAnswer[rand]) continue;

            answer = cardManeger.GetCards()[rand].cardNum;

            displayAnswer[rand] = true;
            informationText.text = answer.ToString();

            break;
        }
    }

    public void StageClear()
    {
        timer.Pause();

        informationText.text = level.ToString() + " 레벨 클리어";
        cardManeger.ShowCardsNumber();
        cardManeger.canFlip = false;

        nextStageButton.gameObject.SetActive(true);
    }

    public void StageFailed()
    {
        cardManeger.ShowCardsNumber();

        isSeeingtime = false;
        isAnswerTime = false;

        gameoverUI.SetActive(true);
        gameoverUI.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { SceneManager.LoadScene("GameScene"); });
        gameoverUI.transform.GetChild(1).GetComponent<Text>().text = "게임 오버! \n" + level.ToString() + " 레벨 달성!";
    }

    public void OnClickedCard(Card card)
    {
        if (isAnswerTime)
        {
            if (card.cardNum == answer)
            {
                answerCount++;
                answerCountText.text = answerCount.ToString() + " / " + maxAnswer.ToString();

                if (answerCount == maxAnswer)
                {
                    StageClear();
                }
                else
                {
                    SetAnswer();
                }
            }
            else
            {
                StageFailed();
            }
        }
    }

    public int level
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
        }
    }

}
