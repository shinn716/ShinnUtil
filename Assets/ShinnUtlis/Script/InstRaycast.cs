﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InstRaycast : MonoBehaviour {

    public enum CameraState
    {
        perspective,
        orthographic
    }

    [Header("Camera State")]
    public bool enable = true;
    public Camera c;
    public CameraState camstate;
    public float raycastDist = 10;

    [Header("Instantiate State")]
    public int gentime = 100;
    public GameObject prefab;

    int count = 0;

    private void Start () {
        if (c == null)
            c = Camera.main;
	}

    private void FixedUpdate () {
        if (enable) 
            Select(camstate);
	}


    private void Select(CameraState cam) {

        switch (cam) {
            default:
                break;

            case CameraState.perspective:
                count++;
                if (count > gentime)
                {
                    count = 0;
                    GameObject go1 = Instantiate(prefab);
                    go1.transform.localPosition = GetWorldPositionOnPlane(Input.mousePosition, raycastDist);
                }
                break;

            case CameraState.orthographic:
                GameObject go2 = Instantiate(prefab);
                go2.transform.localPosition = new Vector3(c.ScreenToWorldPoint(Input.mousePosition).x, c.ScreenToWorldPoint(Input.mousePosition).y, raycastDist);
                break;
        }
    }

    private Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }


}
