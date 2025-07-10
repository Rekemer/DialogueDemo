using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


[RequireComponent(typeof(DialogueBootstrap))]   
public class DialogueManager : MonoBehaviour
{
    
    [SerializeField] Button DefaultDialogueButton;
    [SerializeField] CharacterText CharacterText;
    [SerializeField] DialogueReplies Replies;
    [SerializeField] Character Character;
    public event Action StoryFinished;

    Dictionary<string, BaseFrameState> m_Frames;
    BaseFrameState m_CurrentState;
    string m_StartKey;


    BaseFrameState CreateState(Frame f, Button defaultDialogueButton)
    {
       
        BaseFrameState Init(BaseFrameState s)
        {
            s.Init(f.nextId,            
                   f.text,
                   f.characterName,
                   CharacterText);  
            return s;
        }

        bool left = f.spritePosition?.ToLower() != "right";

        switch (f.type)
        {
            case FrameType.Dialogue: return Init(new DialogueState(Character, left));

            case FrameType.Text: return Init(new TextState());

            case FrameType.Final: return Init(new FinalState());

            case FrameType.Option: return Init( new OptionState( defaultDialogueButton, Replies, f.options, NextTo));

            default:
                throw new ArgumentOutOfRangeException(
                    $"Unknown frame type {f.type}");
        }
    }

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

        if (key != null && m_Frames.ContainsKey(key))
        {

            m_CurrentState.OnExit();
            
            m_CurrentState = m_Frames[key];

            m_CurrentState.OnEnter();

            m_CurrentState.Show();

        }
        else
        {
            Debug.Log("Story has ended");
            StoryFinished?.Invoke();
        }

    }
    private void Awake()
    {
        if (DefaultDialogueButton == null)
        {
            Debug.LogError("Default Dialogue Button field is not initialized");
            enabled = false;
            return;
        }
        if (Character == null || Replies == null)
        {
            Debug.LogError("Character fields are not initialized");
            enabled = false;
            return;
        }
        ;    
    }
    private void Start()
    {
        NextTo(m_StartKey);
    }
    
    
}
