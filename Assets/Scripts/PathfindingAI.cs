using UnityEngine;
using UnityEngine.AI;

public class PathfindingAI : MonoBehaviour
{
    private NavMeshAgent m_agent;

    public Transform m_target;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //States
    public float sightRange;
    public bool targetInSightRange;

    private void Awake()
    {
        m_target = GameObject.Find("Player").transform;
        m_agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight range
        targetInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!targetInSightRange) Patroling();
        if (targetInSightRange) ChasePlayer();
    }

    private void Patroling()
    {
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
        if (m_target)
        {
            m_agent.SetDestination(m_target.position);
        }
    }
}
