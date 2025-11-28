using UnityEngine;

public enum EdgeType { Left, Right, Bottom }

public class LevelEdge : MonoBehaviour
{
    [SerializeField] EdgeType edgeType;
    
    public EdgeType EdgeType => edgeType;
}
