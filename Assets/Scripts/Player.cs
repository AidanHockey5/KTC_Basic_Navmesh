using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    public GameObject bullet;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
	}

    void Fire()
    {
        GameObject b = Instantiate(bullet, Camera.main.transform.position, Camera.main.transform.rotation) as GameObject;
        b.GetComponent<Rigidbody>().AddForce(b.transform.forward * 1000);
        Destroy(b, 10);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Cursor.lockState = CursorLockMode.None;
            Application.LoadLevel(1);
        }
    }

}
