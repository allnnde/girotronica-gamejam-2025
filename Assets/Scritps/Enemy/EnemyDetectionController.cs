using UnityEngine;


public class EnemyDetectionController : MonoBehaviour
{
    private EnemyStateMachine enemy;

    private void Awake()
    {
        enemy = this.transform.parent.GetComponent<EnemyStateMachine>();
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //        enemy.SetChaseState();
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //        enemy.SetPatrolState();
    //}
}
