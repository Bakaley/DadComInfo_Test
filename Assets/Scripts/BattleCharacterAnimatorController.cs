using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BattleCharacterAnimatorController : MonoBehaviour
{
    private Animator _animator;
    
    private readonly string _movementString = "Movement";
    private readonly string _attackTriggerString = "Attack";
    private readonly string _hitTriggerString = "Hit";
    private readonly string _deadTriggerString = "Dead";

    private bool _running = false;
    private float _smoothTimeBattleLayerActivation = .25f;
    private Coroutine _smoothRunCoroutine;
    private Coroutine _disablingBattleLayerCoroutine;

    public event Action OnAttackApplyEffect;

    public void SetMovementValue(float newValue)
    {
        _animator.SetFloat(_movementString, newValue);
    }

    public void Attack(float duration)
    {
        SmoothlySetBattleLayer(true);
        _animator.SetTrigger(_attackTriggerString);
        if(_disablingBattleLayerCoroutine != null) StopCoroutine(_disablingBattleLayerCoroutine);
        _disablingBattleLayerCoroutine = StartCoroutine(DisableBattleLayerIn(duration));
    }

    public void GetHitted(float duration)
    {
        SmoothlySetBattleLayer(true);
        _animator.SetTrigger(_hitTriggerString);
        if(_disablingBattleLayerCoroutine != null) StopCoroutine(_disablingBattleLayerCoroutine);
        _disablingBattleLayerCoroutine = StartCoroutine(DisableBattleLayerIn(duration));
    }

    public void SetDead(bool newValue)
    {
        SmoothlySetBattleLayer(false);
        _animator.SetBool(_deadTriggerString, newValue);
    }
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private IEnumerator SmoothlySetBattleLayerWeight(float time, float newValue)
    {
        float currentTime = 0;
        float currentValue = _animator.GetLayerWeight(1);
        while (currentTime < time)
        {
            _animator.SetLayerWeight(1, Mathf.Lerp(currentValue, newValue, currentTime/time));
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _animator.SetLayerWeight(1, newValue);
        _smoothRunCoroutine = null;
    }
    
    private void SmoothlySetBattleLayer(bool newValue)
    {
        if (newValue != _running)
        {
            _running = newValue;
            if(_smoothRunCoroutine != null) StopCoroutine(_smoothRunCoroutine);
        
            if (newValue) _smoothRunCoroutine =
                StartCoroutine(SmoothlySetBattleLayerWeight(_smoothTimeBattleLayerActivation, 1));
            else _smoothRunCoroutine =
                StartCoroutine(SmoothlySetBattleLayerWeight(_smoothTimeBattleLayerActivation, 0));
        }
    }

    private IEnumerator DisableBattleLayerIn(float time)
    {
        yield return new WaitForSeconds(time);
        SmoothlySetBattleLayer(false);
        _disablingBattleLayerCoroutine = null;
    }
    
    //Animator event
    private void AttackEffectApply()
    {
        OnAttackApplyEffect?.Invoke();
    }
}
