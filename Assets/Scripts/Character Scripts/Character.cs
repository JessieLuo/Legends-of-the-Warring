using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public float movingSteps;
    public float Speed = 5f;
    public Transform character;
    private Rigidbody2D rb;
    public Animator anim;
    public AudioSource collectAudio;

    public int Violet;    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();                
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }

    // Player movement. The movement is just use for programmer testing.
    void move()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        float horizontalFaceDirection = Input.GetAxisRaw("Horizontal");
        if (horizontalFaceDirection != 0)
        {
            transform.localScale = new Vector3(horizontalFaceDirection, 1, 1);
        }
        Vector2 position = transform.position;
        position.x = position.x + 3.0f * horizontalMove * Time.deltaTime;
        position.y = position.y + 3.0f * verticalMove * Time.deltaTime;
        transform.position = position;
        //anim.SetFloat("moving", Mathf.Abs(horizontalFaceDirection));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Item collection
        if (collision.tag == "Collection")
        {
            collectAudio.Play();
            Destroy(collision.gameObject);
            Violet += 1;
        }                
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        //attack enemy
        if (collision.gameObject.tag == "Enemy")
        {
            //enemy.setHealthValue(characterSkill.baseAttack());
            GameData.Instance.param = 10;
            //SceneManager.LoadScene("BattleScene");
        }
    }

}
