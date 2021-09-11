using UnityEngine;
using UnityEngine.AI;

public class CreatureManagement : MonoBehaviour
{
    NavMeshAgent m_agent;

    Transform m_target;

    LayerMask whatIsGround, whatIsFood;

    public int m_speciesID, m_creatureID, m_curFitness;
    private int foodcount, offspring;

    //genetics
    public float m_geneticMoveSpeed, m_geneticMass, m_mutationRate;
    
    //Patrolling
    Vector3 walkPoint;
    bool walkPointSet;
    float walkPointRange;

    //States
    public float m_geneticSightRange;

    bool targetInSightRange;

    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_speciesID = Random.Range(0, 2147483647);
        m_creatureID = this.gameObject.GetInstanceID();
        m_curFitness = 0;
        foodcount = 0;
        offspring = 0;
        walkPointRange = 10f;

        //set layers
        whatIsFood = LayerMask.NameToLayer("whatIsFood");
        whatIsGround = LayerMask.NameToLayer("whatIsGround");
        //init variables for first generation, need to randomise for variation and mutation.
        //randomise genetic values
        m_geneticMass = 100f + Random.Range(-20f, 20f);
        m_geneticMoveSpeed = 5f + Random.Range(-1f, 1f);
        m_geneticSightRange = 8f + Random.Range(-1.5f, 1.5f);
        m_mutationRate = 1f + Random.Range(-0.1f, 0.1f);
    }

    private void FixedUpdate()
    {
        //if the sim is ended set speed to 0.
        if (DayManager.m_isSimStarted == false && m_agent.hasPath)
        {
            SetMovementNull();
            return;
        }
        else if (DayManager.m_isSimStarted == false)
        {
            SetMovementNull();
            return;
        }

        Time.timeScale = DayManager.m_timeSpeed;

        if (m_agent.speed == 0f && m_agent.angularSpeed == 0f) UpdateMoveSpeed(m_geneticMoveSpeed);//if passed set speed back to normal

        //Check for sight range
        targetInSightRange = Physics.CheckSphere(this.transform.position, m_geneticSightRange, whatIsFood);

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

    /* ------------- Food and Fitness -------------*/
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            this.foodcount++;
        }
    }

    /*------------ Get Set methods ----------------*/
    public void SetMoveSpeed(float newMoveSpeed)//set genetic move speed
    {
        this.m_geneticMoveSpeed = newMoveSpeed;
    }

    public void UpdateMoveSpeed(float newMoveSpeed)//update move speed
    {
        this.m_agent.speed = newMoveSpeed;
    }

    public void SetSigtRange(float newSightRange)
    {
        this.m_geneticSightRange = newSightRange;
    }

    public void SetGeneticMass(float newGeneticMass)
    {
        this.m_geneticMass = newGeneticMass;
    }

    public float GetGeneticMass()
    {
        return this.m_geneticMass;
    }

    public void SetMutationRate(float newMutationRate)
    {
        this.m_mutationRate = newMutationRate;
    }

    public float GetMutationRate()
    {
        return this.m_mutationRate;
    }

    public void SetMovementNull()
    {
        this.m_agent.angularSpeed = 0f;
        this.m_agent.speed = 0f;
    }

    public void AddToFoodcount(int foodcountDelta)
    {
        this.foodcount += foodcountDelta;
    }

    public void SetFoodcount(int newFoodcount)
    {
        this.foodcount = newFoodcount;
    }

    public int GetFoodcount()
    {
        return this.foodcount;
    }

    public void AddToOffspring(int offspringDelta)
    {
        this.offspring += offspringDelta;
    }
    public int GetOffspring()
    {
        return this.offspring;
    }
}
