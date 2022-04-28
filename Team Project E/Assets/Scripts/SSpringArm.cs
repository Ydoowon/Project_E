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

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Player.position + new Vector3(0,3.0f,0);
        myCam.position = this.transform.position;
        if(Input.GetMouseButton(1))
        {
            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");
            RotVec += new Vector3(-v * 360.0f * Time.deltaTime, h * 360.0f * Time.deltaTime, 0);
            RotVec.x = Mathf.Clamp(RotVec.x, -20.0f, 50.0f);
            this.transform.eulerAngles = RotVec;
        }

        TargetZoomDist -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        TargetZoomDist = Mathf.Clamp(TargetZoomDist, 2.0f, 5.0f);
        ZoomDist = Mathf.Lerp(ZoomDist, TargetZoomDist, Time.deltaTime* ZoomSpeed);

        Ray ray = new Ray(this.transform.position, -this.transform.forward);
        if(Physics.Raycast(ray, out RaycastHit hit, ZoomDist+CollisionOffSet, CrashMask ))
        {
            ZoomDist = Vector3.Distance(hit.point - ray.direction * CollisionOffSet, this.transform.position);
        }
        myCam.localPosition = new Vector3(0, 0, -ZoomDist);
    }
}
