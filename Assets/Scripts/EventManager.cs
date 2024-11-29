using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent OnTurnPassed = new UnityEvent();
    public static UnityEvent OnTilesUpdated = new UnityEvent();
    public static UnityEvent <int> OnAgressionChange = new UnityEvent <int>();
    public static UnityEvent <AudioClip> OnDeath = new UnityEvent <AudioClip>();
    public static UnityEvent <bool> OnMagnetState = new UnityEvent <bool>();
    public static UnityEvent <bool> OnInvisabilityChange = new UnityEvent <bool>();
    public static UnityEvent <Transform> OnLightAdded = new UnityEvent <Transform>();
    public static UnityEvent <string> OnMessageSend = new UnityEvent <string>();
    public static UnityEvent <Transform> OnLightRemoved = new UnityEvent <Transform>();
    public static UnityEvent <Transform> OnMagnetTargetSet = new UnityEvent <Transform>();

    public static void PassTheTurn()
    {
        OnTurnPassed.Invoke();
    }
    public static void ChangeInvisability(bool flag)
    {
        OnInvisabilityChange.Invoke(flag);
    }
    public static void DieSound(AudioClip clip)
    {
        OnDeath.Invoke(clip);
    }
    public static void ChangeMagnetState(bool flag)
    {
        OnMagnetState.Invoke(flag);
    }
    public static void SetMagnetTarget(Transform target)
    {
        OnMagnetTargetSet.Invoke(target);
    }
    public static void AgressionUpdate(int amount)
    {
        OnAgressionChange.Invoke(amount);
    }
    public static void UpdateTiless()
    {
        OnTilesUpdated.Invoke();
    }
    public static void SendGameMessage(string text)
    {
        OnMessageSend.Invoke(text);
    }
    public static void AddLightingToList(Transform light)
    {
        OnLightAdded.Invoke(light);
    }
    public static void RemoveLightingToList(Transform light)
    {
        OnLightRemoved.Invoke(light);
    }
}
