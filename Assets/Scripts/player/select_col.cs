using UnityEngine;

public class select_col : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Renderer rend;
    public Color defaultColor = Color.blue;
    public Color highlightColor = Color.green;
     public Color selColor = Color.orange;

     bool isSelected = false;



    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = defaultColor;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("highlight")&& !isSelected)
        {
            Debug.Log("Entered select trigger");
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
    
    } 



}

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("highlight")&& !isSelected)
        {
            Debug.Log("Leave select trigger");
               this.gameObject.GetComponent<Renderer>().material.color = defaultColor;
         
        }
    }
}
