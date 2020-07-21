using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// VaM Utilities
/// By Acidbubbles
/// Changes the camera clip plane, to see through walls for example
/// Source: https://github.com/acidbubbles/vam-utilities
/// </summary>
public class CameraNearClipPlane : MVRScript
{
    private Camera _mainCamera;
    private JSONStorableFloat _nearClipPlaneJSON;
    private JSONStorableFloat _farClipPlaneJSON;
    private float _originalNearClipDistance;
    private float _originalFarClipDistance;
    private bool _ready = false;

    public override void Init()
    {
        _mainCamera = CameraTarget.centerTarget?.targetCamera;
        _originalNearClipDistance = _mainCamera.nearClipPlane;
        _originalFarClipDistance = _mainCamera.farClipPlane;

        CreateTextField(new JSONStorableString("Warning", "Warning: If you increase the range, you could lose the menu and be forced to restart Virt-A-Mate.")).enabled = false;

        _nearClipPlaneJSON = new JSONStorableFloat("Near clip plane", _originalNearClipDistance, (float val) => SyncCameraClipping(), 0.01f, 1.5f, false);
        RegisterFloat(_nearClipPlaneJSON);
        CreateSlider(_nearClipPlaneJSON);

        _farClipPlaneJSON = new JSONStorableFloat("Far clip plane", _originalFarClipDistance, (float val) => SyncCameraClipping(), 2f, 10000f, false);
        RegisterFloat(_farClipPlaneJSON);
        CreateSlider(_farClipPlaneJSON);

        _ready = true;

        SyncCameraClipping();

        StartCoroutine(Temp());
    }

    private IEnumerator Temp()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            _mainCamera.nearClipPlane = _originalNearClipDistance;
            _mainCamera.farClipPlane = _originalFarClipDistance;
        }
    }

    public void OnEnable()
    {
        SyncCameraClipping();
    }

    public void OnDisable()
    {
        _ready = false;
        _mainCamera.nearClipPlane = _originalNearClipDistance;
        _mainCamera.farClipPlane = _originalFarClipDistance;
    }

    private void SyncCameraClipping()
    {
        if (!_ready) return;

        if (_nearClipPlaneJSON.val <= 0f || _farClipPlaneJSON.val <= 0)
        {
            SuperController.LogError("Cannot set the clip distance to 0 or less");
            return;
        }

        if (_nearClipPlaneJSON.val > _farClipPlaneJSON.val)
        {
            SuperController.LogError("Cannot set the far clip distance to less than the near clip distance");
            return;
        }

        _mainCamera.nearClipPlane = _nearClipPlaneJSON.val;
        _mainCamera.farClipPlane = _farClipPlaneJSON.val;
    }
}