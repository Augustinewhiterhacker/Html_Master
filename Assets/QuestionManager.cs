using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public Text questionText;  // Space added between Text and questionText

    public Text scoreText;

    public Text FinalScore;

    public Button[] replyButtons;

    public QtsData qtsData;

    public GameObject Well;
    public GameObject Wrong;

    public GameObject GameFinished;

    private int currentQuestion = 0;

    private static int score = 0;

    void Start(){
        SetQuestion(currentQuestion);
        Well.SetActive(false); // Correct method usage
        Wrong.SetActive(false); // Correct method usage
        GameFinished.SetActive(false); // Correct method usage
    }

    void SetQuestion(int questionIndex)
    {
        questionText.text = qtsData.questions[questionIndex].questionText;

        foreach(Button r in replyButtons)
        {
            r.onClick.RemoveAllListeners();
        }

        for (int i = 0; i < replyButtons.Length; i++)
        {
            replyButtons[i].GetComponentInChildren<Text>().text = qtsData.questions[questionIndex].replies[i]; // Correct method usage
            int replyIndex = i; // Change to 'i' instead of '1'
            replyButtons[i].onClick.AddListener(() =>
            {
                CheckReply(replyIndex);
            });
        }
    }

    void CheckReply(int replyIndex)
    {
        if(replyIndex == qtsData.questions[currentQuestion].correctReplyIndex)
        {
            score++;
            scoreText.text = "" + score;

            // well panel
            Well.SetActive(true);
            // set active false all buttons
            foreach(Button r in replyButtons)
            {
                r.interactable = false;
            }

            // next question
            StartCoroutine(Next()); // Correct method usage
        }
        else
        {
            // wrong
            Wrong.SetActive(true);

            // set active false all buttons
            foreach(Button r in replyButtons)
            {
                r.interactable = false;
            }

            // next question
            StartCoroutine(Next()); // Correct method usage
        }
    }

    IEnumerator Next()
    {
        yield return new WaitForSeconds(2);

        currentQuestion++;

        if(currentQuestion < qtsData.questions.Length)
        {
            Reset();
        }
        else
        {
            GameFinished.SetActive(true);
            // calculate score percentage
            float scorePercentage = (float)score / qtsData.questions.Length * 100;
            // display score percentage
            FinalScore.text = "You Scored " + scorePercentage.ToString("F0") + "%"; // Correct variable name

            // display appropriate message
            if(scorePercentage < 50)
            {
                FinalScore.text += "\nGame Over";
            }
            else if(scorePercentage < 60)
            {
                FinalScore.text += "\nKeep Trying ";
            }
            else if(scorePercentage < 70)
            {
                FinalScore.text += "\nGood Job ";
            }
            else if(scorePercentage < 80)
            {
                FinalScore.text += "\nWell Done";
            }
            else
            {
                FinalScore.text += "\nYou're a genius";
            }
        }
    }

    public void Reset()
    {
        // hide well or wrong 
        Well.SetActive(false);
        Wrong.SetActive(false);

        // enable all reply buttons
        foreach(Button r in replyButtons)
        {
            r.interactable = true;
        }

        // set next question
        SetQuestion(currentQuestion);
    }
}
