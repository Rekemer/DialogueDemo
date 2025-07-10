using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class DialogueState : BaseFrameState
{
    Character m_Character;
    bool m_IsLeft;
    public DialogueState(Character character, bool isLeft)
    {
        m_Character = character;
        m_IsLeft = isLeft;
    }

    public override void OnEnter()
    {

        base.OnEnter();
        if (m_IsLeft)
            m_Character.SetLeft();
        else
            m_Character.SetRight();
    }

    public override void Show()
    {
        m_CharacterText.SetLine(m_Text);
        m_CharacterText.SetName(m_CharacterName);
    }
}
public class TextState : BaseFrameState
{
   

    public override void Show()
    {
        m_CharacterText.SetLine(m_Text);
        m_CharacterText.SetName(m_CharacterName);
    }
}

public class FinalState : BaseFrameState
{
    
    public override void Show()
    {
        m_CharacterText.SetLine(m_Text);
        m_CharacterText.SetName(m_CharacterName);
    }
}

public class OptionState : BaseFrameState
{
    Button m_DefaultButton;
    CharacterTextReply m_Replies;
    List<Option> m_Options;
    protected Action<string> m_NextFrame;
    public OptionState(Button defaultButton, CharacterTextReply replies,  List<Option> options, Action<string>next)
    {
        m_DefaultButton = defaultButton;
       
        m_Replies = replies;
        m_Options = options;
        m_NextFrame = next;
    }

    public override void OnEnter()
    {
        m_DefaultButton.interactable = false;
        m_Replies.Reset();
        m_Replies.gameObject.SetActive(true);
    }

    public override void OnExit()
    {
        m_DefaultButton.interactable = true;
        m_Replies.gameObject.SetActive(false);
    }

    public override void Show()
    {
        // show options 
        foreach (var option in m_Options)
        {
            m_Replies.AddChoice(option.choice,option.targetFrameId, m_NextFrame);
        }
    }
}