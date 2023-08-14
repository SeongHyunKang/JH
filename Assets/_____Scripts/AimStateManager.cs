using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimStateManager : MonoBehaviour
{
    float xAxis, yAxis;
    [SerializeField] Transform modelTransform;
    [SerializeField] Transform camFollowPos;
    [SerializeField] float mouseSense = 1;

    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -80, 80);
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis,camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        modelTransform.eulerAngles = new Vector3(modelTransform.eulerAngles.x, xAxis, modelTransform.eulerAngles.z);
        //-> Changed from this.transform to modelTransform because the rotation should be affecting the model, not the playerCharacter.
        // This is because I've enabled root motion on animation, which makes the model's xyz transform change, and not the playerCharacter.
    }
}
