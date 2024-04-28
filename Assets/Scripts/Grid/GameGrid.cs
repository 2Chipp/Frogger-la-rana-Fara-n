using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public GameObject cellPrefab;
    public float cellOffset;
    GameObject instancePoint;

    public int gridHeight;
    public int gridWidth;

    public GameObject[] decoratonObjects;
    public GameObject[] groundPrefab;
    public Material[] roadGroundMaterials;
    public GameObject roadGroundPrefab;
    public GameObject waterGroundPrefab;
    public GameObject saveZoneGroundPrefab;
    public GameObject saveZoneHoleGroundPrefab;


    public GameObject coinPrefab;

    public Cell[] temporalCellArray;

    int iterator=0;


    protected GroupData[] groupDataArray;

    Cell[] cellGroup_0;
    Cell[] cellGroup_1;
    Cell[] cellGroup_2;
    Cell[] cellGroup_3;
    Cell[] cellGroup_4;
    Cell[] cellGroup_5;
    Cell[] cellGroup_6;
    Cell[] cellGroup_7;
    Cell[] cellGroup_8;
    Cell[] cellGroup_9;
    Cell[] cellGroup_10;
    Cell[] cellGroup_11;
    Cell[] cellGroup_12;
    Cell[] cellGroup_13;
    Cell[] cellGroup_14;
    Cell[] cellGroup_15;
    Cell[] cellGroup_16;
    Cell[] cellGroup_17;
    Cell[] cellGroup_18;
    Cell[] cellGroup_19;

    void InitializeVariables()
    {
        //Debug.Log("InitializeVariables");
        groupDataArray = new GroupData[gridHeight];

        cellGroup_0 = new Cell[gridWidth];
        cellGroup_1 = new Cell[gridWidth];
        cellGroup_2 = new Cell[gridWidth];
        cellGroup_3 = new Cell[gridWidth];
        cellGroup_4 = new Cell[gridWidth];
        cellGroup_5 = new Cell[gridWidth];
        cellGroup_6 = new Cell[gridWidth];
        cellGroup_7 = new Cell[gridWidth];
        cellGroup_8 = new Cell[gridWidth];
        cellGroup_9 = new Cell[gridWidth];
        cellGroup_10 = new Cell[gridWidth];
        cellGroup_11 = new Cell[gridWidth];
        cellGroup_12 = new Cell[gridWidth];
        cellGroup_13 = new Cell[gridWidth];
        cellGroup_14 = new Cell[gridWidth];
        cellGroup_15 = new Cell[gridWidth];
        cellGroup_16 = new Cell[gridWidth];
        cellGroup_17 = new Cell[gridWidth];
        cellGroup_18 = new Cell[gridWidth];
        cellGroup_19 = new Cell[gridWidth];

        instancePoint.transform.position = new Vector3(5, 0.001f, 5);
        cellOffset = 10;
    }

    // Start is called before the first frame update
    void Start()
    {
        instancePoint = new GameObject();
        instancePoint.transform.position = new Vector3(5f, 0.1f, 5f);
        InitializeVariables();
        GridCreation();
        SetGroupData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GridCreation()
    {
        for (int i = 0; i < gridHeight; i++)
        {
            //Debug.Log("Grid creation into for, init");
            int cellGroupNumberSelector = i;
            
            for (int a = 0; a < gridWidth; a++)
            {
                //Debug.Log("Grid creation into 2for, init");
                var cell = Instantiate(cellPrefab, instancePoint.transform.position, Quaternion.identity);
                Cell thisCell = cell.GetComponent<Cell>();
                thisCell.cellNumber = a;
                thisCell.cellGroup = cellGroupNumberSelector;
                instancePoint.transform.position += Vector3.right * cellOffset;
                SetCellGroup(thisCell);

            }
            instancePoint.transform.position = new Vector3(5, 0.1f, instancePoint.transform.position.z + cellOffset);
        }
    }

    void SetCellGroup(Cell cell)
    {
        switch (cell.cellGroup)
        {            
            case 0: cellGroup_0[iterator] = cell; break;
            case 1: cellGroup_1[iterator] = cell; break;
            case 2: cellGroup_2[iterator] = cell; break;
            case 3: cellGroup_3[iterator] = cell; break;
            case 4: cellGroup_4[iterator] = cell; break;
            case 5: cellGroup_5[iterator] = cell; break;
            case 6: cellGroup_6[iterator] = cell; break;
            case 7: cellGroup_7[iterator] = cell; break;
            case 8: cellGroup_8[iterator] = cell; break;
            case 9: cellGroup_9[iterator] = cell; break;
            case 10: cellGroup_10[iterator] = cell; break;
            case 11: cellGroup_11[iterator] = cell; break;
            case 12: cellGroup_12[iterator] = cell; break;
            case 13: cellGroup_13[iterator] = cell; break;
            case 14: cellGroup_14[iterator] = cell; break;
            case 15: cellGroup_15[iterator] = cell; break;
            case 16: cellGroup_16[iterator] = cell; break;
            case 17: cellGroup_17[iterator] = cell; break;
            case 18: cellGroup_18[iterator] = cell; break;
            case 19: cellGroup_19[iterator] = cell; break;
            default: Debug.LogError("Default"); cellGroup_1[iterator] = cell; break;
        }
        iterator++;
        //Debug.Log("iterator :"+iterator);
        iterator = iterator >= gridWidth ? 0 : iterator;
    }

    void SetGroupData()
    {
        for (int i = 0; i < gridHeight; i++)
        {
            //Debug.Log("SetGroupData for " + i);
            switch (i)
            {
                case 0: temporalCellArray = cellGroup_0; break;
                case 1: temporalCellArray = cellGroup_1; break;
                case 2: temporalCellArray = cellGroup_2; break;
                case 3: temporalCellArray = cellGroup_3; break;
                case 4: temporalCellArray = cellGroup_4; break;
                case 5: temporalCellArray = cellGroup_5; break;
                case 6: temporalCellArray = cellGroup_6; break;
                case 7: temporalCellArray = cellGroup_7; break;
                case 8: temporalCellArray = cellGroup_8; break;
                case 9: temporalCellArray = cellGroup_9; break;
                case 10: temporalCellArray = cellGroup_10; break;
                case 11: temporalCellArray = cellGroup_11; break;
                case 12: temporalCellArray = cellGroup_12; break;
                case 13: temporalCellArray = cellGroup_13; break;
                case 14: temporalCellArray = cellGroup_14; break;
                case 15: temporalCellArray = cellGroup_15; break;
                case 16: temporalCellArray = cellGroup_16; break;
                case 17: temporalCellArray = cellGroup_17; break;
                case 18: temporalCellArray = cellGroup_18; break;
                case 19: temporalCellArray = cellGroup_19; break;
                default: temporalCellArray = cellGroup_1; break;
            }
            GameObject groupData = new GameObject();
            groupData.AddComponent<GroupData>();
            groupData.GetComponent<GroupData>().ImportCellGroup(temporalCellArray);
            groupDataArray[i] = groupData.GetComponent<GroupData>();
            groupData.name = "GroupData_" + i.ToString();
            //Debug.Log(groupDataArray[i].cellGroup);            
            //Debug.Log("SetGroupData end " + groupDataArray[i]);
        }
    }    


    void SetGround()
    {

    }
}



