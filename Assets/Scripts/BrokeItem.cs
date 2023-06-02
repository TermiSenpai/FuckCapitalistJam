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
        KnockbackOtherObjects();
    }

    private void KnockbackOtherObjects()
    {
        // Crea una esfera en el objeto y al entrar en contacto con otro, genera una fuerza en cadena
        Collider[] colliders = Physics.OverlapSphere(transform.position, config.knockbackOtherRadius); 

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

    // Hace visible la esfera que genera la fuerza en cadena en el editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, config.knockbackOtherRadius);
    }
}
