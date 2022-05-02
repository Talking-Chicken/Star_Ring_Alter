using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player control class. Stores decks of card that are in hand, in discard deck, and in ready to deal deck
//this class also in charge of player animation and player death
//this class creates four other state classes (idle, move, play, setUp)
public class PlayerControlArcade : MonoBehaviour
{
    private int handSize;
    [SerializeField, Header("prefab")] private GameObject card;
    [SerializeField] private GameObject canvas, bullet;
    public List<GameObject> hand = new List<GameObject>();
    [SerializeField]private Card currentCard;
    [SerializeField, Header("back ground")] private List<GameObject> backgrounds = new List<GameObject>();
    [SerializeField] private GameObject indicator;

    //discard deck will be shuffled and put back to player deck
    private Stack<GameObject> discardDeck = new Stack<GameObject>();
    [Header("card deck")]public List<GameObject> playerDeck; //all cards that player have

    //player movements
    [SerializeField, Header("movement")]private Collider2D bound;
    private LocationManager locationManager;
    [SerializeField] private float speed;
    private int destinationIndex, currentIndex;
    private Animator myAnim;

    private AudioSource audioSource;

    //cameara shake
    [SerializeField, Header("Camera")] Camera shakingCamera; 

    //getters & setters
    public int HandSize {get {return handSize;} private set {handSize = value;}}
    public Card CurrentCard {get {return currentCard;} private set {currentCard = value;}}
    public int DestinationIndex {get {return destinationIndex;} private set {destinationIndex = value;}}
    public float Speed {get {return speed;} private set {speed = value;}}
    public List<GameObject> Hand {get {return hand;} set {hand = value;}}
    public Stack<GameObject> DiscardDeck {get {return discardDeck;} set {discardDeck = value;}}
    public GameObject Indicator {get {return indicator;} private set {indicator = value;}}
    public GameObject Bullet {get {return bullet;}}
    public PlayerStateBaseArcade CurrentState {get {return currentState;}}

    // State
    // private BoardState state;
    private PlayerStateBaseArcade currentState;
    public PlayerStateIdle stateIdle = new PlayerStateIdle();
    public PlayerStateSetUp stateSetUp = new PlayerStateSetUp();
    public PlayerStatePlay statePlay = new PlayerStatePlay();
    public PlayerStateMove stateMove = new PlayerStateMove();

    //transition state functions
    public void ChangeToMoveState() {ChangeState(stateMove);}

    public void ChangeState(PlayerStateBaseArcade newState)
    {
        if (newState != currentState) {
            if (currentState != null)
            {
                currentState.LeaveState(this);
            }

            currentState = newState;

            if (currentState != null)
            {
                currentState.EnterState(this);
            }
        }
    }
    void Start()
    {
        locationManager = FindObjectOfType<LocationManager>();
        DestinationIndex = locationManager.Locations.Count/2;
        currentIndex = DestinationIndex;
        currentState = stateIdle;
        HandSize = 4;
        for (int i = 0; i < Mathf.Min(playerDeck.Count, HandSize); i++) {
            Hand.Add(null);
            playerDeck[i].GetComponent<Card>().IndexInHand = i;
            playerDeck[i].SetActive(false);
        }
        ChangeState(stateSetUp);
        myAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        currentState.Update(this);
    }

    void FixedUpdate() {
        moveTowardDestination();
    }

    //draw a card to fill the empty position of player's hand
    public void draw(int position) {
        if (position >= HandSize) {
            Debug.LogWarning("trying to draw card to position (" + position + ") bigger than hand size");
        } else {
            if (playerDeck.Count <= 0) {
                for (int i = 0; i < DiscardDeck.Count; i++) {
                    playerDeck.Add(DiscardDeck.Pop());
                }

                Debug.Log(playerDeck.Count + " is player deck");

                //shuffule the deck
                for (int i = 0; i < playerDeck.Count; i++) {
                    GameObject temp = playerDeck[i];
                    int randomIndex = (int)Random.Range(0, playerDeck.Count - 0.1f);
                    playerDeck[i] = playerDeck[randomIndex];
                    playerDeck[randomIndex] = temp;
                }
            }
            Hand[position] = playerDeck[playerDeck.Count-1];
            playerDeck.RemoveAt(playerDeck.Count-1);
            Hand[position].SetActive(true);
            Hand[position].transform.position = backgrounds[position].transform.position;

            Hand[position].GetComponent<Card>().IndexInHand = position;
        }
    }

    //discard card from hand, set it inactive
    public void discard(int position) {
        if (position >= HandSize) {
            Debug.LogWarning("trying to discard card from position (" + position + ") bigger than hand size");
        } else {
            DiscardDeck.Push(Hand[position]);
            Hand[position] = null;
            DiscardDeck.Peek().SetActive(false);
        }
    }

    //set a target destination to player, transition to move state
    public void setDestination(bool isMovingRight, int distance) {
        if (isMovingRight) {
            DestinationIndex = Mathf.Min(currentIndex + distance, locationManager.Locations.Count - 1);
            currentIndex = DestinationIndex;
        }
        else {
            DestinationIndex = Mathf.Max(currentIndex - distance, 0);
            currentIndex = DestinationIndex;
        }
    }

    //using lerp to move to destination, if haven't reach destination
    public void moveTowardDestination() {
        Vector2 targetDestination = new Vector2(locationManager.Locations[DestinationIndex].transform.position.x, transform.position.y);
        if (Vector2.Distance(transform.position, targetDestination) > 0.1f) {
            myAnim.SetBool("isMoving", true);
            transform.position = Vector2.MoveTowards(transform.position, targetDestination, Time.deltaTime * Speed);
        } else {
            myAnim.SetBool("isMoving", false);
        }
    }

    //set current card to the card that player clicked on
    public void selectCurrentCard(Card selectingCard) {
        CurrentCard = selectingCard;
    }

    //after cool down, change to play state
    public IEnumerator waitForCardCD() {
        CurrentCard = null;
        yield return new WaitForSeconds(0.5f);
        ChangeState(statePlay);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<EnemyBullet>() != null) {
            shakingCamera.GetComponent<CameraShake>().shake();
            audioSource.Play();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.GetComponent<Enemy>() != null) {
            shakingCamera.GetComponent<CameraShake>().shake();
            audioSource.Play();
            Destroy(gameObject);
        }
    }
}
