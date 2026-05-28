using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatusExemplaire
{
    public Status Status { get; private set; }
    public NegativeEffectData EffectData { get; private set; }
    public StatusExemplaire(NegativeEffectData effectData, Status status)
    {
        Status = status;
        EffectData = effectData;
    }
    public void UpdateData(NegativeEffectData effectData, bool isActive)
    {
        if (effectData != null)
        {
            EffectData = effectData;
            {
                if (!isActive)
                {
                    Status.enabled = true;
                }
                Status.UpdateData(effectData);
            }
        }
    }
}
public class EffectController : MonoBehaviour
{
    Dictionary<StatusEffectType, StatusExemplaire> _activeEffects = new();

    Dictionary<StatusEffectType, Func<GameObject, Status>> _factory = new()
    {
        {StatusEffectType.Poison, go => go.AddComponent<PoisonEffect>() },
        {StatusEffectType.Burn, go => go.AddComponent<BurnEffect>() },
        {StatusEffectType.Stun, go => throw new NotSupportedException("Stun effect is not realized") },
        {StatusEffectType.Slow, go => throw new NotSupportedException("Slow effect is not realized") },
    };
    public void AddStatus(NegativeEffectData effectData)
    {
        if (!gameObject.activeInHierarchy)
            return;

        if (_activeEffects.TryGetValue(effectData.EffectType, out StatusExemplaire existingEffect))
        {
            UpdateExistedEffect(existingEffect, effectData);
        }
        else
        {
            AddNewEffect(effectData);
        }
    }
    public Status CreateEffect(StatusEffectType statusEffectType)
    {
        return statusEffectType switch
        {
            StatusEffectType.Poison => gameObject.AddComponent<PoisonEffect>(),
            StatusEffectType.Burn => gameObject.AddComponent<BurnEffect>(),
            _ => null,
        };
    }
    public void AddNewEffect(NegativeEffectData effectData)
    {
        Status newEffect = _factory[effectData.EffectType](gameObject);
        if (newEffect != null)
        {
            newEffect.Initialize(effectData);
            StatusExemplaire statusExemplaire = new(effectData, newEffect);
            _activeEffects.Add(effectData.EffectType, statusExemplaire);
        }
    }
    public void UpdateExistedEffect(StatusExemplaire ExistedEffectExemplaire, NegativeEffectData newEffectData)
    {
        if (ExistedEffectExemplaire != null && newEffectData != null)
        {
            if (ExistedEffectExemplaire.Status.enabled)
            {
                if (ExistedEffectExemplaire.EffectData.Level < newEffectData.Level)
                {
                    ExistedEffectExemplaire.UpdateData(newEffectData, true);
                }
            }
            else
            {
                ExistedEffectExemplaire.UpdateData(newEffectData, false);
            }
        }
    }
    public void ResetEffects()
    {
        foreach (var effect in _activeEffects.Values)
        {
            Destroy(effect.Status);
        }
        _activeEffects.Clear();
    }
}
