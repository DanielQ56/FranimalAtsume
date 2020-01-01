using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float xdrag = 0.3f;
    Transform camTrans;

    bool mouseIsDown = false;
    Vector3 originPosition;

    private void Awake()
    {
        camTrans = this.gameObject.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            originPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if(Input.GetMouseButton(0))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - originPosition;
            float xval = position.x;


            camTrans.position = Vector3.Lerp(camTrans.position, camTrans.position + Vector3.left * xval, xdrag);
            camTrans.position = new Vector3(Mathf.Clamp(camTrans.position.x, -5, 5), camTrans.position.y, camTrans.position.z);
        }
    }
}
