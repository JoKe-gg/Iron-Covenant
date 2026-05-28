using UnityEngine;

public class SoundSettingsTab : Tab
{
    [SerializeField] private SoundSettingsActions SoundSettingsActions;
    public override void Initialize()
    {
        if (SoundSettingsActions != null)
        {
            SoundSettingsActions.SetAudionSettings();
        }
    }
}
