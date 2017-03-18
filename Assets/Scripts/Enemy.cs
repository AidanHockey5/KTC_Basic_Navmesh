using UnityEngine;
using System.Collections;

public enum State
{
    WAIT,
    PATROL,
    ATTACK
}

public class Enemy : MonoBehaviour 
{
    NavMeshAgent nav;
    GameObject[] waypoints;
    GameObject player;
    public State state = State.PATROL;
    public float timeToWait = 3f;
    public float agroRange = 10f;
    public int health = 100;
    float timer = 0;
    public Vector3 targetWaypoint;
	// Use this for initialization
	void Start () 
    {
        nav = GetComponent<NavMeshAgent>();
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        player = GameObject.FindGameObjectWithTag("Player");
        targetWaypoint = PickNewWaypoint();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(waypoints.Length == 0)
        {
            Debug.LogError("No waypoints found!");
            return;
        }

        if(state == State.PATROL)
        {
            nav.destination = targetWaypoint;
            if (Vector3.Distance(transform.position, targetWaypoint) <= 1f)
                state = State.WAIT;
        }

	    if(state == State.WAIT)
        {
            timer += Time.deltaTime;
            if(timer >= timeToWait)
            {
                timer = 0;
                state = State.PATROL;
                targetWaypoint = PickNewWaypoint();
            }
        }

        if (Vector3.Distance(player.transform.position, transform.position) < agroRange)
            state = State.ATTACK;
        else if (state == State.ATTACK)
        {
            state = State.PATROL;
            targetWaypoint = PickNewWaypoint();
        }

        if(state == State.ATTACK)
        {
            nav.destination = player.transform.position;
        }
	}

    Vector3 PickNewWaypoint()
    {
        return waypoints[Random.Range(0, waypoints.Length)].transform.position; 
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            health -= 10;
            Destroy(other.gameObject);
            if(health <=0)
            {
                Destroy(gameObject);
            }
        }
    }

}
