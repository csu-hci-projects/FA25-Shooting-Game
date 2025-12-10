using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using Unity.Cinemachine;
using System.Collections.Generic;
using NUnit.Framework;

public class Admin
{

    public static int totalEngines = 0;

    public static int totalMissles = 0;
  
    // player is index 0 and enemy is 1
    public static int[] Players_scores= new int[2];
    public static int[] Players_health= new int[2];

   

    public static List<GameObject>  spawn_points = new List<GameObject>();
  

    public static float Enemy_shot_chance = 0f;




    public static int _enemy_walked = 0;
        public static int turn = 0;

    public static bool triggered = false;
    
    public static int _cam_index = 0;
    public static int _ply_index = 0;
    public static int _cam_total = 0;
    public static int _ply_total = 0;
    public static int _ply_actions = 0;

        public static int _ply_total_actions = 50;

          public static int _ene_killed = 0;

        public static int _ene_killed_total = 5;

    public static GameObject[] cameras = new GameObject[5];
    public static GameObject[] players = new GameObject[5];
     

    public static List<GameObject>  enemys = new List<GameObject>();


  


    public static void RegisterEngine()
    {
        totalEngines++;
        //Debug.Log($"Total engines registered: {totalEngines}");
    }
    
    public static void RegisterTile()
    {
        Players_scores[0] += 1;
        Debug.Log($"Total registered: {totalMissles}");

        
    }

    public static void RegisterSpawnPoint(GameObject tile)
    {
     spawn_points.Add(tile);
   
    
    }





    public static int RegisterCAM(GameObject cam)
    {
        _cam_total++;
        _cam_index = _cam_total - 1;
        Debug.Log($"Total cams registered: {_cam_total}");
        cameras[_cam_index] = cam;

        if(_cam_total == 1)
        {
            cameras[0].GetComponent<Camera>().enabled = true;
        }
        else
        {
            cameras[_cam_index].GetComponent<Camera>().enabled = false;
        }


        return _cam_total;
    }

    
    public static void ChangeCamtest()
    {
        cameras[_cam_index].GetComponent<Camera>().enabled = false;

        _cam_index++;


        if (_cam_index > _cam_total-1)
        {
            _cam_index = 0;
        }
        Debug.Log($"current cams index affer add: {_cam_index}");

        cameras[_cam_index].GetComponent<Camera>().enabled = true;


    }


    


    public static int Registerplayer(GameObject ply)
    {
        _ply_total++;
        _ply_index = _ply_total - 1;
        Debug.Log($"Total player registered: {_ply_total}");
        players[_ply_index] = ply;
        //players[_ply_index].GetComponent<player_branch>()

    if(players[_ply_index].GetComponent<player_branch>().order_num==0)
        {
            players[_ply_index].SetActive(true);
        }
        else
        {
            players[_ply_index].SetActive(false);
        }


        return _ply_total;
    }

    public static void inact_player()
    {
     players[_ply_index].SetActive(false);
    }

       public static bool Action()
    {
        bool left = Admin._ply_actions>_ply_total_actions?false:true;
        float num_f = Admin._ply_actions;
        
        int num = (_ply_total_actions - Admin._ply_actions)/10 ;
       
        Debug.Log($"Actions Left: {num}");
        return left;
        
    
    }

           public static bool Action_shoot()
    {
        bool left = _ene_killed>_ene_killed_total?false:true;
        int num = _ene_killed_total - _ene_killed ;
        
        Debug.Log($"Actions Left: {num}");
        return left;
        
    
    }

 



public static void act_player()
    {
        
        players[_ply_index].SetActive(true);
    }

    public static void Changeplayer()
    {
        players[_ply_index].SetActive(false);

        _ply_index++;
        if (_ply_index >= _ply_total)
        {
            _ply_index = 0;
        }

        players[_ply_index].SetActive(true);

    }

    public static int ActionsLeft()
    {
        return _ply_total_actions - _ply_actions;
    }

    









}