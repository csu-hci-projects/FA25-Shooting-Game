using UnityEngine;
using System.Collections;

public class enemy_eng : MonoBehaviour
{

    int total_goons = 5;
    GameObject[] goonsPrefabs;
    [Tooltip("Drag your goons prefab here")]
    public GameObject prefab;

    Vector3 orginal_pos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //place goons in the world they will be placed with the map gen script
        goonsPrefabs = Admin.enemys;
        orginal_pos = this.transform.position;
      



    }

    // Update is called once per frame
    void Update()
    {

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
            StartCoroutine(randomwalk());
        }
        
        }

IEnumerator randomwalk()
{
   
    {
        // 1. Wait a random delay
        float wait = Random.Range(5f, 7f);   // random time between 1–5 seconds
        yield return new WaitForSeconds(wait);

     

      
    
    }
}
        
}
