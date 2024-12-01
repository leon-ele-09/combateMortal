using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracking : MonoBehaviour
{
    // Start is called before the first frame update
    public UDPReceive udpReceive;
    public GameObject[] handPoints;
    public int[] data1;
    void Start()
    {
        data1 = new int[5];
    }

    // Update is called once per frame
    void Update()
    {
        string data = udpReceive.data;

        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        //print(data);
        string[] points = data.Split(',');
        //print(points[0]);

        //0        1*3      2*3
        //x1,y1,z1,x2,y2,z2,x3,y3,z3

        for (int i = 0; i < 32; i++)
        {

            float x = 7 - float.Parse(points[i * 3]) / 100;
            float y = float.Parse(points[i * 3 + 1]) / 100;
            float z = float.Parse(points[i * 3 + 2]) / 100;

            handPoints[i].transform.localPosition = new Vector3(x, y, 10);

            
        }

        data1[0] = 0;

        data1[2] = CalculateAngle(11, 13);
        //mano
        data1[0] = CalculateAngle(11, 15);

        // Angle between 12 and 14 ; 12 and 16
        data1[3] = CalculateAngle(12, 14);
        //mano
        data1[1] = CalculateAngle(12, 16);



    }

    int CalculateAngle(int id1, int id2)
    {
        // Ensure the indices are within bounds
        if (id1 < 0 || id1 >= handPoints.Length || id2 < 0 || id2 >= handPoints.Length)
        {
            Debug.LogError($"Invalid indices: id1={id1}, id2={id2}");
            return 0;
        }

        // Get the positions of the points
        Vector3 pos1 = handPoints[id1].transform.position;
        Vector3 pos2 = handPoints[id2].transform.position;

        // Calculate the direction vector
        Vector3 direccion = pos2 - pos1;

        // Calculate the angle in degrees using Atan2
        float angleInRadians = Mathf.Atan2(direccion.y, direccion.x);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        // Ensure the angle is positive (0-360)
        if (angleInDegrees < 0)
        {
            angleInDegrees += 360f;
        }

        // Return the rounded angle
        return Mathf.RoundToInt(angleInDegrees);
    }

}