using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float minX, minY;
    [SerializeField] private float maxX, maxY;
    public float MinX => minX;
    public float MinY => minY;
    public float MaxX => maxX;
    public float MaxY => maxY;
    private void LateUpdate()
    { 
        Vector3 targetpositon = target.position; // lay vi tri cua doi tuong can theo doi
        targetpositon.z = -10;

        targetpositon.x = Mathf.Clamp(targetpositon.x, minX, maxX); // gioi han vi tri x cua camera
        targetpositon.y = Mathf.Clamp(targetpositon.y, minY, maxY); // gioi han vi tri y cua camera
        transform.position = targetpositon; // dat vi tri cua camera bang vi tri cua doi tuong can theo doi
    }
}
 