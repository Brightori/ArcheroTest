using System;

public class ReactivePressButton
{
    public event Action Pressed;
    public event Action Released;

    public void SetPressed(bool press)
    {
        if (press)
            Pressed?.Invoke();
    }

    public void SetReleased(bool release)
    {
        if (release)
            Released?.Invoke();
    }
}