using UnityEngine;
using UnityEngine.AI;

public class PathfindingAI : MonoBehaviour
{
    private NavMeshAgent m_agent;

    public Transform m_target;

    public LayerMask whatIsGround, whatIsFood;

    public float timeSpeed = 1f; //edit this value to speed up sim

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //States
    public float sightRange;
    public bool targetInSightRange;

    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        Time.timeScale = timeSpeed;

        //if the sim is ended reset path.
        if (!DayManager.m_isSimStarted && m_agent.hasPath)
        {
            m_agent.ResetPath();
            return;
        }
        else if (!DayManager.m_isSimStarted)
        {
            return;
        }

        //Check for sight range
        targetInSightRange = Physics.CheckSphere(this.transform.position, sightRange, whatIsFood);

        //find and assign target object
        try
        {
            GameObject m_targetObj = FindClosestFood(); //find target object
            if (m_targetObj)
            {
                m_target = m_targetObj.transform;
            }
        }
        catch (System.NullReferenceException e) 
        {
            Debug.Log(e);
        }

        if (!targetInSightRange) Patrolling();
        if (targetInSightRange) ChasePlayer();
    }

    private void Patrolling()
    {
        //Debug.Log($"{this.name} Is Patrolling");

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            m_agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        //Debug.Log($"{this.name} is Chasing!");

        if (m_target)
        {
            //Debug.Log(m_target.position);
            //Debug.Log(new Vector3(0, 0, 0));
            m_agent.SetDestination(m_target.position);
        }
    }

    private GameObject FindClosestFood()
    {
        GameObject[] foods;
        foods = GameObject.FindGameObjectsWithTag("Food");

        GameObject closest = null;
        Vector3 position = transform.position;

        float viewDistance = Mathf.Infinity; //Field of View!!!!

        foreach (GameObject food in foods)
        {
            Vector3 diff = food.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < viewDistance)
            {
                closest = food;
                viewDistance = curDistance;
            }
        }
        if (closest)
        {
            return closest;
        }
        return null;
    }
}
