using UnityEngine;

public class select_col : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Renderer rend;
    public Color defaultColor = Color.blue;
    public Color highlightColor = Color.green;
     public Color selColor = Color.orange;

     bool isSelected = false;
     public bool has_enemy = false;



    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = defaultColor;
        string parentTag = transform.parent.tag;
    }

    void has_enemy_check(Collider other){
        if(other.CompareTag("has_enemy")){
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }   

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("highlight")&& !isSelected)
        {
           // Debug.Log("Entered select trigger");
            this.gameObject.GetComponent<Renderer>().material.color = highlightColor;
            
         
        }
    }


    void OnTriggerStay(Collider other)
{
    if (other.CompareTag("select"))
    {
        // Slowly reduce player health while inside a damage zone
         this.gameObject.GetComponent<Renderer>().material.color = selColor;
        isSelected = true;
        Admin.RegisterTile();
        Admin._ply_actions++;
    
    } 
     has_enemy_check(other);

    



}

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("highlight")&& !isSelected)
        {
            //Debug.Log("Leave select trigger");
               this.gameObject.GetComponent<Renderer>().material.color = defaultColor;
         
        }
        
    }
}
