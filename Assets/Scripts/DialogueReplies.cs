using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class DialogueReplies: MonoBehaviour
{ 
    [SerializeField] List<Button> Replies;
    [SerializeField, Range(1, 4)] int maxOptions = 4;
    int currentChoice = 0;
    
    private void Awake()
    {
        Reset();
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        currentChoice = 0;
        foreach (var reply in Replies)
        {
            reply.gameObject.SetActive(false);
            reply.onClick.RemoveAllListeners();
        }

    }
    public void AddChoice(string line, string nextId, Action<string> next)
    {
        Assert.IsTrue(currentChoice < maxOptions);
        var button = Replies[currentChoice];
        button.onClick.RemoveAllListeners();        
        button.onClick.AddListener(() =>
        {
            next?.Invoke(nextId);            
        });
        button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = line;
        Replies[currentChoice].gameObject.SetActive(true);
        currentChoice++;
    }
}
