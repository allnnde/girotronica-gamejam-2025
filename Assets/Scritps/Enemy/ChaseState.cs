public class ChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        enemy.agent.isStopped = false;
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        if (enemy.player != null)
        {
            enemy.agent.SetDestination(enemy.player.gameObject.transform.position);
        }
    }

    public override void ExitState(EnemyStateMachine enemy)
    {
        // Nada especial que limpiar
    }
}
