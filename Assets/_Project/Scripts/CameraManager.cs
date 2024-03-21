using System;
using System.Collections;
using Cinemachine;
using KBCore.Refs;
using UnityEngine;

namespace Platformer {

    public class CameraManager : ValidatedMonoBehaviour {

    [Header("Refernces")]
    [SerializeField, Anywhere] InputReader input;
    [SerializeField, Anywhere] CinemachineFreeLook freeLookVCamera;

    [Header("Settings")]
    [SerializeField, Range(10f, 100f)] float speedMultiplayer = 50f;

    bool isRBMPressed;
    bool isDeviceMouse;
    bool cameraMovementLook;

    void OnEnable() {
      input.Look += OnLook;
      input.EnableMouseControlCamera += OnEnableMouseControlCamera;
      input.DisableMouseControlCamera += OnDisableMouseControlCamera;
    }

    void OnDisable() {
      input.Look -= OnLook;
      input.EnableMouseControlCamera -= OnEnableMouseControlCamera;
      input.DisableMouseControlCamera -= OnDisableMouseControlCamera;
    }

    private void OnLook(Vector2 cameraMovement, bool isDeviceMouse) {
      if (cameraMovementLook)
        return;

      if (isDeviceMouse && !isRBMPressed)
        return;

      float deviceMultiplayer = isDeviceMouse ? Time.fixedDeltaTime : Time.deltaTime;

      freeLookVCamera.m_XAxis.m_InputAxisValue = cameraMovement.x * speedMultiplayer * deviceMultiplayer;
      freeLookVCamera.m_YAxis.m_InputAxisValue = cameraMovement.y * speedMultiplayer * deviceMultiplayer;
    }

    void OnEnableMouseControlCamera() {
      isRBMPressed = true;

      // Lock mouse cursor
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;

      StartCoroutine(DisableMouseForFrame());
    }

    void OnDisableMouseControlCamera() {
      isRBMPressed = false;

      // Unlock mouse cursor
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;

      freeLookVCamera.m_XAxis.m_InputAxisValue = 0f;
      freeLookVCamera.m_YAxis.m_InputAxisValue = 0f;
    }

    IEnumerator DisableMouseForFrame() {
      cameraMovementLook = true;
      yield return new WaitForEndOfFrame();
      cameraMovementLook = false;
    }

  }

}
