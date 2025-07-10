using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class CharacterTextReply : CharacterText
{
    [SerializeField] List<GameObject> replies;
    int currentChoice = 0;

    private void Awake()
    {
        Reset();
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        currentChoice = 0;
        foreach (var reply in replies)
        {
            reply.SetActive(false);
        }

    }
    public void AddChoice(string line, string nextId, Action<string> next)
    {
        Assert.IsTrue(currentChoice < 4);
        replies[currentChoice].GetComponent<Button>().onClick.AddListener(() =>
        {
            next?.Invoke(nextId);            // hand back to manager
        });
        replies[currentChoice].SetActive(true);
        currentChoice++;
    }
}
