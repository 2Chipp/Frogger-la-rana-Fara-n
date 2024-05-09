using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cell : MonoBehaviour
{
    public enum GroupType { SaveZone, Water, Road }
    public GroupType groupType;

    public int cellNumber;
    public int cellGroup;

    public bool moving;
    public bool interactable;

    public int movementDir=0;

    public float speed;

    GameGrid gameGrid;


    GroupData groupData;



    public Vector3 floatingPoint;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        gameGrid = FindObjectOfType<GameGrid>();
        floatingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Traslation();
        Movement();
    }

    private void OnMouseDown()
    {
        Debug.Log("interactable :" + interactable);
    }

    void Traslation()
    {
        if (transform.position.x >= gameGrid.gridWidth * gameGrid.cellOffset)
        {
            transform.position -= Vector3.right * gameGrid.gridWidth * gameGrid.cellOffset;
            groupData.Respawn(cellNumber);
        }
        else if(transform.position.x <= 0)
        {
            transform.position += Vector3.right * gameGrid.gridWidth * gameGrid.cellOffset;
            groupData.Respawn(cellNumber);
        }
        else if(floatingPoint.x >= gameGrid.gridWidth * gameGrid.cellOffset)
        {
            floatingPoint -= Vector3.right * gameGrid.gridWidth * gameGrid.cellOffset;
            groupData.Respawn(cellNumber);
        }
        else if (floatingPoint.x <= 0)
        {
            floatingPoint += Vector3.right * gameGrid.gridWidth * gameGrid.cellOffset;
            groupData.Respawn(cellNumber);
        }
    }

    void Movement()
    {
        if (groupData != null)
        {
            if (moving)
            {
                transform.position += Vector3.right * movementDir * speed * Time.deltaTime;
            }
            else if (groupData.groupType == GroupData.GroupType.Road)
            {
                floatingPoint += Vector3.right * movementDir * speed * Time.deltaTime;
            }
        }  
    }

    public void GetGroupData(GroupData groupData)// es llamado en GameGrid después de crear esta celda
    {
        this.groupData = groupData;
        if (groupData.groupType == GroupData.GroupType.Road) moving = false;
    }

    
}
