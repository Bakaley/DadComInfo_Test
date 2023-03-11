using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using StateControllers;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootGenerator : MonoBehaviour
{
    [SerializeField] private BattleCharacterStateController _character;
    [SerializeField] private LootGenerationSO _config;
    
    private void OnEnable()
    {
        _character.OnDie += OnCharacterDieHandler;
    }

    private void OnCharacterDieHandler(BattleCharacterStateController character)
    {
        float r = Random.Range(0f, 1f);
        if (r < _config.HpSphereProbability)
            Instantiate(_config.HpSphereSampler, transform.position, Quaternion.identity);
    }
    
    private void OnDisable()
    {
        _character.OnDie -= OnCharacterDieHandler;
    }
}
