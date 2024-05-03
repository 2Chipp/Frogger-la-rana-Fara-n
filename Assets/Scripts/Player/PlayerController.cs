using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    Camera mainCamera;
    GameGrid gameGrid;
    GamePlayUI gamePlayUI;
    DataManager dataManager;
    SoundManager soundManager;
    GameFinisher gameFinisher;

    EventManager eventManager;

    //public GameObject camCurtain;

    Ray ray;
    RaycastHit hit;

    public Vector3 temporalTarget;
    public Vector3 target;
    Vector3 initialPosition;

    float distance;
    float maxDistance = 17f;
    float jumpCooldown;
    bool jumping;

    public Material pointedTarguetRed;
    public Material pointedTarguetGreen;
    public GameObject pointedTarguetPrefab;

    Animator animator;

    public Ease myEase = Ease.InQuart;

    [SerializeField] float jumpForce;
    [SerializeField] float jumpDuration;

    public bool restriction=false;
    public bool trigger=true;
    public int currentCoins=0;

    Vector3 lastSaveZoneCellPosition;

    public bool onMovingPlatform = false;
    [Range(-1, 1)]
    int direction;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        eventManager = EventManager.eventManager;
        dataManager = DataManager.dataManager;
        gamePlayUI = GamePlayUI.gamePlayUI;
        soundManager = SoundManager.soundManager;

        eventManager.onRestartGame += RestartGame;

        animator = GetComponent<Animator>();
        gameGrid = FindObjectOfType<GameGrid>();
        mainCamera = Camera.main;
        target = transform.position;
        pointedTarguetPrefab = Instantiate(pointedTarguetPrefab, Vector3.zero, Quaternion.identity);
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        Jump();
        OutsideTheGridCheck();
    }

    void OutsideTheGridCheck()
    {
        if ((transform.position.x < 0) || (transform.position.x > gameGrid.gridWidth * 10))
        {
            dataManager.Lives--;
            dataManager.Points -= 100;
            TranslatePlayer();
        }
    }

    void Jump()
    {
        jumpCooldown -= Time.deltaTime;
        jumping = jumpCooldown > 0 ? true : false;

        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(transform.position, -Vector3.up, Color.red, 0.5f);
        if(Physics.Raycast(transform.position, -Vector3.up, out hit,  0.5f))
        {
            if (hit.transform.tag == "Ground")
            {
                
                distance = Vector3.Distance(transform.position, pointedTarguetPrefab.transform.position);
                
                if ((currentCoins == GroupData.totalCoins) && (hit.transform.GetComponent<Cell>().cellGroup == gameGrid.gridHeight - 1))
                {
                    dataManager.MyPlayerStatus = GameFinisher.PlayerStatus.Winner;
                    eventManager.EndGame();
                    
                }
                else if (hit.transform.GetComponent<Cell>().groupType == Cell.GroupType.SaveZone)
                {
                    lastSaveZoneCellPosition = hit.transform.position+Vector3.up;
                }
            }
        }
        if (!jumping)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag == "Ground")
                {
                    Cell cell = hit.transform.GetComponent<Cell>();
                    temporalTarget = cell.transform.position;
                    distance = Vector3.Distance(transform.position, temporalTarget);
                    SetPointedTarguet(cell,distance);
                    if (distance < maxDistance)
                    {
                        Debug.DrawLine(transform.position, target, Color.green);
                        if ((Input.GetMouseButtonDown(0)) && (temporalTarget != target) && (!restriction) && (cell.interactable) )
                        {
                            dataManager.Points += 50;
                            soundManager.PlaySFX("JumpSFX");
                            target = temporalTarget;
                            onMovingPlatform = cell.moving;
                            jumpCooldown = jumpDuration ;
                            direction = cell.movementDir;
                            speed = cell.speed;
                            trigger = true;
                            if (!cell.interactable) target -= Vector3.up * 1;
                            if (distance > 17)
                            {
                                maxDistance = 17;
                                jumpForce = 7;
                            }
                            else jumpForce = 5;
                            if (cell.moving)
                            {
                                transform.DOJump(target + Vector3.up * 0.1f + Vector3.right * cell.speed * jumpDuration * cell.movementDir, jumpForce, 1, jumpDuration).SetEase(myEase);
                                transform.DOLookAt(target, 0.3f);
                                animator.SetTrigger("Jump");
                            }
                            else
                            {
                                transform.DOJump(target + Vector3.up * 0.1f, jumpForce, 1, jumpDuration).SetEase(myEase);
                                transform.DOLookAt(target, 0.3f);
                                animator.SetTrigger("Jump");
                            }
                        }
                    }
                    else Debug.DrawLine(transform.position, target, Color.red);
                }
            }
        }
    }

    void Moving()
    {
        if ((onMovingPlatform) && (!jumping))
        {
            transform.position += Vector3.right * direction * speed * Time.deltaTime;
        }
        dataManager.PlayerPosition = transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            if (trigger)
            {
                restriction = true;
                animator.SetTrigger("Dead");
                dataManager.Lives--;
                Invoke("TranslatePlayer", 1.25f);
                dataManager.Points -= 100;
                trigger = false;
            }
            soundManager.PlaySFX("CrashSFX");
            if (dataManager.Lives <= 0) eventManager.EndGame();
        }
        else if (other.tag == "Coin")
        {
            soundManager.PlaySFX("CoinSFX");
            trigger = false;
            dataManager.Points += 100;
            maxDistance = 29;
            currentCoins++;
            other.transform.position += Vector3.up * 100;
        }
    }
    
    void TranslatePlayer()
    {
        target = lastSaveZoneCellPosition;
        transform.position = lastSaveZoneCellPosition;
        onMovingPlatform = false;
        restriction = false;
    }

    void SetPointedTarguet(Cell cell, float distance)
    {
        pointedTarguetPrefab.transform.position = cell.transform.position + Vector3.up * 1;
        if ((distance < maxDistance)&&(!jumping)&&(cell.interactable))
        {
            pointedTarguetPrefab.transform.GetChild(0).GetComponent<MeshRenderer>().material = pointedTarguetGreen;
            pointedTarguetPrefab.transform.GetChild(1).GetComponent<MeshRenderer>().material = pointedTarguetGreen;
        }
        else
        {
            pointedTarguetPrefab.transform.GetChild(0).GetComponent<MeshRenderer>().material = pointedTarguetRed;
            pointedTarguetPrefab.transform.GetChild(1).GetComponent<MeshRenderer>().material = pointedTarguetRed;
        }
    }

    public void RestartGame()
    {
        target = Vector3.zero;
        transform.position = initialPosition;
        onMovingPlatform = false;
        trigger = true;
    }

    private void OnDestroy()
    {
        eventManager.onRestartGame -= RestartGame;
    }

}
