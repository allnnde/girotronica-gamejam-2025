using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SuperArmPowerUp : MonoBehaviour, IPowerUp
{
    [field: SerializeField] public LayerMask HittableMask { get; set; }
    [field: SerializeField] public float Distances { get; set; } = 3f;
    [field: SerializeField] public float HitForce { get; set; } = 10f;
    [field: SerializeField] public Vector3 HItVolumen { get; set; } = Vector3.one;
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    public void Action()
    {
        var coliders = Physics.BoxCastAll(transform.position, HItVolumen, transform.forward, Quaternion.identity, Distances, HittableMask, QueryTriggerInteraction.Collide);
        Debug.Log($"numero de colider {coliders.Length}");
        foreach (var item in coliders)
        {
            Debug.Log($"colider  a volaaar {item.collider.name}");
            var a = Random.insideUnitSphere;
            item.rigidbody.AddForce(HitForce * (transform.forward.normalized + a), ForceMode.Impulse);
        }
        _anim.SetTrigger("IsAttaking");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Quaternion rotation = Quaternion.identity;
        Vector3 direction = transform.forward;

        // Posición inicial de la caja
        Vector3 start = transform.position;

        // Posición final donde termina el BoxCast
        Vector3 end = start + direction * Distances;


        // Dibujar el volumen final
        Gizmos.matrix = Matrix4x4.TRS(end, rotation, HItVolumen * 2);
        Gizmos.DrawWireCube(Vector3.zero, HItVolumen);

    }
}
