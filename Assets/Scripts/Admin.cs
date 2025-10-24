using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Admin
{
    public static int totalEngines = 0;

    public static int totalMissles = 0;
    public static int[] Missles_Order= new int[5];

    public static int _missile_index = 0;

    public static bool triggered = false;
    
    public static int _cam_index = 0;

    public static int _cam_total = 0;
    public static GameObject[] cameras = new GameObject[5];

    public static void RegisterEngine()
    {
        totalEngines++;
        //Debug.Log($"Total engines registered: {totalEngines}");
    }
    
    public static int RegisterMissle()
    {
        totalMissles++;
        Debug.Log($"Total missles registered: {totalMissles}");
        Missles_Order[totalMissles - 1] = totalMissles;


        return totalMissles;
    }



 public static int RegisterCAM(GameObject cam)
    {
        _cam_total++;
        Debug.Log($"Total cams registered: {_cam_total}");
        cameras[_cam_total - 1] = cam;


        return _cam_total;
    }








}