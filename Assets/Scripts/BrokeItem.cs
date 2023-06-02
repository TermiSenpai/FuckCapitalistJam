using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokeItem : MonoBehaviour, IBreakable
{
    public BreakableConfig config;
    Rigidbody rb;
    AudioSource source;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponentInParent<AudioSource>();
    }


    public void breakItem(Vector3 dir)
    {
        rb.isKinematic = false;

        // Normalizamos la dirección para que siempre tenga una magnitud de 1
        dir.Normalize();
        // Aplicamos una fuerza al RigidBody en la dirección especificada
        rb.AddForce(dir * config.knockbackForce, ForceMode.Impulse);

        onBreakItem();
    }

    public void onBreakItem()
    {
        // aplicar sonido
        source.PlayOneShot(config.onBreakSound);
        // aplicar knockback en cadena
        KnockbackOtherObjects(rb);
    }

    private void KnockbackOtherObjects(Rigidbody rb)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, config.knockbackOtherRadius); // Ajusta el radio según tus necesidades

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("breakable"))
            {
                Vector3 knockbackDirection = collider.transform.position - transform.position;
                knockbackDirection.Normalize();
                Rigidbody otherRb = collider.gameObject.GetComponent<Rigidbody>();
                otherRb.isKinematic = false;
                otherRb.AddForce(knockbackDirection * config.knockbackForce, ForceMode.Impulse);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, config.knockbackOtherRadius);
    }
}
