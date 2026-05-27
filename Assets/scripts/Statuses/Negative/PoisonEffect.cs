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
        if (gameObject.activeInHierarchy)
        {
            _effectData = negativeEffectPoison;
            _intervalBetweenTicks = negativeEffectPoison.IntervalBetweenTicks;
            _tickCoroutine = StartCoroutine(Tick());
            _disableCoroutine = StartCoroutine(DisableAfterTime(_effectData.TimeOfEffect));
        }
    }
    public override void UpdateData(NegativeEffectData negativeEffectData)
    {
        StopCoroutine(_disableCoroutine); 
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
