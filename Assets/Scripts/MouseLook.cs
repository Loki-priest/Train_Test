using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MouseLook : MonoBehaviour {

    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;

    private Camera m_Camera;

    public float XSensitivity = 2f;
    public float YSensitivity = 2f;

    public float smoothTime = 5f;
    public bool smooth;
    public bool clampVerticalRotation = true;

    public float MinimumX = -90F;
    public float MaximumX = 90F;

    public GameObject charact;

    void Start()
    {
        m_Camera = Camera.main;
        Init(charact.transform, m_Camera.transform);
    }

    public void Init(Transform character, Transform camera)
    {
        m_CharacterTargetRot = character.localRotation;
        m_CameraTargetRot = camera.localRotation;
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

    public void LookRotation(Transform character, Transform camera)
    {
        float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
        float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;

        m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
        m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

        if (clampVerticalRotation)
            m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

        if (smooth)
        {
            character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot,
                smoothTime * Time.deltaTime);
            camera.localRotation = Quaternion.Slerp(camera.localRotation, m_CameraTargetRot,
                smoothTime * Time.deltaTime);
        }
        else
        {
            character.localRotation = m_CharacterTargetRot;
            camera.localRotation = m_CameraTargetRot;
        }

        
    }

    void Renew(Transform character, Transform camera)
    {
        character.localRotation = Quaternion.Slerp(character.localRotation, Quaternion.Euler(0,0,0),
            smoothTime * Time.deltaTime);
        camera.localRotation = Quaternion.Slerp(camera.localRotation, GameMgr.Instance.mainCam.transform.localRotation,
            smoothTime * Time.deltaTime);
    }
    

    void Update()
    {
        if (GameMgr.Instance.freeCam)
        {
            LookRotation(charact.transform, m_Camera.transform);
        }
        else
        {
            Renew(charact.transform, m_Camera.transform);
        }

    }

}
