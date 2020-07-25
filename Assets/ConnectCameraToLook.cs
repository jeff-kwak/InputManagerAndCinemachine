using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConnectCameraToLook : MonoBehaviour
{
  [Tooltip("A camera following the player's movements")]
  public CinemachineFreeLook FreeLookCamera;

  public void OnLook(InputValue value)
  {
    var inputVector = value.Get<Vector2>();
    FreeLookCamera.m_XAxis.m_InputAxisValue = inputVector.x;
    FreeLookCamera.m_YAxis.m_InputAxisValue = inputVector.y;
  }
}
