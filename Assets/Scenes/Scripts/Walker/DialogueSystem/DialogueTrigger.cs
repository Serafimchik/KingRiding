using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueTrigger : MonoBehaviour
{   
    public GameObject Window;
    public Text text;
    public Text firstAnswer;
    public Text secondAnswer;
    public Text thirdAnswer;
    public Button firstButton;
    public Button secondButton;
    public Button thirdButton;
    public bool dialogueEnded = false;
    public TextAsset DialogueXML;
    public int currentNode = 0;
    public int butClicked;

    Node[] nd;
    Dialogue dialogue;

    void Start()
    {
        Window.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        firstButton.onClick.AddListener(but1);
        secondButton.onClick.AddListener(but2);
        thirdButton.onClick.AddListener(but3);
        if (dialogue == null) 
        {
            dialogue = new Dialogue();
            dialogue = Dialogue.Load(DialogueXML);
            nd = dialogue.nodes;
        }
        Window.SetActive(true);
        AnswerClicked(-1, dialogueEnded);
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        EndDialogue();
    }

    private void but1()
    {
        butClicked = 0;
        dialogueEnded = Convert.ToBoolean(dialogue.nodes[currentNode].answers[butClicked].end);
        AnswerClicked(butClicked, dialogueEnded);
    }
    private void but2()
    {
        butClicked = 1;
        dialogueEnded = Convert.ToBoolean(dialogue.nodes[currentNode].answers[butClicked].end);
        AnswerClicked(butClicked, dialogueEnded);
    }
    private void but3()
    {
        butClicked = 2;
        dialogueEnded = Convert.ToBoolean(dialogue.nodes[currentNode].answers[butClicked].end);
        AnswerClicked(butClicked, dialogueEnded);
    }
    public void AnswerClicked(int numberOfButton, bool dialogueEnded)
    {
        if (dialogueEnded == true)
        {
            Window.SetActive(false);
            return;
        }
        if (numberOfButton == -1)//для того чтобы вывести диалог в первый раз
            currentNode = 0;
        else
        {
            currentNode = dialogue.nodes[currentNode].answers[numberOfButton].nextNode;
        }

        text.text = dialogue.nodes[currentNode].Npctext;
        switch (dialogue.nodes[currentNode].answers.Length)
        {
            case 0:
                break;
            case 1:
                firstButton.enabled = true;
                firstAnswer.text = dialogue.nodes[currentNode].answers[0].text;
                secondButton.enabled = false; 
                secondAnswer.text = "";
                break;
            case 2:
                firstButton.enabled = true;
                firstAnswer.text = dialogue.nodes[currentNode].answers[0].text;
                secondButton.enabled = true;
                secondAnswer.text = dialogue.nodes[currentNode].answers[1].text;
                thirdButton.enabled = false;
                break;
            case 3:
                firstButton.enabled = true;
                firstAnswer.text = nd[currentNode].answers[0].text;
                secondButton.enabled = true;
                secondAnswer.text = nd[currentNode].answers[1].text;
                thirdButton.enabled = true;
                thirdAnswer.text = nd[currentNode].answers[2].text;
                break;
            default:
                break;
        }
    }
    public void EndDialogue()
    {
        firstButton.onClick.RemoveListener(but1);
        secondButton.onClick.RemoveListener(but2);
        thirdButton.onClick.RemoveListener(but3);
        Window.SetActive(false);
        currentNode = 0;
    }
}
