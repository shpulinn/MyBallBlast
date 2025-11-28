using UnityEngine;

public interface IDropFactory<T> where T : IDroppable
{
    T Create(Vector3 position);
}