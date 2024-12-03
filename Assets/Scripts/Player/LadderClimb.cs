using Player;
using UnityEditor.AssetImporters;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public BoxCollider2D bx;
    public PlayerScript ps;

    private void Start()
    {
        bx = GetComponent<BoxCollider2D>();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        print(collision.tag);
        if(collision.CompareTag("Bottom"))
        {
            ps.upClimb = true;
            ps.downClimb = false;
            print("At bottom");
        }
        if(collision.CompareTag("Top"))
        {
            ps.upClimb = false;
            ps.downClimb = true;
        }
        else
        {
            ps.upClimb = true;
            ps.downClimb = true;
        }
    }
    /*
    public void OnTriggerExit2D(Collider2D collision)
    {
        ps.upClimb = false;
        ps.downClimb = false;
    }
    */

    private void Update()
    {
        print("Doing ladder climb");
    }
}
