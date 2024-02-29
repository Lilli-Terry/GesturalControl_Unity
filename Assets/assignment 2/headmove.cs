// IMDM Course material
// Author: Myungin Lee
// Date: Spring 2024
// This code demonstrates mapping between gesture to a rigged model
// Landmarks label reference: 
// https://developers.google.com/mediapipe/solutions/vision/pose_landmarker
// https://developers.google.com/mediapipe/solutions/vision/hand_landmarker

using JetBrains.Annotations;
using Mediapipe.Unity.Holistic;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class headmove : MonoBehaviour
{
    static int poseLandmark_number = 32;
    static int handLandmark_number = 20;
    public GameObject tkWhole;
    public static Gesture gen; // singleton
    public bool trigger = false;
    private float distance;
    int totalNumberofLandmark;
    Vector3 tkposition;
    public float spinSpeed = 0f;
    Vector3 right;
    Vector3 left;
    Vector3 foward;
    Vector3 back;

    private void Awake()
    {
        totalNumberofLandmark = poseLandmark_number + handLandmark_number + handLandmark_number; //dont know what for

    }
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        // Assign tk's left and right arms and body position.
        // Averaged 2 vectors to get the stable estimation
        // move rabbit using right hands (right: thumb/index/middle) 
        left = (Gesture.gen.lefthandpos[12] + Gesture.gen.lefthandpos[10] + Gesture.gen.lefthandpos[11] + Gesture.gen.lefthandpos[9]) / 4;
        foward = (Gesture.gen.lefthandpos[8] + Gesture.gen.lefthandpos[6] + Gesture.gen.lefthandpos[7] + Gesture.gen.lefthandpos[5]) / 4;
        right = (Gesture.gen.righthandpos[12] + Gesture.gen.righthandpos[10] + Gesture.gen.righthandpos[11] + Gesture.gen.righthandpos[9]) / 4;
        back = (Gesture.gen.righthandpos[8] + Gesture.gen.righthandpos[6] + Gesture.gen.righthandpos[7] + Gesture.gen.righthandpos[5]) / 4;

        //try force 
        if (right.y < left.y && right.y < foward.y && right.y < back.y)
        {
            if ((int)tkWhole.transform.position.x < 25)
            {
                tkWhole.transform.Translate(Vector3.right * 10 * Time.deltaTime, Space.World);
                tkWhole.transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime, Space.Self);
            }
        }
        else if (left.y < right.y && left.y < foward.y && left.y < back.y)
        {
            if((int)tkWhole.transform.position.x > -25)
            {
                tkWhole.transform.Translate(Vector3.left * 10 * Time.deltaTime, Space.World);
                tkWhole.transform.Rotate(Vector3.left, spinSpeed * Time.deltaTime, Space.Self);
            }
        }
        else if (foward.y < right.y && foward.y < left.y && foward.y < back.y)
        {
            if ((int)tkWhole.transform.position.z < 16)
            {
                tkWhole.transform.Translate(Vector3.forward * 10 * Time.deltaTime, Space.World);
                tkWhole.transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime, Space.Self);
            }
        }
        else if (back.y < right.y && back.y < left.y && back.y < foward.y)
        {
            if ((int)tkWhole.transform.position.z > -16)
            {
                tkWhole.transform.Translate(Vector3.back * 10 * Time.deltaTime, Space.World);
                tkWhole.transform.Rotate(Vector3.back, spinSpeed * Time.deltaTime, Space.Self);
            }
        }
       
        
    }
}
