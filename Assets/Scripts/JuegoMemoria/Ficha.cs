using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    /*private void OnMouseDown()
    {
        Debug.Log("clicked");
        m_Animator.Play("FichaFrenteAlReverso");
        this.transform.localScale = new Vector3(0.5f, 0.1f, 0.5f);
        
    }*/


    public void MostrarFrente()
    {
        Debug.Log("clicked");
        m_Animator.Play("FichaReversoAlFrente");
    }
    public void MostrarReverso()
    {
        Debug.Log("clicked");
        m_Animator.Play("FichaFrenteAlReverso");
    }
}
