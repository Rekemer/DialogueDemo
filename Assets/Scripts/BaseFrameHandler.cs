
public abstract class BaseFrameHandler
{
    public void Show(Frame f)
    {
       // ClearUI();
        SetupData(f);
        RegisterInput(f);
        //Finalise();
    }
    protected abstract void SetupData(Frame f);
    protected abstract void RegisterInput(Frame f);
    // …
}