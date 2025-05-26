using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    [SerializeField]private Slider SnakeSpeedSlider;
    [SerializeField] private Text SnakeSpeedValue;
    public GameObject TailPrefab;
    public AudioSource EatingFood;
    public AudioSource GameOverSound;


     Vector2 dir = Vector2.right;
    // Start is called before the first frame update
    // Keep Track of Tail
     List<Transform> tail = new List<Transform>();
     // Did the snake eat something?
     bool ate = false;

     public float SnakeSpeed = 0.06f;

    void Start()
    {
        LiveScoreScript.scoreValue = 0;
        SnakeSpeedSlider.onValueChanged.AddListener((SnakeSpeed) =>
        {
           CancelInvoke();
           InvokeRepeating("Move", 0.06f, SnakeSpeed);
           SnakeSpeedValue.text = (100*SnakeSpeed).ToString("0.00");
        }
        );

         // Move the Snake every 300ms
        InvokeRepeating("Move", 0.06f, SnakeSpeed);
    }

    // Update is called once per frame
    void Update()
    {
         // Move in a new Direction?
    if (Input.GetKey(KeyCode.RightArrow))
        dir = Vector2.right;
    else if (Input.GetKey(KeyCode.DownArrow))
        dir = -Vector2.up;    // '-up' means 'down'
    else if (Input.GetKey(KeyCode.LeftArrow))
        dir = -Vector2.right; // '-right' means 'left'
    else if (Input.GetKey(KeyCode.UpArrow))
        dir = Vector2.up;
    }
    void Move() {
    // Save current position (gap will be here)
    Vector2 v = transform.position;

    // Move head into new direction (now there is a gap)
    this.transform.Translate(dir);

    // Ate something? Then insert new Element into gap
    if (ate) {
        // Load Prefab into the world
        // soundmanager.PlaySound("EatingFood");
        // SoundManager.PlaySound("ring");
        GameObject g =(GameObject)Instantiate(TailPrefab,v,Quaternion.identity);

        // Keep track of it in our tail list
        tail.Insert(0, g.transform);

        // Reset the flag
        ate = false;
    }
    // Do we have a Tail?
    else if (tail.Count > 0) {
        // Move last Tail Element to where the Head was
        tail.Last().position = v;

        // Add to front of list, remove from the back
        tail.Insert(0, tail.Last());
        tail.RemoveAt(tail.Count - 1);
    }
}
void OnTriggerEnter2D(Collider2D coll) {
    // Food?
    if (coll.name.StartsWith("FoodPrefab")) {
        // Get longer in next Move call
        ate = true;
        EatingFood.Play();
        LiveScoreScript.scoreValue +=1;
        // Remove the Food
        Destroy(coll.gameObject);
    }
    else if(coll.name.StartsWith("Border"))
    {
           GameOverSound.Play();
        //    SceneManager.LoadScene("SnakeGameV1");
            GameOver.Setup();
    // Collided with Tail or Border
    }
 }
 public void AdjustSpeed(float newSpeed)
 {
     SnakeSpeed = newSpeed;
 }
}
