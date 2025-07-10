using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DialogueManager : MonoBehaviour
{
    
    Dictionary<string, BaseFrameState> m_Frames;
    IStoryLoader m_StoryLoader;
    BaseFrameState m_CurrentState;
    [SerializeField] Button DefaultDialogueButton;

    public BaseFrameState CreateState(Frame f, Button dialogue)
    {
        BaseFrameState state;
        switch (f.type)
        {
            case FrameType.Dialogue: state = new DialogueState() ;break;
            case FrameType.Text: state = new TextState() ;break;
            case FrameType.Final: state = new FinalState() ;break;
            case FrameType.Option: state = new OptionState(dialogue); break;
            default:
            {
              Debug.LogError("Cannot create Frametype");
              return null;
            }
        }
        state.Init(f.nextId, f.text);
        return state;
    }


    private void Awake()
    {
        if (DefaultDialogueButton == null)
        {
            Debug.LogError("Default Dialogue Button field is not initialized");
            return;
        }
        m_StoryLoader = new JsonStoryLoader();

        var stories = m_StoryLoader.Load("./Assets/Stories/story.json");

        foreach (var s in stories.frames)
        {
            m_Frames[s.id] = CreateState(s, DefaultDialogueButton);
        }
    }
   
    //invokes on the click to advance dialogue 
    // callback is binded via inspector
    void Next()
    {
        m_CurrentState.OnExit();

        m_CurrentState = m_Frames[m_CurrentState.NextId];

        m_CurrentState.OnEnter();

        m_CurrentState.Show();

    }
   
}
