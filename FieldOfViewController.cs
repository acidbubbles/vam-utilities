using UnityEngine;

/// <summary>
/// VaM Utilities
/// By Acidbubbles
/// Allows triggers that affect field of view
/// Source: https://github.com/acidbubbles/vam-utilities
/// </summary>
public class FieldOfViewController : MVRScript
{
    bool _isSessionPlugin;
    JSONStorableFloat _monitorFOVJSON;

    public override void Init()
    {
        _isSessionPlugin = containingAtom.type == "SessionPluginManager";
        var slider = SuperController.singleton.monitorCameraFOVSlider;

        _monitorFOVJSON = new JSONStorableFloat("Monitor FoV", 40f, slider.minValue, slider.maxValue)
        {
            isStorable = _isSessionPlugin,
            valNoCallback = slider.value,
            setCallbackFunction = val => slider.value = val
        };
        RegisterFloat(_monitorFOVJSON);
        CreateSlider(_monitorFOVJSON).label = "Monitor FoV (Set Only)";

        if(_isSessionPlugin)
        {
            slider.onValueChanged.AddListener(OnMonitorCameraFovChanged);
        }
    }

    void OnMonitorCameraFovChanged(float value) => _monitorFOVJSON.valNoCallback = value;

    public void OnDestroy()
    {
        if(_isSessionPlugin)
        {
            SuperController.singleton.monitorCameraFOVSlider.onValueChanged.RemoveListener(OnMonitorCameraFovChanged);
        }
    }
}
