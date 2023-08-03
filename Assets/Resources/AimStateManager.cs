using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimStateManager : MonoBehaviour
{
    float xAxis, yAxis;
    [SerializeField] Transform camFollowPos;
    [SerializeField] float mouseSense = 1;

    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -80, 80);
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }
}
