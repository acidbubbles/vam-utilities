using UnityEngine;

/// <summary>
/// VaM Utilities
/// By Acidbubbles
/// Allows triggers that affect field of view
/// Source: https://github.com/acidbubbles/vam-utilities
/// </summary>
public class FieldOfViewController : MVRScript
{
    public override void Init()
    {
        var slider = SuperController.singleton.monitorCameraFOVSlider;

        var monitorFOVJSON = new JSONStorableFloat("Monitor FoV", 40f, slider.minValue, slider.maxValue)
        {
            isStorable = false,
            valNoCallback = slider.value,
            setCallbackFunction = val =>
            {
                slider.value = val;
            }
        };
        RegisterFloat(monitorFOVJSON);
        CreateSlider(monitorFOVJSON).label = "Monitor FoV (Set Only)";
    }
}
