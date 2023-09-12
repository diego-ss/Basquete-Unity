using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Arremessar : MonoBehaviour
{

    public GameObject pontosTrajetoria;
    
    // components
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;

    // objects
    private List<GameObject> caminho;

    // flags
    private bool tiro = false;
    private bool mirando = false;

    // aux
    [SerializeField]
    private float ajusteMiraX, ajusteMiraY;
    private float forca = 2.0f;
    private Vector2 startPos;

    [SerializeField]
    private float vidaBola = 1f;
    [SerializeField]
    private Color corOriginal;
    [SerializeField]
    private Renderer bolaRender;
    [SerializeField]
    private bool noChao = false;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.circleCollider = GetComponent<CircleCollider2D>();
        this.bolaRender = GetComponent<Renderer>();

        corOriginal = bolaRender.material.GetColor("_Color");

        rb.isKinematic = true;
        circleCollider.enabled = false;
        startPos = transform.position;
        caminho = pontosTrajetoria.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);    
    
        for (int i = 0; i < caminho.Count; i++)
            caminho[i].GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (noChao)
            MatarBola();
    }

    private void FixedUpdate()
    {
        Mirando();
    }

    void MostraCaminho() =>
        caminho.ForEach(p => p.GetComponent<Renderer>().enabled = true);

    void EscondeCaminho() =>
        caminho.ForEach(p => p.GetComponent<Renderer>().enabled = false);
    
    Vector2 PegaForca(Vector3 mouse) => 
        (new Vector2((startPos.x - mouse.x + ajusteMiraX), (startPos.y - mouse.y + ajusteMiraY)) * forca);
    
    Vector2 CaminhoPonto(Vector2 posInicial, Vector2 velInicial, float tempo) =>
        posInicial + velInicial * tempo + 0.5f * Physics2D.gravity * tempo * tempo;

    
    void CalculaCaminho()
    {
        Vector2 vel = PegaForca(Input.mousePosition) * Time.fixedDeltaTime / rb.mass;

        caminho.ForEach(item =>
        {
            item.GetComponent<Renderer>().enabled = true;
            float t = caminho.IndexOf(item) / 20f;
            Vector3 point = CaminhoPonto(transform.position, vel, t);
            point.z = 1.0f;
            item.transform.position = point;
        });
    }

    void Mirando()
    {
        if (tiro == true)
            return;

        if (Input.GetMouseButton(0))
        {
            if(!mirando)
            {
                mirando = true;
                startPos = Input.mousePosition;
                CalculaCaminho();
                MostraCaminho();
            } else
            {
                CalculaCaminho();
            }
        } else if (mirando && tiro == false)
        {
            rb.isKinematic = false;
            circleCollider.enabled = true;
            tiro = true;
            mirando = false;
            EscondeCaminho();
            rb.AddForce(PegaForca(Input.mousePosition));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
            noChao = true;
    }

    void MatarBola()
    {
        if (vidaBola > 0)
        {
            vidaBola -= Time.deltaTime;
            bolaRender.material.SetColor("_Color", new Color(corOriginal.r, corOriginal.g, corOriginal.b, vidaBola));
        } 
        else
            Destroy(gameObject);
    }
}
