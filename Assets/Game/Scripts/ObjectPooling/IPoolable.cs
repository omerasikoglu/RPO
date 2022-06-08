using System;
public interface IPoolable<out T> {
    void Initialize(Action<T> returnAction);
    void ReturnToPool();
}