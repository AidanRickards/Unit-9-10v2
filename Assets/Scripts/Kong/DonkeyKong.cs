using UnityEngine;

public class DonkeyKong : MonoBehaviour
{
    public GameObject barrel;
    public GameObject point;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnBarrel()
    {
        Instantiate(barrel, point.transform.position, Quaternion.identity);
    }
}
