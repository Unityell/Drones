using UnityEngine;

public class Entertainer : Signal
{
    public event EmitSignal MySignal;
    [SerializeField] float MoveSpeed;
    Vector2 NewPosition;
    bool Move;
    public void StartMove(Vector2 NewPos)
    {
        if(!Move)
        {
           NewPosition = NewPos;
           Move = true;
           MySignal?.Invoke("StartMove"); 
        }
    }
    void FixedUpdate()
    {
        if(Move)
        {
            transform.position = Vector2.MoveTowards(transform.position, NewPosition, Time.deltaTime * MoveSpeed);
            if((Vector2)transform.position == NewPosition)
            {
                MySignal?.Invoke("EndMove");
                Move = false;
            }
        }
    }
}
