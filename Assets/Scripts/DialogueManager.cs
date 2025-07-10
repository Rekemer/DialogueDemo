using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Globalization;


public class DialogueManager : MonoBehaviour
{
    
    Dictionary<string, BaseFrameState> m_Frames;
    IStoryLoader m_StoryLoader;
    BaseFrameState m_CurrentState;
    string m_StartKey;
    [SerializeField] Button DefaultDialogueButton;
    [SerializeField] CharacterText CharacterText;
    [SerializeField] CharacterTextReply Replies;
    [SerializeField] Character Character;


    public BaseFrameState CreateState(Frame f, Button dialogue)
    {
        BaseFrameState state;

        switch (f.type)
        {
            case FrameType.Dialogue:
                {
                    if (f.spritePosition == null)
                    {
                        Debug.LogWarning("dialogue has unspecified position of character");
                        f.spritePosition = "left";
                    }
                    state = new DialogueState(Character, f.spritePosition.ToLower() == "left");
                    break;
                }

            case FrameType.Text: state = new TextState() ;break;
            case FrameType.Final: state = new FinalState() ;break;
            case FrameType.Option: state = new OptionState(dialogue, Replies,f.options, NextTo); break;
            default:
            {
              Debug.LogError("Cannot create Frametype");
              return null;
            }
        }
        state.Init(f.nextId, f.text, f.characterName, CharacterText);
        return state;
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

        m_StoryLoader = new JsonStoryLoader();
        m_Frames = new Dictionary<string, BaseFrameState>();
        var stories = m_StoryLoader.Load("./Assets/Stories/story.json");

        // populate maps of states
        foreach (var s in stories.frames)
        {
            
            m_Frames[s.id] = CreateState(s, DefaultDialogueButton);
            // initialize start state
            if (m_CurrentState == null && s.type == FrameType.Dialogue)
            {
                m_CurrentState = m_Frames[s.id]; 
            }
        }
    }
    private void Start()
    {
        Next();
    }
    
    public void Next()
    {
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
}
