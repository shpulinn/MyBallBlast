using UnityEngine;

public interface IDropFactory
{
    IDroppable Create(Vector3 position);
}

public interface IDropFactory<out T> : IDropFactory where T : IDroppable
{
    new T Create(Vector3 position);
}