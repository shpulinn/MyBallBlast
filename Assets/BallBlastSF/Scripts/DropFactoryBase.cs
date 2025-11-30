using UnityEngine;

public abstract class DropFactoryBase<T> : IDropFactory<T> where T : IDroppable
{
    protected readonly T prefab;

    protected DropFactoryBase(T prefab)
    {
        this.prefab = prefab;
    }

    public virtual T Create(Vector3 position)
    {
        var instance = Object.Instantiate(prefab as MonoBehaviour, position, Quaternion.identity);
        var droppable = instance.GetComponent<T>();
        ConfigureInstance(droppable);
        return droppable;
    }

    protected abstract void ConfigureInstance(T instance);
    
    IDroppable IDropFactory.Create(Vector3 position)
    {
        return Create(position);
    }
}