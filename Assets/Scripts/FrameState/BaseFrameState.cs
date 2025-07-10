using System;

public abstract class BaseFrameState
{
    protected string m_Text;
    protected string m_CharacterName;
    protected CharacterText m_CharacterText;
   

    public string NextId => m_NextId;
    string m_NextId;
    public void Init(string nextId, string text, string characterName, CharacterText characterText)
    {
        m_NextId = nextId;
        m_Text = text;
        m_CharacterName= characterName;
        m_CharacterText = characterText;
    }

    public virtual  void OnEnter() { }
    public abstract void Show();
    public virtual void OnExit() { }
}