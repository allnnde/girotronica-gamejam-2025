
using System;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStateMachine : MonoBehaviour
{
    public EnemyBaseState currentState;

    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();

    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public PlayerMovimentController player;

    public Transform[] patrolPoints;
    [HideInInspector] public int patrolIndex = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovimentController>();
    }

    private void Start()
    {
        SwitchState(patrolState);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState newState)
    {
        if (currentState != null)
            currentState.ExitState(this);

        currentState = newState;
        newState.EnterState(this);
    }

    public void SetChaseState()
    {
        SwitchState(chaseState);
    }

    public void SetPatrolState()
    {
        SwitchState(patrolState);
    }

    void LateUpdate()
    {
        if (player == null) return;
       
            // --- Solo rotación en el eje Y ---
            Vector3 dir = player.transform.position - transform.position;
            dir.y = 0;  // Evitamos inclinación
            if (dir.sqrMagnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(dir);
            }
      
    }
}