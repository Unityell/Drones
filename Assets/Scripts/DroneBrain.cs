using UnityEngine;

public class DroneBrain : Signal
{
    public event EmitSignal MySignal;
    [SerializeField] float Acceleration;
    [SerializeField] float MaxMoveSpeed;
    [SerializeField] GameObject[] Screws;
    Vector2 Direction;
    Rigidbody2D MyPhysic;
    bool Init;
    public void Initialization()
    {
        RandomVector();
        MyPhysic = GetComponent<Rigidbody2D>();
        foreach (var item in Screws)
        {
            item.GetComponent<HingeJoint2D>().enabled = true;
            item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        Init = true;
    }

    void OnCollisionEnter2D(Collision2D Other)
    {
        GetVector();
    }

    void OnMouseDown()
    {
        if(Init)
        {
            MySignal?.Invoke("Touch");
        }
    }
    void RandomVector()
    {
        Invoke(nameof(RandomVector), 3f);
        GetVector();
    }

    void GetVector()
    {
        switch (Random.Range(0,8))
        {
            case 0: Direction = Vector2.down;                     break;
            case 1: Direction = Vector2.down + Vector2.right;     break;
            case 2: Direction = Vector3.right;                    break;
            case 3: Direction = Vector3.up + Vector3.right;       break;
            case 4: Direction = Vector3.up;                       break;
            case 5: Direction = Vector3.up + Vector3.left;        break;
            case 6: Direction = Vector3.left;                     break;
            case 7: Direction = Vector3.down + Vector3.left;      break;
            default:                                              break;
        }
    }
    void FixedUpdate()
    {
        if(Init)
        {
            if(MyPhysic.velocity.magnitude < MaxMoveSpeed)
            {
                MyPhysic.AddForce(Direction * Acceleration); 
            }
        }
    }
}
