using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestTower : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
    Camera MainCamera;
    Vector3 ofset;

    void Awake() 
    {
        MainCamera = Camera.allCameras[0];
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        ofset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector3 pos = MainCamera.ScreenToWorldPoint(eventData.position);
        pos.z = 0;
        transform.position = pos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        
    }

    
}
