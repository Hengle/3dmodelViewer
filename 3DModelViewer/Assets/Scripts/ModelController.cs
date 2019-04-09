﻿using UnityEngine;

public class ModelController : MonoBehaviour
{
    private GameObject go;
    private float lastPosX;
    private float lastPosY;

    private ButtonController bc;

    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.

    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        go = GameObject.Find("model");
        bc = GameObject.Find("ModelController").GetComponent<ButtonController>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // If there are two touches on the device...
        // CODE FroM : https://unity3d.com/learn/tutorials/topics/mobile-touch/pinch-zoom
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            //// If the camera is orthographic...
            //if (camera.isOrthoGraphic)
            //{
            //    // ... change the orthographic size based on the change in distance between the touches.
            //    camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

            //    // Make sure the orthographic size never drops below zero.
            //    camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
            //}
            //else
            //{
                // Otherwise change the field of view based on the change in distance between the touches.
                camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 45 and 90.
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 45.1f, 89.9f);
            //}
        }
    }

    void OnMouseDown()
    {
        bc.CloseTextBox();
    }


    void OnMouseDrag()
    {
        float diffX;
        float diffY;

        diffX = lastPosX - Input.mousePosition.x;
        diffY = lastPosY - Input.mousePosition.y;

        go.transform.Rotate(Vector3.up, diffX);
        go.transform.Rotate(Vector3.right, diffY);


        lastPosX = Input.mousePosition.x;
        lastPosY = Input.mousePosition.y;
    }
}
