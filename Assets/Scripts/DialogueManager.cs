using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Globalization;
using System.IO;
using UnityEngine.Analytics;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(DialogueBootstrap))]   
public class DialogueManager : MonoBehaviour
{
    
    [SerializeField] Button DefaultDialogueButton;
    [SerializeField] CharacterText CharacterText;
    [SerializeField] DialogueReplies Replies;
    [SerializeField] Character Character;

    Dictionary<string, BaseFrameState> m_Frames;
    BaseFrameState m_CurrentState;
    string m_StartKey;


    BaseFrameState CreateState(Frame f, Button defaultDialogueButton)
    {
        // helper that runs Init once and returns the fully-prepared state
        BaseFrameState Init(BaseFrameState s)
        {
            s.Init(f.nextId,            // shared data
                   f.text,
                   f.characterName,
                   CharacterText);  // the single header view
            return s;
        }

        bool left = f.spritePosition?.ToLower() != "right";

        switch (f.type)
        {
            case FrameType.Dialogue: return Init(new DialogueState(Character, left));

            case FrameType.Text: return Init(new TextState());

            case FrameType.Final: return Init(new FinalState());

            case FrameType.Option:
                return Init(
                                          new OptionState(
                                             defaultDialogueButton, Replies, f.options, NextTo)                   // callback
                                       );

            default:
                throw new ArgumentOutOfRangeException(
                    $"Unknown frame type {f.type}");
        }
    }

    //public BaseFrameState CreateState(Frame f, Button dialogue)
    //{
    //    BaseFrameState state;

    //    switch (f.type)
    //    {
    //        case FrameType.Dialogue:
    //            {
    //                if (f.spritePosition == null)
    //                {
    //                    Debug.LogWarning("dialogue has unspecified position of character");
    //                }
    //                state = new DialogueState(Character, f.spritePosition?.ToLower() != "right");
    //                break;
    //            }

    //        case FrameType.Text: state = new TextState() ;break;
    //        case FrameType.Final: state = new FinalState() ;break;
    //        case FrameType.Option: state = new OptionState(dialogue, Replies,f.options, NextTo); break;
    //        default:
    //        {
    //          Debug.LogError("Cannot create Frametype");
    //          return null;
    //        }
    //    }
    //    state.Init(f.nextId, f.text, f.characterName, CharacterText);
    //    return state;
    //}

    public void BuildStates(StoryData stories)
    {
        m_Frames = new Dictionary<string, BaseFrameState>();
        // populate maps of states
        foreach (var s in stories.frames)
        {

            m_Frames[s.id] = CreateState(s, DefaultDialogueButton);
            // initialize start state
            if (m_CurrentState == null && s.type == FrameType.Dialogue)
            {
                m_StartKey = s.id;
                m_CurrentState = m_Frames[s.id];
            }
        }
    }
    public void Next()
    {
        if (m_CurrentState != null)
            NextTo(m_CurrentState.NextId);
    }
    public void NextTo(string key)
    {
        m_CurrentState.OnExit();

        if (key != null && m_Frames.ContainsKey(key))
        {

            m_CurrentState = m_Frames[key];

            m_CurrentState.OnEnter();

            m_CurrentState.Show();

        }
        else
        {
            Debug.Log("Story has ended");
        }

    }
    private void Awake()
    {
        if (DefaultDialogueButton == null)
        {
            Debug.LogError("Default Dialogue Button field is not initialized");
            return;
        }
        if (Character == null || Replies == null)
        {
            Debug.LogError("Character fields are not initialized");
            return;
        }
        ;    
    }
    private void Start()
    {
        NextTo(m_StartKey);
    }
    
    
}
