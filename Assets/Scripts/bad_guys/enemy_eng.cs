using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Unity.Multiplayer.Center.Common;

public class enemy_eng : MonoBehaviour
{

    int total_goons = 5;
    GameObject[] goonsPrefabs;
    [Tooltip("Drag your goons prefab here")]
    public GameObject prefab;

    Vector3 orginal_pos;
    Vector3 target_pos;
    Vector3 current_pos;

    bool isdone;
    bool lk;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //place goons in the world they will be placed with the map gen script
        goonsPrefabs = Admin.enemys;
        orginal_pos = this.transform.position;
        target_pos = orginal_pos;

    

    }

    // Update is called once per frame
    void Update()
    {
   
        transform.position = Vector3.MoveTowards(transform.position, target_pos, 3f * Time.deltaTime);
        transform.LookAt(target_pos);
      
        isdone = (transform.position == target_pos)? true : false;


        if (isdone)
        {
         
            StartCoroutine(randomwalk());

        }

/*
        if(Vector3.Distance(orginal_pos, this.transform.position) > 10)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, orginal_pos, 5f * Time.deltaTime);
            transform.LookAt(orginal_pos);
             StartCoroutine(randomwalk());
        }else{
        float x = Random.Range(-10, 10); 
        float z = Random.Range(-10, 10); 

        Vector3 current_pos = this.transform.position;  

        Vector3 targetPos = new Vector3(current_pos.x + x, current_pos.y, current_pos.z + z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 3f * Time.deltaTime);
        transform.LookAt(targetPos);
            StartCoroutine(randomwalk(orginal_pos));
        }
        */

        }

IEnumerator randomwalk()
{
   
    {
        // 1. Wait a random delay
        Debug.Log("Enemy moving");
        lk = false;
        float wait = Random.Range(10f, 15f);   // random time between 1–5 seconds
        yield return new WaitForSeconds(wait);

        if(!lk){
            lk =true;
            float x;
            float z;

            if(Vector3.Distance(orginal_pos, this.transform.position) > 5)
            {
                x = orginal_pos.x - this.transform.position.x;
                z = orginal_pos.z - this.transform.position.z;
                
            }else{
                 x = Random.Range(-10, 10); 
                 z = Random.Range(-5, 5); 
            
            }

            Debug.Log("Enemy new target pos");
            target_pos = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        }
    
    }
}


        
}
