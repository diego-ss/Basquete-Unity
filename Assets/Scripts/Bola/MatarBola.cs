using UnityEngine;

public class MatarBola : MonoBehaviour
{
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
        this.bolaRender = GetComponent<Renderer>();
        corOriginal = bolaRender.material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        if (noChao)
            EfeitoMorte();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
            noChao = true;
    }

    void EfeitoMorte()
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
