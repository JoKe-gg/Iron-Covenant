using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BurnEffect : Status
{
    private IDamageable _damageable;
    private NegativeEffectData _effectData;
    private Coroutine _tickCoroutine;
    private float _intervalBetweenTicks;
    private void Awake()
    {
        _damageable = GetComponent<IDamageable>();
    }
    private void TakeBurnDamage()
    {
        if (_damageable != null)
        {
            _damageable.TakeDamage(_effectData.DamageData);
        }
    }
    public override void Initialize(NegativeEffectData negativeEffectBurn)
    {
        _effectData = negativeEffectBurn;
        _intervalBetweenTicks = negativeEffectBurn.IntervalBetweenTicks;
        _tickCoroutine = StartCoroutine(Tick());
        _disableCoroutine = StartCoroutine(DisableAfterTime(_effectData.TimeOfEffect));
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
        while (true)
        {
            TakeBurnDamage();
            yield return new WaitForSeconds(_intervalBetweenTicks);
        }
    }
}
