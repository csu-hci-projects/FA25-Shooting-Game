using UnityEngine;

public class ply_branch : MonoBehaviour
{
     void Awake()
    {
        Admin.Registerplayer(this.gameObject);
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
