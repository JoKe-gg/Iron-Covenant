using System;
using UnityEngine;

public class EnemyVisionSensor : MonoBehaviour
{
    [Header("Rays settings")]
    [SerializeField] private float _length;
    [SerializeField, Min(3)] private int _amountOfRays;
    [SerializeField] private bool _drawGizmos;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _scanInterval = 0.1f;
    [SerializeField] int maxRays = 20;
    //rays data
    private Vector2[] _directions;
    private float[] _rayLengths;
    //Repeating data
    private float nextScanTime;

    public event Action<Vector2> OnCaughtPlayer;

    void OnValidate()
    {
        _amountOfRays = Mathf.Clamp(_amountOfRays, 3, maxRays);
    }
    private void OnEnable()
    {
        nextScanTime = Time.time;
    }
    private void Awake()
    {
        InitRays();
    }
    public void Scan()
    {
        bool caughtPlayer = false;
        Vector2 caughtPoint = default;
        if (_directions == null || _rayLengths == null || _directions.Length != _amountOfRays || _rayLengths.Length != _amountOfRays)
        {
            InitRays();
        }
        for (int i = 0; i < _amountOfRays; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _directions[i], _length, _targetLayer);
            if (hit.collider != null)
            {
                _rayLengths[i] = hit.distance;
                caughtPlayer = true;
                caughtPoint = hit.point;
            }
            else
            {
                _rayLengths[i] = _length;
            }
        }
        if (caughtPlayer)
        {
            OnCaughtPlayer?.Invoke(caughtPoint);
        }
    }
    private void FixedUpdate()
    {
        if (Time.time >= nextScanTime)
        {
            nextScanTime = Time.time + _scanInterval;
            Scan();
        }
    }
    private void InitRays()
    {
        float step = 360 / (float)_amountOfRays;
        _directions = new Vector2[_amountOfRays];
        _rayLengths = new float[_amountOfRays];
        for (int i = 0; i < _amountOfRays; i++)
        {
            float angleRad = i * step * Mathf.Deg2Rad;
            _directions[i] = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _directions[i], _length, _targetLayer);
            if (hit.collider != null)
            {
                _rayLengths[i] = hit.distance;
            }
            else
            {
                _rayLengths[i] = _length;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (!_drawGizmos)
        {
            return; 
        }

        if (_directions == null ||
        _rayLengths == null ||
        _directions.Length != _amountOfRays ||
        _rayLengths.Length != _amountOfRays)
        {
            InitRays();
        }

        Gizmos.color = Color.red;

        for (int i = 0; i < _amountOfRays; i++)
        {
            Gizmos.DrawRay(transform.position, _directions[i] * _rayLengths[i]);
        }
    }
}
