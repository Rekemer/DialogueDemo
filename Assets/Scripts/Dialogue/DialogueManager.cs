using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;



[RequireComponent(typeof(DialogueBootstrap))]   
public class DialogueManager : MonoBehaviour
{
    
    IClickable DefaultDialogueButton;
    [SerializeField] CharacterText CharacterText;
    [SerializeField] DialogueReplies Replies;
    [SerializeField] Character Character;
    public event Action StoryFinished;
    FrameStateFactory m_StateFactory;
    
    Dictionary<string, BaseFrameState> m_Frames;
    BaseFrameState m_CurrentState;
    string m_StartKey;

    public void Init()
    {
        DefaultDialogueButton = GetComponent<IClickable>();
        m_StateFactory = new FrameStateFactory();
        BaseFrameState Init(BaseFrameState s, Frame f)
        {
            s.Init(f.nextId,
                   f.text,
                   f.characterName,
                   CharacterText);
            return s;
        }
        m_StateFactory.Register(FrameType.Dialogue,
            f => {
                bool left = f.spritePosition?.ToLower() != "right";
                var s = new DialogueState(Character, left);
                Init(s,f);
                return s;
            });

        m_StateFactory.Register(FrameType.Text,
            f => {
                var s = new TextState();
                Init(s,f);
                return s;
            });

        m_StateFactory.Register(FrameType.Final,
            f => {
                var s = new FinalState();
                Init(s,f);
                return s;
            });

        m_StateFactory.Register(FrameType.Option,
            f => {
                var s = new OptionState(DefaultDialogueButton, Replies, f.options, NextTo);
                Init(s,f);               
                return s;
            });

    }
    BaseFrameState CreateState(Frame f)
    {
        return m_StateFactory.Create(f);
    }

    public void BuildStates(StoryData stories)
    {
        m_Frames = new Dictionary<string, BaseFrameState>();
        // populate maps of states
        foreach (var s in stories.frames)
        {
            m_Frames[s.id] = CreateState(s);
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

public class FrameStateFactory
{
    private readonly Dictionary<FrameType, Func<Frame, BaseFrameState>> m_InitMap = new();

    public void Register(FrameType type, Func<Frame, BaseFrameState> ctor) => m_InitMap[type] = ctor;

    public BaseFrameState Create(Frame frame)
    {
        return m_InitMap.TryGetValue(frame.type, out var initFunc)
           ? initFunc(frame)
           : throw new ArgumentOutOfRangeException($"There is no initialization for {frame.type}");
    }
}
