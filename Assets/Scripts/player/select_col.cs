using UnityEngine;

public class select_col : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Renderer rend;
    public Color defaultColor = Color.blue;
    public Color highlightColor = Color.green;
    public Color selColor = Color.orange;
    public AudioSource audioSource;
    public AudioClip selectClip;
    public float playCooldown = 0.1f;
    private float lastPlayTime = 0f;

     bool isSelected = false;
     public bool has_enemy = false;



    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = defaultColor;
        string parentTag = transform.parent.tag;
    }

    void Update()
    {
        if((Admin.turn%5==0)&&(this.gameObject.GetComponent<Renderer>().material.color == Color.red))
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.darkRed;
            Admin.RegisterSpawnPoint(this.gameObject);
            
        }

    }

    void has_enemy_check(Collider other){
        if(other.CompareTag("has_enemy")){
            Admin.Players_scores[1]++;
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
    if (other.CompareTag("select")&&!(this.gameObject.GetComponent<Renderer>().material.color == Color.darkRed))
    {
        
         this.gameObject.GetComponent<Renderer>().material.color = selColor;
        isSelected = true;
        Admin.RegisterTile();
      
        Admin._ply_actions++;
          Debug.Log("this one "+Admin._ply_actions);
        if(audioSource != null && selectClip != null && Time.time - lastPlayTime >= playCooldown)
        {
            audioSource.PlayOneShot(selectClip);
            lastPlayTime = Time.time;
        }
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
