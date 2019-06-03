using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AttackStats : MonoBehaviour
{
    [SerializeField]
    public float Damage;
    [SerializeField]
    public float AttackSpeed;
    [SerializeField]
    public float AttackRange;
    [SerializeField]
    public KeyCode KeyButton;
}
