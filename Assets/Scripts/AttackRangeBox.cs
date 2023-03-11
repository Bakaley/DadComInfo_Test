using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AttackRangeBox : MonoBehaviour
{
    public event Action<Collider> OnTriggerStayEvent;
    
    private void OnTriggerStay(Collider other)
    {
        OnTriggerStayEvent?.Invoke(other);
    }
}
