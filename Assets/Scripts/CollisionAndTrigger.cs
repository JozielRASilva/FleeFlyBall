using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class CollisionAndTrigger : MonoBehaviour
{
    public List<StringSO> TagsToCheck = new List<StringSO>();
    public Action<GameObject> OnEnter;
    public Action<GameObject> OnStay;
    public Action<GameObject> OnExit;

    public UnityEvent EventOnEnter;
    public UnityEvent EventOnStay;
    public UnityEvent EventOnExit;

    private void OnTriggerEnter(Collider other)
    {
        if (TagsToCheck.Exists(t => t.value.Equals(other.tag)))
        {
            OnEnter?.Invoke(other.gameObject);
            EventOnEnter?.Invoke();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (TagsToCheck.Exists(t => t.value.Equals(other.tag)))
        {
            OnStay?.Invoke(other.gameObject);
            EventOnStay?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (TagsToCheck.Exists(t => t.value.Equals(other.tag)))
        {
            OnExit?.Invoke(other.gameObject);
            EventOnExit?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (TagsToCheck.Exists(t => t.value.Equals(other.gameObject.tag)))
        {
            OnEnter?.Invoke(other.gameObject);
            EventOnEnter?.Invoke();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (TagsToCheck.Exists(t => t.value.Equals(other.gameObject.tag)))
        {
            OnStay?.Invoke(other.gameObject);
            EventOnStay?.Invoke();
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (TagsToCheck.Exists(t => t.value.Equals(other.gameObject.tag)))
        {
            OnExit?.Invoke(other.gameObject);
            EventOnExit?.Invoke();
        }
    }
}
