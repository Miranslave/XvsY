using Unity.VisualScripting;
using UnityEngine;

public class Summoned : MonoBehaviour
{

    public GameObject toFollowed;
    public Vector3 Offset;
    

    // Update is called once per frame
    void Update()
    {
        if (toFollowed)
        {
            Follow(toFollowed,Offset);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    private void Follow(GameObject g,Vector3 offset)
    {
        this.transform.position = g.transform.position + offset;
    }
    private void Follow(GameObject g)
    {
        this.transform.position = g.transform.position;
    }
    
}
