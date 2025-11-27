using Unity.VisualScripting;
using UnityEngine;

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

    

    

    public State currentState;
    public State[] States;

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

    public int Game()
    {

   

         while(!WinConditon()){
            SetState(1);
            SetState(2);
        }






        
       

        //user input to change states would go here

        //start game state

        /*

       
        //end of game

        SetState(3);
        */
        return 0;
    }
    //method here for basics
    public bool Action()
    {
        bool left = Admin._ply_actions>50?false:true;
        Debug.Log($"Actions Left: {Admin._ply_actions}");
        return left;
        
    
    }





    void Start()
    {

        Debug.Log("Game State machine has started");

        //places a try/save block

        //Start the game from state 0
        //main menu state
        Admin.Players_health[0]= 100;
        Admin.Players_health[0]= 100;
        Admin.Players_scores[0] = 0;
        Admin.Players_scores[0] = 0;

        
    }

    // Update is called once per frame
    void Update()
    {


        if (Action()&&endturn_obj.GetComponent<endturn>().endturn_confirmed==false)
        {
            Debug.Log("You still have a turn left");
        }
        else
        {
             Debug.Log("all done");
             //call next state            
        }

         //int choice = 0;
         //stay withing currnet loop


        
    }
}
