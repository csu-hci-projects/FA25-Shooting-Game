using UnityEngine;

public class player_branch : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 void Awake()
    {
        Admin.Registerplayer(this.gameObject);
         Debug.Log("I am ", this.gameObject);
    }
    void OnEnable()
    {
        Debug.Log("I am enabled:Current Player Index: " + Admin._ply_index);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Player C key pressed");
            Admin.Changeplayer();
        }
    }



    void OnDisable()
    {
         Debug.Log("I am disabling new  Player Index: " + Admin._ply_index);
        
    }
    
}
