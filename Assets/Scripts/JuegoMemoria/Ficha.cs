using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ficha : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MostrarFrente()
    {
        m_Animator.Play("FichaReversoAlFrente");
    }
}
