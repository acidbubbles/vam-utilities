using System;
using UnityEngine;

/// <summary>
/// VaM Utilities
/// By Acidbubbles
/// Will teleport the user when the plugin is loaded
/// Source: https://github.com/acidbubbles/vam-utilities
/// </summary>
public class TeleportOnLoad : MVRScript
{
    public override void Init()
    {
        CreateButton("Teleport now").button.onClick.AddListener(TeleportNow);
        TeleportNow();
    }

    private void TeleportNow()
    {
        var sc = SuperController.singleton;
        var offset = new Vector3(sc.centerCameraTarget.transform.position.x - sc.navigationPlayArea.position.x, 0f, sc.centerCameraTarget.transform.position.z - sc.navigationPlayArea.position.z);
        sc.navigationPlayArea.position = containingAtom.transform.position - offset;
        sc.navigationRig.position = sc.navigationPlayArea.position;
        sc.navigationRig.rotation = sc.navigationPlayArea.rotation;
        if (sc.navigationRigParent != null)
        {
            sc.navigationRigParent.position = sc.navigationRig.position;
            sc.navigationRigParent.rotation = sc.navigationRig.rotation;
            sc.navigationRig.position = sc.navigationRigParent.position;
            sc.navigationRig.rotation = sc.navigationRigParent.rotation;
        }
    }
}