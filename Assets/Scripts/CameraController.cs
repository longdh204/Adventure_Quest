using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private Transform target; // bien luu tham chieu den doi tuong can theo doi
    //[SerializeField] private Vector3 offset; // khoang cach giua camera va doi tuong
    [Header("Setiings")]
    [SerializeField] private Vector2 minMaxXY;

    private void LateUpdate()
    { 
        //offset = new Vector3(0, 0, -10); // Dat khoang cach giua camera va doi tuong  
        //transform.position = target.position + offset;

        Vector3 targetpositon = target.position; // lay vi tri cua doi tuong can theo doi
        targetpositon.z = -10;

        targetpositon.x = Mathf.Clamp(targetpositon.x, -minMaxXY.x, minMaxXY.x); // gioi han vi tri x cua camera
        targetpositon.y = Mathf.Clamp(targetpositon.y, -minMaxXY.y, minMaxXY.y); // gioi han vi tri y cua camera
        transform.position = targetpositon; // dat vi tri cua camera bang vi tri cua doi tuong can theo doi
    }
}
 