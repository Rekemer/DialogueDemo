using System.Collections.Generic;
using System;
using UnityEngine.UI;
public class OptionState : BaseFrameState
{
    IClickable m_DefaultButton;
    IRepliesView m_Replies;
    List<Option> m_Options;
    protected Action<string> m_NextFrame;
    public OptionState(IClickable defaultButton, IRepliesView replies, List<Option> options, Action<string> next)
    {
        m_DefaultButton = defaultButton;
        m_Replies = replies;
        m_Options = options;
        m_NextFrame = next;
    }

    public override void OnEnter()
    {
        m_DefaultButton.SetBlocked(false);
        m_Replies.Reset();
        m_Replies.gameObject.SetActive(true);
    }

    public override void OnExit()
    {
        m_DefaultButton.SetBlocked(true);
        m_Replies.gameObject.SetActive(false);
    }

    public override void Show()
    {
        ShowHeader();

        foreach (var option in m_Options)
        {
            m_Replies.AddChoice(option.choice, option.targetFrameId, m_NextFrame);
        }
    }
}