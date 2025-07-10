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
        ShowHeader();
    }
}



