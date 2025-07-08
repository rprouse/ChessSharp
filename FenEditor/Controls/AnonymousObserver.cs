using System;

namespace FenEditor.Controls;

public class AnonymousObserver<T> : IObserver<T>
{
    private readonly Action<T> _onNext;

    public AnonymousObserver(Action<T> onNext)
    {
        _onNext = onNext;
    }

    public void OnCompleted() { }

    public void OnError(Exception error) { }

    public void OnNext(T value)
    {
        _onNext(value);
    }
}
