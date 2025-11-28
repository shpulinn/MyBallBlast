using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner Instance;
    
    private void Awake() => Instance = this;
    
    public static Coroutine Start(IEnumerator c) => Instance.StartCoroutine(c);
}