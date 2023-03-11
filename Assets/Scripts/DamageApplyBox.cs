using System.Collections;
using System.Collections.Generic;
using StateControllers;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageApplyBox : MonoBehaviour
{ 
    private BattleCharacterStateController _owner;
    public BattleCharacterStateController Owner => _owner;

    public void Init(BattleCharacterStateController owner)
    {
        _owner = owner;
    }
}
