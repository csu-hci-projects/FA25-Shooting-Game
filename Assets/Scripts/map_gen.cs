using NUnit.Framework;
using UnityEngine;

public class map_gen : MonoBehaviour
{
    [SerializeField]
[Tooltip("Drag your hex tile prefab here")]
public GameObject prefab;
    public int edge = 3;

    public float radius = 1.6f;
    
    public float theta = 0f;
    public float spacing = 1.5f;


    // public float spacing_X = 3.7f;
    // public float spacing_y = -14.6f;
    // public float spacing_z = 97.8f;
   
    

    void Start()
    {
        if (prefab == null) return;

       // Vector3 center = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        //Vector3 center = new Vector3(spacing_X, spacing_y, spacing_z);
        
        Instantiate(prefab, Vector3.zero, Quaternion.identity, this.transform);

        // Calculate hex dimensions with spacing adjustment
        float hexWidth = radius * 2f * spacing;  // Diameter with spacing
        float hexHeight = hexWidth * Mathf.Sqrt(3f) / 2f;  // Height of hexagon

        // Loop for each ring
        for (int ring = 1; ring <= edge; ring++)
        {
            // Number of hexagons in this ring = 6 * ring
            int hexagonsInRing = 6 * ring;
            
            // Angle between each hexagon in this ring
            float angleStep = 360f / hexagonsInRing;
            
            // Start angle for this ring (30 degrees rotation for proper alignment)
            theta = 30f;

            // Place hexagons in this ring
            for (int i = 0; i < hexagonsInRing; i++)
            {
                // Calculate position using hexagonal spacing
                // ring * hexWidth gives us the proper radius for each ring with spacing
                float x = ring * hexWidth * Mathf.Cos(theta * Mathf.Deg2Rad);
                float z = ring * hexWidth * Mathf.Sin(theta * Mathf.Deg2Rad);

                Vector3 pos = new Vector3(x, 0, z);
                
                // Create hexagon tile
                GameObject hex = Instantiate(prefab, pos, Quaternion.identity, this.transform);
                
                // Rotate to next position
                theta += angleStep;
            }
        }
    }
          
    }
