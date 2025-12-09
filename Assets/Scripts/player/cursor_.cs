using System.Data.Common;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class cursor_ : MonoBehaviour
{

    public GameObject selector;
      GameObject selector_instance;
       Vector3 def;
        Vector3 s;


          
 
      
      

/*
     public float height = 8f;
     public Camera customCam;

    void Update()
    {
        // Follow the mouse in world space
        Ray ray = customCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
        {
            transform.position = hit.point + Vector3.up * height;
        }
    }

*/



    // Start is called once before the first execution of Update after the MonoBehaviour is created
     
    private InputAction clickAction;
        private void Awake()
    {
     clickAction = new InputAction(
            type: InputActionType.PassThrough,
            binding: "<Mouse>/leftButton"
        );
    }


    
    void Start()
    {
        Cursor.visible = false;    
              
      
    }
    
        private void OnEnable()
    {
        
        clickAction.Enable();
        clickAction.performed += OnClickPerformed;
        Cursor.lockState = CursorLockMode.Confined;

    
    }

    // Update is called once per frame
    void Update()
    {


        // s = selector.transform.localScale;
        // s.y = 3f;   // only affect Y
        // selector.transform.localScale = s;


     

     

        Vector2 pos = Input.mousePosition;

   

        float posx = math.remap(3, 963, -42,42, pos.x);
        float posz = math.remap(0, 512, -18, 23, pos.y);

        //Debug.Log("mousex bound " + pos.x+" mousez bound " + pos.y);




        Vector3 pos_ = new Vector3(-1*(posz) , 8, posx);
        gameObject.GetComponent<RectTransform>().anchoredPosition3D = pos_;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }




    }

        private void OnDisable()
    {
        clickAction.performed -= OnClickPerformed;
        clickAction.Disable();
        Cursor.lockState = CursorLockMode.None;


    }

        private void OnClickPerformed(InputAction.CallbackContext ctx)
    {

       

        if (Admin._ply_actions<50){
        selector_instance  = Instantiate(selector, selector.transform.position, Quaternion.identity, this.transform);
        selector_instance.SetActive(true);
        Destroy(selector_instance, 0.1f);
        }
        else
        {
            Debug.Log("No actions left");
        }
        // canClick = false;
        // }else{
        //     canClick = true;
        // }
    


     
      

        //selector.transform.localScale = def;
        
    }

    IEnumerator Wait()
{
    yield return new WaitForSeconds(1f);
    
}



    
}
