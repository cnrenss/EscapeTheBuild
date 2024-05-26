using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAmeraController : MonoBehaviour
{
    public Transform target;
    public float mousespeed;
    float xrot,yrot;
    public float minx, maxx;

 
    void Start()
    {
        
    }
    void LateUpdate()
    {
        xrot -= Input.GetAxis("Mouse Y")* Time.deltaTime*mousespeed;
        yrot += Input.GetAxis("Mouse X")*Time.deltaTime*mousespeed;
        xrot = Mathf.Clamp(xrot, minx, maxx);
        transform.localRotation = Quaternion.Euler(0, yrot, 0);
        transform.GetChild(0).localRotation = Quaternion.Euler(xrot, 0, 0);
        

    }

    
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position , target.transform.position, 0.2F);
    }
}
