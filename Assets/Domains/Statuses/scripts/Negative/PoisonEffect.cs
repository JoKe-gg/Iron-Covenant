using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PoisonEffect : Status
{
    private IDamageable _damageable;
    private NegativeEffectData _effectData;
    private float _intervalBetweenTicks = 0;
    private Coroutine _tickCoroutine;
    private void Awake()
    {
        _damageable = GetComponent<IDamageable>();
    }
    private void TakePoisonedDamage()
    {
        if (_damageable != null)
        {
            _damageable.TakeDamage(_effectData.DamageData);
        }
    }
    public override void Initialize(NegativeEffectData negativeEffectPoison)
    {
        if (gameObject.activeSelf)
        {
            _effectData = negativeEffectPoison;
            _intervalBetweenTicks = negativeEffectPoison.IntervalBetweenTicks;
            if (gameObject.activeSelf)
                _tickCoroutine = StartCoroutine(Tick());
            if (gameObject.activeSelf)
                _disableCoroutine = StartCoroutine(DisableAfterTime(_effectData.TimeOfEffect));
        }
    }
    public override void UpdateData(NegativeEffectData negativeEffectData)
    {
        if (gameObject.activeSelf)
            StopCoroutine(_disableCoroutine); 
        if (gameObject.activeSelf)
            StopCoroutine(_tickCoroutine);
        Initialize(negativeEffectData);
    }
    private void OnDisable()
    {
        StopCoroutine(_tickCoroutine);
    }
    protected override IEnumerator Tick()
    {
        while(true)
        {
            TakePoisonedDamage();
            yield return new WaitForSeconds(_intervalBetweenTicks);
        }
    }
}
