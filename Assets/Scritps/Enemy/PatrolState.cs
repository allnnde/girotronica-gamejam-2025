public class PatrolState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        enemy.agent.isStopped = false;
        enemy.agent.SetDestination(enemy.patrolPoints[enemy.patrolIndex].position);
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        if (!enemy.agent.pathPending && enemy.agent.remainingDistance < 0.3f)
        {
            enemy.patrolIndex = (enemy.patrolIndex + 1) % enemy.patrolPoints.Length;
            enemy.agent.SetDestination(enemy.patrolPoints[enemy.patrolIndex].position);
        }
    }

    public override void ExitState(EnemyStateMachine enemy)
    {
        // Nada especial para limpiar
    }
}
