using System;
using System.Collections;
using System.Collections.Generic;
using StateControllers;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{
    private Coroutine _spawnCoroutine;
    private float _spawnAcrossPlayerRadius = 75;
    [SerializeField] private MonsterStateController _monsterCharacter;
    [SerializeField] private float _spawnInterval = 5;

    [SerializeField] private PlayerStateController _playerPrefabSampler;
    [SerializeField] private Transform _playerSpawnPoint;

    [SerializeField] private Transform[] _startMonsterPositions;

    private List<MonsterStateController> _monsters = new List<MonsterStateController>();

    private PlayerStateController _player;

    private int _currentScore = 0;

    public int CurrentScore
    {
        get => _currentScore;
        set
        {
            _currentScore = value;
            OnScoreChanged?.Invoke(value);
        }
    }

    public event Action<int> OnScoreChanged;

    public PlayerStateController StartGame()
    {
        _player = Instantiate(_playerPrefabSampler, _playerSpawnPoint.transform.position, _playerSpawnPoint.transform.rotation);
        _player.InitOnSpawn();

        int i = 0;
        foreach (var monster in _monsters)
        {
            monster.InitOnSpawn(_player, _startMonsterPositions[i].position);
            i++;
        }
        
        return _player;
    }

    public void RestartGame()
    {
        CurrentScore = 0;
        
        _player.transform.position = _playerSpawnPoint.transform.position;
        _player.transform.rotation = _playerSpawnPoint.transform.rotation;
        _player.InitOnSpawn();

        int i = 0;
        foreach (var monster in _monsters)
        {
            monster.InitOnSpawn(_player, _startMonsterPositions[i].position);
            i++;
        }
    }
    
    private void Awake()
    {
        foreach (var point in _startMonsterPositions)
        {
            MonsterStateController monster = Instantiate(_monsterCharacter, point.transform.position,
                Quaternion.identity);
            _monsters.Add(monster);
            monster.OnDie += OnMonsterDieHandler;
        }
    }

    private void OnMonsterDieHandler(BattleCharacterStateController monster)
    {
        CurrentScore++;
        StartCoroutine(MonsterRespawnCoroutine((MonsterStateController)monster, _spawnInterval));
    }

    private IEnumerator MonsterRespawnCoroutine(MonsterStateController monster, float respawnInterval)
    {
        yield return new WaitForSeconds(respawnInterval);
        monster.InitOnRespawn(CalculateNewMonsterSpawnPosition());
    }

    private Vector3 CalculateNewMonsterSpawnPosition()
    {
        int angle = Random.Range(0, 360);
        float x = _player.transform.position.x + _spawnAcrossPlayerRadius * Mathf.Cos(angle);
        float z = _player.transform.position.z + _spawnAcrossPlayerRadius * Mathf.Sin(angle);
        return new Vector3(x, _player.transform.position.y, z);
    }
}
