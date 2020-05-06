using System;

public class ReactiveValue<T> 
{
    private T currentState;
    public event Action<T> OnChange;
    
    public void Signal()
    {
        OnChange?.Invoke(currentState);
    }

    public T CurrentValue
    {
        get => currentState;
        set
        {
            if (value.Equals(currentState))
                return;
            else
            {
                currentState = value;
                OnChange?.Invoke(currentState);
            }
        }
    }
}