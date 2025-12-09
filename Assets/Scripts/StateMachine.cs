using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;


public class StateMachine : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject endturn_obj;
    public class State
    {
        public string name_;
        public State(string name)
        {
         this.name_ = name;   
        }
        public virtual string PrintStateName() {return "";}
        public virtual void PrevState() { }
        public virtual void NextState() { }
    }
    //hardcode states for use
   public State playerturn;
    public State enemyturn;

    public State endturn;


    

    public int turn = 0;
    public float dis_to_next_hex = 4.3f;
    public State currentState;
    public State[] States;
    public GameObject enemy_prefab;

    public int curr_enemy_num=5;
    public int max_enemy_num = 5;

    public static bool WinConditon()
    {

        if (Admin.Players_health[0] < 0 || Admin.Players_health[1] < 0 || Admin.Players_scores[0] == 100 || Admin.Players_scores[1] == 0)
        {
            return true;
        }
        else
        {
            return false;
        }


    }

      public void SetState(int stateIndex)
    {
        if (stateIndex >= 0 && stateIndex < States.Length)
        {
            currentState = States[stateIndex];
            currentState.PrintStateName();
        }
        else
        {
            Debug.LogError("State index out of range.");
        }
    }

  
    //method here for basics
 

  



    void Start()
    {

        Debug.Log("Game State machine has started");

        //places a try/save block

        //Start the game from state 0
        //main menu state
        Admin.Players_health[0]= 100;
        Admin.Players_health[1]= 100;
        Admin.Players_scores[0] = 0;
        Admin.Players_scores[1] = 0;

        
    }
    
    void enemyturn_state()
    {
        Debug.Log("Enemy Turn State");
        //Admin.inact_player();
        Debug.Log("Enemy count value "+Admin.enemys.Count);

        curr_enemy_num = Admin.enemys.Count;
    
        if(curr_enemy_num < max_enemy_num)
        {

            if ((Admin.turn % 5 == 0) && (Admin.turn > 0))
            {
                max_enemy_num = max_enemy_num + 2;
                Admin._ply_total_actions =  Admin._ply_total_actions +20;
            }

            int leg= curr_enemy_num<5?5-curr_enemy_num:max_enemy_num-curr_enemy_num;
          
            for (int i = 0; i < leg; i++)
            {
                    Vector3 pos = new Vector3();
                    pos = Admin.spawn_points[UnityEngine.Random.Range(0, Admin.spawn_points.Count)].transform.position;
                    pos.y =3.5f;

                    GameObject goon =  Instantiate(enemy_prefab,pos, Quaternion.identity, this.transform);
                   
                        goon.transform.rotation = Quaternion.Euler(0, 180, 0);
                       
                        Admin.enemys.Add(goon);
                            
                        goon.AddComponent<enemy_goons>();
                        goon.AddComponent<EnemeyGunSimple>();
                        goon.GetComponent<enemy_goons>().enabled = false;
                        goon.GetComponent<EnemeyGunSimple>().enabled = false;
                       
                
            }

             Debug.Log("Enemys spawned "+leg);

        }

       
        for (int i = 0; i < curr_enemy_num; i++)
        {

                if (Admin.enemys[i]!= null&&Admin.enemys[i].GetComponent<enemy_goons>()!= null)
                
            {
                Admin.enemys[i].GetComponent<enemy_goons>().enabled = true;
            }

            if (Admin.enemys[i].transform.position.z < 0)
            {
                 Admin.enemys[i].GetComponentInChildren<EnemeyGunSimple>().enabled = true;
               // Admin.enemys[i].GetComponent<EnemeyGunSimple>().enabled = true;
            }
           
        }

        StartCoroutine(Walking_enemy());

        

        
    }

    // Update is called once per frame
    void Update()
    {


        if ((endturn_obj.GetComponent<endturn>().endturn_confirmed==false))
        {
            Admin.Action();
            Debug.Log("You still have a action left");
        }
        else
        {
             Debug.Log("all done");
             enemyturn_state();           
        }

         //int choice = 0;
         //stay withing currnet loop
        
    }


    IEnumerator Walking_enemy()
{
   
    
        // 1. Wait a random delay
        Debug.Log("Enemy moving");
        Debug.Log("Enemys that move"+Admin.enemys.Count);

        yield return new WaitUntil(()=>Admin._enemy_walked>curr_enemy_num);
        Debug.Log("All Enemy have walked");
        Admin._ply_actions = 0;
        Admin._ene_killed = 0;
        endturn_obj.GetComponent<endturn>().endturn_confirmed=false;
        endturn_obj.GetComponent<endturn>().endturn_triggered=false;
        Admin._enemy_walked = 0;
        Admin.turn++;
        //Admin.act_player();


        




}

}
