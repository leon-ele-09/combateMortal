using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poses : MonoBehaviour
{
    // Start is called before the first frame update

    public HandTracking tracking;
    public int[] poseData = new int[5];
    public int pose;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pose = -1;
        poseData[0] = 0;
        poseData[1] = 0;
        poseData[2] = 0;
        poseData[3] = 0;
        poseData[4] = 0;
        // Light Punch - jab der. 180°

        if ((tracking.data1[0] > 160 && tracking.data1[0] < 200) && (tracking.data1[2] > 160 && tracking.data1[2] < 200))
        {
            poseData[0] = 1;
        }

        // Serious Punch - jab izq.  

        if ((tracking.data1[1] > 160 && tracking.data1[1] < 200) && (tracking.data1[3] > 160 && tracking.data1[3] < 200))
        {
            poseData[1] = 1;
        }


        // Push
        if (poseData[0] == 1 && poseData[1] == 1)
        {

            poseData[0] = 0;
            poseData[1] = 0;
            poseData[2] = 1;

        }


        // You are my Specialz

        if ((tracking.data1[0] > 170 && tracking.data1[0] < 230) && (tracking.data1[2] > 170 && tracking.data1[2] < 230))
        {
            if ((tracking.data1[1] > 130 && tracking.data1[1] < 170) && (tracking.data1[3] > 130 && tracking.data1[3] < 170))
            {


                poseData[0] = 0;
                poseData[1] = 0;
                poseData[2] = 0;
                poseData[3] = 1;

            }

        }

        for(int k = 0; k < 4; k++)
        {
            if (poseData[k] == 1) { pose = k; }
        }

    }
}
