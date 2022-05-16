using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSpringArm : MonoBehaviour
{

    Vector3 RotVec = Vector3.zero;
    public Transform myCam;

    float TargetZoomDist = 0.0f;
    float ZoomDist = 0.0f;
    float ZoomSpeed = 5.0f;
    public Transform Player;
    float CollisionOffSet = 0.5f;
    public LayerMask CrashMask;
    // Start is called before the first frame update
    void Start()
    {
        TargetZoomDist = ZoomDist = -myCam.localPosition.z;
    }



    private void Update()
    {
        /*
        this.transform.position = Player.position + new Vector3(0, 3.0f, 0);
        myCam.position = this.transform.position;
        */
        if (Input.GetMouseButton(1))
        {
            Vector3 Rot = transform.localRotation.eulerAngles;
            Rot.y += Input.GetAxis("Mouse X") * 5.0f;
            Rot.x -= Input.GetAxis("Mouse Y") * 5.0f;

            if (Rot.x > 180.0f)
            {
                Rot.x -= 360.0f;
            }
            Rot.x = Mathf.Clamp(Rot.x, -20.0f, 50.0f);
            Rot.z = 0;
            Quaternion q = Quaternion.Euler(Rot);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, 1f);

        }

        TargetZoomDist -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        TargetZoomDist = Mathf.Clamp(TargetZoomDist, 2.0f, 5.0f);
        ZoomDist = Mathf.Lerp(ZoomDist, TargetZoomDist, Time.deltaTime * ZoomSpeed);

        Ray ray = new Ray(this.transform.position, -this.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, ZoomDist + CollisionOffSet, CrashMask))
        {
            ZoomDist = Vector3.Distance(hit.point - ray.direction * CollisionOffSet, this.transform.position);
        }
        myCam.localPosition = new Vector3(0, 0, -ZoomDist);
    }
}
