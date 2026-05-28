using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PlayerCombat : Attackable
{
    [SerializeField] Weapon Weapon;
    private PlayerActionMap input;
    private bool _isPaused = false;
    private void Awake()
    {
        bool error = false;

        if (Weapon == null) 
        {
            Debug.Log($"Miised \"{nameof(Weapon)}\" in the script \"{nameof(PlayerCombat)}\".");
            error = true;
        }
        if (error) 
        {
            enabled = false;
        }
        input = new PlayerActionMap();
    }
    private void OnEnable() { 
        input.Enable(); 
        PauseManager.instance.OnPauseStatusChanged += OnPaused;
    }
    private void OnDisable() {
        input.Disable();
        PauseManager.instance.OnPauseStatusChanged -= OnPaused;
    }
    private void OnPaused(bool value) => _isPaused = value;
    private void Update()
    {
        if (_isPaused) return;
        if (input.Player.attack.IsPressed())
        {
            Weapon.TryAttack();
        }
        if (input.Player.ability.IsPressed())
        {
            Weapon.TryAbilityAttack();
        }
    }
}
