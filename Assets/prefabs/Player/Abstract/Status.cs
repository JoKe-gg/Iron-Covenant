using System;
using UnityEngine;
using System.Collections;
public class StatusBasicData
{
    public string StatusName { get; private set; }

    public StatusBasicData (string statusName)
    {
        StatusName = statusName;
    }
} 

public abstract class Status : MonoBehaviour
{
    [SerializeField] private StatusBasicData _negativeStatusBasicData;
    protected Coroutine _disableCoroutine;

    protected IEnumerator DisableAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        enabled = false;
    }
    public abstract void UpdateData(NegativeEffectData negativeEffectData);
    public abstract void Initialize(NegativeEffectData negativeEffectData);
    protected abstract IEnumerator Tick();
}
