using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class week3 : MonoBehaviour
{
    public Quaternion rotation;
    private float x, y, z;
    public Vector3 currentEulerAngles;
    public float rotSpeed;

    public Transform targetA;
    public Transform targetB;
    public Transform targetC;

    public float timecount = 0.0f;

    public void Start()
    {
        transform.rotation = quaternion.Euler(90, 90, 90);
        transform.rotation = quaternion.identity;
    }

    private void Update()
    {
        //rotationInputs();
        QuaternionRotatTowards();
        //QuaternionSlerp();
        //lookRotation();
    }

    public void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 18;


        //Use the euler angles to show the euler angls of thee transform rotation
        GUI.Label(new Rect(10, 0, 0, 0),"rotation on X" + x + "y" + y + "z" + z, style);
        //Output the quatenrion.euler angle value
        GUI.Label(new Rect(18, 25, 0, 0), " current euler angles" + currentEulerAngles, style);
        //Output transform.angle of the gameobjct
        GUI.Label(new Rect(18, 50, 0, 0), "GameObject world enter angle" + transform.eulerAngles, style);
    }

    private void rotationInputs()
    {
        if (Input.GetKeyDown(KeyCode.X)) { x = 1 - x; }
        if (Input.GetKeyDown(KeyCode.Y)) { y = 1 - y; }
        if (Input.GetKeyDown(KeyCode.Z)) { z = 1 - z; }
        if(Input.GetKeyDown(KeyCode.UpArrow)) { rotSpeed+=3; }
        if (Input.GetKeyDown(KeyCode.UpArrow)) { rotSpeed--; }

        currentEulerAngles += new Vector3(x, y, z) * Time.deltaTime * rotSpeed;
        rotation.eulerAngles = currentEulerAngles;
        transform.rotation = rotation;
    }
    public void QuaternionRotatTowards()
    {
        var step = rotSpeed * Time.time;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetA.rotation, step);
        transform.position = Vector3.Lerp(transform.position, targetC.position, rotSpeed * Time.deltaTime);
    }

    public void QuaternionSlerp()
    {
        transform.rotation = Quaternion.Slerp(targetA.rotation,targetB.rotation, timecount);
        timecount = timecount + Time.deltaTime;
    }

    public void lookRotation()
    {
        Vector3  relativePos = targetA.position - transform.position;
        Quaternion rotaion = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotaion;
    }
}
