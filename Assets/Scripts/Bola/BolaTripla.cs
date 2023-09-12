using UnityEngine;

public class BolaTripla : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rbBola1,rbBola2,rbBolaAtual,rbBolaPrefab;
    [SerializeField]
    private bool trava = false;
    private bool liberar = false;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !rbBolaAtual.isKinematic && !trava)
        {
            liberar = true;
            startPos = transform.position;
            rbBola1 = Instantiate(rbBolaPrefab, startPos + new Vector3(0f, 0.8f, 0f), Quaternion.identity);
            rbBola2 = Instantiate(rbBolaPrefab, startPos + new Vector3(0f, 0.4f, 0f), Quaternion.identity);

            trava = true;
        }
    }

    private void FixedUpdate()
    {
        if (liberar)
        {
            rbBola1.velocity = rbBolaAtual.velocity;
            rbBola2.velocity = rbBolaAtual.velocity;

            rbBola1.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
            rbBola2.AddForce(Vector2.up * 4, ForceMode2D.Impulse);

            liberar = false;
        }
    }
}
