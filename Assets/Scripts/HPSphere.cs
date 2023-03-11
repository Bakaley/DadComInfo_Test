using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class HPSphere : MonoBehaviour
{
    [SerializeField] private HPSphereSO _config;

    public int HPGain => _config.HpGain;
}
