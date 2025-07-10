public abstract class BaseFrameState
{
    string m_NextId;
    string m_Text;
    public string NextId => m_NextId;
    public void Init(string nextId, string text)
    {
        m_NextId = nextId;
        m_Text = text;
    }

    public abstract void OnEnter();
    public abstract void Show();
    public abstract void OnExit();
}