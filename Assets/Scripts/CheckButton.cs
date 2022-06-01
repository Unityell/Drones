
public class CheckButton : Signal
{
    public event EmitSignal MySignal;

    public void OnPress()
    {
        MySignal?.Invoke("Press");
    }
}
