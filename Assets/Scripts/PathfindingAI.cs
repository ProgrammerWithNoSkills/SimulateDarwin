using UnityEngine;
using UnityEngine.AI;

public class PathfindingAI : MonoBehaviour
{
    private NavMeshAgent m_agent;

    public Transform m_target;

    public LayerMask whatIsGround, whatIsFood;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //States
    public float sightRange;
    public bool targetInSightRange;

    private void Awake()
    {
        m_target = GameObject.FindWithTag("Food").transform;
        //Debug.Log(m_target.position);
        m_agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight range
        targetInSightRange = Physics.CheckSphere(this.transform.position, sightRange, whatIsFood);

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
}
