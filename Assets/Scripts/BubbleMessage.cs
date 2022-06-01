using UnityEngine;
using TMPro;

public class BubbleMessage : Signal
{
    public event EmitSignal MySignal;
    [SerializeField] string Message;
    [SerializeField] float TimePerChar;
    [SerializeField] TextMeshPro Text;
    int Number;
    char[] Chars;

    void OnEnable()
    {
        Chars = Message.ToCharArray();
        WriteMessage();
    }

    public void WriteMessage()
    {
        Text.text = Text.text + Chars[Number];
        Number++;
        if(Number < Chars.Length)
        {
            Invoke(nameof(WriteMessage), TimePerChar);
        }
        else
        {
            Invoke(nameof(SendMessage), 2f);
        }
    }

    void SendMessage()
    {
        MySignal?.Invoke("Send");
    }
}
