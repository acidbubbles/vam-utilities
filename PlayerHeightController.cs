using UnityEngine;

/// <summary>
/// VaM Utilities
/// By Acidbubbles
/// Allows triggers that affect player height
/// Source: https://github.com/acidbubbles/vam-utilities
/// </summary>
public class PlayerHeightController : MVRScript
{
    private JSONStorableFloat _playerHeightJSON;

    public override void Init()
    {
        SuperController.singleton.playerHeightAdjustSlider.onValueChanged.AddListener(OnPlayerHeightChanged);
        _playerHeightJSON = new JSONStorableFloat("Player Height", 0f, -1f, 5f, false)
        {
            setCallbackFunction = val =>
            {
                SuperController.singleton.playerHeightAdjust = val;
            },
            valNoCallback = SuperController.singleton.playerHeightAdjust,
            defaultVal = SuperController.singleton.playerHeightAdjust,
        };
        RegisterFloat(_playerHeightJSON);
        CreateSlider(_playerHeightJSON);
    }

    private void OnPlayerHeightChanged(float val)
    {
        _playerHeightJSON.valNoCallback = val;
    }

    private void OnDisable()
    {
        SuperController.singleton.playerHeightAdjustSlider.onValueChanged.RemoveListener(OnPlayerHeightChanged);
    }
}