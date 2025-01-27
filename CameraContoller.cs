using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    public Transform player; 
    public float defaultFOV = 60f; 
    public float maxFOV = 90f;
    public float fovChangeSpeed = 5f; 

    private Camera mainCamera;
    private CharacterController characterController; 

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        if (player != null)
        {
            characterController = player.GetComponent<CharacterController>();
        }
        
        mainCamera.fieldOfView = defaultFOV;
    }

    private void Update()
    {
        if (characterController != null)
        {
            Vector3 horizontalVelocity = new Vector3(characterController.velocity.x, 0, characterController.velocity.z);
            float playerSpeed = horizontalVelocity.magnitude;
            
            float targetFOV = Mathf.Lerp(defaultFOV, maxFOV, playerSpeed / 10f); 
            
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * fovChangeSpeed);
        }
    }
}
