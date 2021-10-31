using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDiceToMove : MonoBehaviour
{
    private Animator animate;
    public ShowDiceResult showDiceResult;

    // Node Movement
    public NodeList nodeList;
    public Transform targetNode;
    public int nodesCurrentIndex = 0;
    public float speed;
    public float distance;
    public bool isMoving = false;
    public int diceAmount = 0;
    public int steps;
    public bool completed = false;
    public bool clicked = false;
    public bool changeDirection = false;
    public Vector3 offset;
    public Transform currentNode;


    // Start is called before the first frame update
    void Start()
    {
        animate = this.GetComponent<Animator>();
        nodesCurrentIndex = nodeList.nodeList.IndexOf(currentNode);
        targetNode = nodeList.nodeList[nodesCurrentIndex];
    }

    void Update()
    {
        if (isMoving)
        {         
            animate.Play("Walk");
            MoveCharacter();
        }

        if (nodesCurrentIndex > 27 && nodesCurrentIndex < 44)
        {
            this.GetComponentInParent<SpriteRenderer>().flipX = true;
        }
        else this.GetComponentInParent<SpriteRenderer>().flipX = false;

    }

    public void Click()
    {
        clicked = true;
        diceAmount = Random.Range(1, 7);
        
        if (this.CompareTag("Player"))
        {
            steps = diceAmount + (int)CharacterAttributes.Instance.movement + (int)CharacterAttributes.Instance.temporaryMovement;
        }
        else if (this.CompareTag("Enemy"))
        {        
            steps = diceAmount + (int)this.GetComponent<EnemyData>().movement + (int)CharacterAttributes.Instance.temporaryMovement;
            print("Enemy Movement: " + steps);
        }
        
        showDiceResult.ShowResult();
        showDiceResult.ShowDiceImage(diceAmount);
        BeginMovement();
    }

    public void BeginMovement()
    {
        completed = false;
        StartCoroutine(MovementCoroutine(steps));
    }

    public void MoveCharacter()
    {
        if (isMoving)
        {
            if (nodesCurrentIndex == nodeList.nodeList.Count)
            {
                nodesCurrentIndex = 0;
            }

            targetNode = nodeList.nodeList[nodesCurrentIndex];
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetNode.position + offset, speed * Time.deltaTime);
            distance = Vector3.Distance(targetNode.position + offset, this.transform.position);

            if (distance == 0)
            {
                currentNode = targetNode;
                isMoving = false;
            }
        }       
    }

    public void ReturnToMap()
    {
        nodesCurrentIndex = nodeList.nodeList.IndexOf(currentNode);
        if (this.CompareTag("Player"))
        {
            nodesCurrentIndex++;
        }        
        if (nodesCurrentIndex == nodeList.nodeList.Count)
        {
            nodesCurrentIndex = 0;
        }
        currentNode = nodeList.nodeList[nodesCurrentIndex];
        this.transform.position = nodeList.nodeList[nodesCurrentIndex].position;
        AiController.inBattle = false;
    }

    public void ChangeDirection()
    {
        nodesCurrentIndex = nodeList.nodeList.Count - 1 - nodesCurrentIndex;
        nodeList.nodeList.Reverse();
        changeDirection = false;
    }

    public IEnumerator MovementCoroutine(int steps)
    {
        int i = 0;
        while (!completed)
        {
            i++;
            nodesCurrentIndex++;
            isMoving = true;

            yield return new WaitForSeconds(0.8f);

            if (i == steps)
            {
                completed = true;
                animate.Play("Idle");
                clicked = false;
                AiController.hasPlayerMoved = true;
                CharacterAttributes.Instance.temporaryMovement = 0;
            }
        }
    }
}
