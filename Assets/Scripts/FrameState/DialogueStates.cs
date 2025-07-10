using UnityEngine;
using UnityEngine.UI;

public class DialogueState : BaseFrameState
{
    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void Show()
    {
        throw new System.NotImplementedException();
    }
}
public class TextState : BaseFrameState
{
    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void Show()
    {
        throw new System.NotImplementedException();
    }
}

public class FinalState : BaseFrameState
{
    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void Show()
    {
        throw new System.NotImplementedException();
    }
}

public class OptionState : BaseFrameState
{
    Button m_DefaultButton;
    public OptionState(Button defaultButton)
    {
        m_DefaultButton = defaultButton;
    }

    public override void OnEnter()
    {
        m_DefaultButton.interactable = false;
    }

    public override void OnExit()
    {
        m_DefaultButton.interactable = true;
    }

    public override void Show()
    {
        // show options 
        throw new System.NotImplementedException();
    }
}