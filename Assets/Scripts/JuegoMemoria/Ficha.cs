using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Ficha : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    protected string m_Name;
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private Image cara;
    private int veces = 0;


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
    public void changeName(string name) { 
        m_Name = name;
        m_text.text = name; 
    }


    public void MostrarFrente()
    {
        Debug.Log("clicked");
        m_Animator.Play("FichaReversoAlFrente");
        Invoke("changeColor", 0.5f);
    }

    private void changeColor()
    {
        if(veces%2==0)
        cara.color = new Color32(255, 255, 225, 100);
        else
        cara.color = new Color32(255, 0, 0, 100);

        veces++;
    }
    public void MostrarReverso()
    {
        Debug.Log("clicked");
        m_Animator.Play("FichaFrenteAlReverso");
        Invoke("changeColor", 0.5f);
    }

}
