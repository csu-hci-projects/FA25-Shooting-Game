using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Unity.Multiplayer.Center.Common;
using System.Collections.Generic;

public class enemy_goons : MonoBehaviour
{


    List<GameObject> goonsPrefabs;
    [Tooltip("Drag your goons prefab here")]
    public GameObject prefab;
    

    Vector3 orginal_pos;
    Vector3 target_pos;
    Vector3 current_pos;

    [Tooltip("distance to next tile")]
    public float dis_to_next_hex = 4.3f;





    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //place goons in the world they will be placed with the map gen script
        goonsPrefabs = Admin.enemys;
    


    }

        void OnEnable()
    {
 
        target_pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - dis_to_next_hex);
       
    }


    // Update is called once per frame
    void Update()
    {

        

        transform.position = Vector3.MoveTowards(transform.position, target_pos, 3f * Time.deltaTime);
        transform.LookAt(target_pos);

        if(transform.position == target_pos)
        {
            Admin._enemy_walked++;
            this.enabled = false;
        }




                
    }


        
        
}
