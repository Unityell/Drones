using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematic : MonoBehaviour
{
    [Header("---------------------------")]
    [SerializeField] Entertainer Entertainer;
    [SerializeField] Vector2 EntertainerNewPosition;
    Vector2 EntertainerStartPos;
    [Header("---------------------------")]
    [SerializeField] BubbleMessage TalkBubble;
    [Header("---------------------------")]
    [SerializeField] DroneBrain[] Drones;
    [Header("---------------------------")]
    [SerializeField] GameObject LayerWithCard;
    [SerializeField] GameObject LayerWithLines;
    [Header("---------------------------")]
    [SerializeField] Card Card;
    [Header("---------------------------")]
    [SerializeField] CheckButton Button;

    void Start()
    {
        Entertainer.MySignal += Directing;
        EntertainerStartPos = Entertainer.transform.position;
        Entertainer.StartMove(EntertainerNewPosition);
    }

    void Directing(string Message)
    {
        switch (GameProgress.GP)
        {
            case 0 : 
            if(Message == "EndMove")
            {
                TalkBubble.MySignal += Directing;
                TalkBubble.gameObject.SetActive(true);
                GameProgress.GP = 1;
            }
            break;
            case 1 :
            if(Message == "Send")
            {
                TalkBubble.gameObject.SetActive(false);
                TalkBubble.MySignal -= Directing;
                Entertainer.StartMove(EntertainerStartPos);
                GameProgress.GP = 2;
            }
            break;
            case 2 :
            if(Message == "EndMove")
            {
                Entertainer.MySignal -= Directing;
                foreach (var item in Drones)
                {
                    item.GetComponent<DroneBrain>().Initialization();
                    item.MySignal += Directing;
                }
                GameProgress.GP = 3;
            }
            break;
            case 3 :
            if(Message == "Touch")
            {
                var Drones = GameObject.FindGameObjectsWithTag("Drone");
                foreach (var item in Drones)
                {
                    Destroy(item);
                }
                LayerWithLines.SetActive(false);
                LayerWithCard.SetActive(true);
                Card.MySignal += Directing;
                Card.RotateAndScale();
                Button.MySignal += Directing;
                GameProgress.GP = 4;
            }
            break;
            case 4 :
            if(Message == "CardReady")
            {
                GameProgress.GP = 5;
                Button.gameObject.SetActive(true);
            }
            break;
            case 5 :
            if(Message == "Press")
            {
                GameProgress.GP = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            break;
            default: break;
        }
    }
}
