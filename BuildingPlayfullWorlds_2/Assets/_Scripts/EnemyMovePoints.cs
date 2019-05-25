using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovePoints : MonoBehaviour
{
    public List<Transform> EnemySpots;

    [HideInInspector]
    public EnemyMovePoints Instance;

    private void Awake()
    {
        Instance = this;
    }
}
