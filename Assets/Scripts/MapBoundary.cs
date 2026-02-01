using UnityEngine;

public class MapBoundary : MonoBehaviour
{
    [Header("Boundary Settings")]
    public float leftBoundary = -10f;
    public float rightBoundary = 10f;
    public float topBoundary = 10f;
    public float bottomBoundary = -5f;
    
    [Header("Auto Setup")]
    public bool createInvisibleWalls = true;
    public bool useCamera = true;
    public float wallThickness = 1f;
    
    void Start()
    {
        if (useCamera)
        {
            SetBoundariesFromCamera();
        }
        
        if (createInvisibleWalls)
        {
            CreateInvisibleWalls();
        }
    }
    
    void SetBoundariesFromCamera()
    {
        Camera cam = Camera.main;
        if (cam == null) return;
        
        float cameraHeight = cam.orthographicSize;
        float cameraWidth = cameraHeight * cam.aspect;
        
        Vector3 cameraPos = cam.transform.position;
        
        leftBoundary = cameraPos.x - cameraWidth;
        rightBoundary = cameraPos.x + cameraWidth;
        topBoundary = cameraPos.y + cameraHeight;
        bottomBoundary = cameraPos.y - cameraHeight;
        
        Debug.Log($"Boundaries set from camera: L={leftBoundary:F1}, R={rightBoundary:F1}, T={topBoundary:F1}, B={bottomBoundary:F1}");
    }
    
    void CreateInvisibleWalls()
    {
        // Left wall
        CreateWall("LeftWall", 
                  new Vector3(leftBoundary - wallThickness/2, (topBoundary + bottomBoundary)/2, 0),
                  new Vector3(wallThickness, topBoundary - bottomBoundary + wallThickness*2, 1));
        
        // Right wall
        CreateWall("RightWall", 
                  new Vector3(rightBoundary + wallThickness/2, (topBoundary + bottomBoundary)/2, 0),
                  new Vector3(wallThickness, topBoundary - bottomBoundary + wallThickness*2, 1));
        
        // Bottom wall
        CreateWall("BottomWall", 
                  new Vector3((leftBoundary + rightBoundary)/2, bottomBoundary - wallThickness/2, 0),
                  new Vector3(rightBoundary - leftBoundary + wallThickness*2, wallThickness, 1));
        
        // Top wall (optional, biasanya tidak perlu)
        // CreateWall("TopWall", 
        //           new Vector3((leftBoundary + rightBoundary)/2, topBoundary + wallThickness/2, 0),
        //           new Vector3(rightBoundary - leftBoundary + wallThickness*2, wallThickness, 1));
    }
    
    void CreateWall(string name, Vector3 position, Vector3 scale)
    {
        GameObject wall = new GameObject(name);
        wall.transform.position = position;
        wall.transform.localScale = scale;
        wall.transform.parent = this.transform;
        
        // Add BoxCollider2D
        BoxCollider2D collider = wall.AddComponent<BoxCollider2D>();
        collider.isTrigger = false;
        
        // Add tag for identification
        wall.tag = "Boundary";
        
        Debug.Log($"Created {name} at {position}");
    }
    
    // Visualize boundaries in Scene view
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        
        // Draw boundary rectangle
        Vector3 center = new Vector3((leftBoundary + rightBoundary)/2, (topBoundary + bottomBoundary)/2, 0);
        Vector3 size = new Vector3(rightBoundary - leftBoundary, topBoundary - bottomBoundary, 0);
        
        Gizmos.DrawWireCube(center, size);
        
        // Draw corner markers
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(leftBoundary, bottomBoundary, 0), 0.5f);
        Gizmos.DrawWireSphere(new Vector3(rightBoundary, bottomBoundary, 0), 0.5f);
        Gizmos.DrawWireSphere(new Vector3(leftBoundary, topBoundary, 0), 0.5f);
        Gizmos.DrawWireSphere(new Vector3(rightBoundary, topBoundary, 0), 0.5f);
    }
}