using UnityEngine;

public class GroupData : MonoBehaviour
{
    protected GameGrid gameGrid;
    public enum GroupType { SaveZone, Water, Road }
    public GroupType groupType;
    ObstaclePool obstaclePool;

    public bool interactable;
    public bool moving = false;
    public int moveDir = 0;
    public float groupSpeed = 0;
    public Cell[] cellGroup = new Cell[6];
    protected GameObject[] obstacles;
    protected GameObject[] decoration;
    Vector3[] cellFirstPositionAray;

    float obstacleOffset;

    GameObject[] groundPrefab;

    public bool restart = false;
    GroupData mySelf_groupData;

    Vector3 playerInitialPos;
    PlayerController player;

    DataManager dataManager;

    protected int lastEmptyPositionCount;
    private void Start()
    {
        myCoin = new GameObject[3];
        player = FindObjectOfType<PlayerController>();
        playerInitialPos = player.transform.position;
        dataManager = DataManager.dataManager;
        mySelf_groupData = GetComponent<GroupData>();
        gameGrid = FindObjectOfType<GameGrid>();
        obstaclePool = FindObjectOfType<ObstaclePool>();
        obstacles = new GameObject[gameGrid.gridWidth + 1];
        decoration = new GameObject[gameGrid.gridWidth + 1];
        cellFirstPositionAray = new Vector3[gameGrid.gridWidth];
        groundPrefab = new GameObject[gameGrid.gridWidth];
        // Get initial position
        for (int i = 0; i < gameGrid.gridWidth; i++)
        {
            cellFirstPositionAray[i] = cellGroup[i].transform.position;
        }
    }

    public void ImportCellGroup(Cell[] _cellGroup)
    {
        //Debug.Log("Import CellGroup OK");
        cellGroup = _cellGroup;
        //Debug.Log(cellGroup.Length);
    }

    public void SetCellData() //<== Es llamado a travez de SetGroup
    {
        //Debug.Log("SetCellData OK");
        for (int i = 0; i < gameGrid.gridWidth; i++)
        {
            cellGroup[i].interactable = interactable;
            cellGroup[i].moving = moving;
            cellGroup[i].movementDir = moveDir;
            cellGroup[i].speed = groupSpeed;
            cellGroup[i].GetGroupData(mySelf_groupData);
            if (mySelf_groupData.groupType == GroupType.Road) cellGroup[i].groupType = Cell.GroupType.Road;
            else if (mySelf_groupData.groupType == GroupType.Water) cellGroup[i].groupType = Cell.GroupType.Water;
            else cellGroup[i].groupType = Cell.GroupType.SaveZone;
            SetGround(i);
            SpawnObstacleSorter(i);
            SetCoins(i);
        }
    }

    void Update()
    {
        MovingObstacles();
        /*if (Input.GetButtonDown("Jump")) restart = true;*/
        if (restart) ResetInitialValues();
    }

    void MovingObstacles()
    {
        for (int i = 0; i < gameGrid.gridWidth; i++)
        {
            if (obstacles[i] != null)
            {
                if (moving)
                {
                    obstacles[i].transform.position = cellGroup[i].transform.position + Vector3.up * obstacleOffset;
                }
                else if (mySelf_groupData.groupType == GroupData.GroupType.Road)
                {
                    obstacles[i].transform.position = cellGroup[i].floatingPoint + Vector3.up * obstacleOffset;
                }
            }
        }
    }



    public void Respawn(int CellNumber) // Esllamado desde cada celda
    {
        if (groupType.ToString() != "Road")
        {
            if (obstacles[CellNumber] != null)
            {
                obstacles[CellNumber].SetActive(false);
                var temporalobj = obstaclePool.Spawner(groupType.ToString(), cellGroup[CellNumber].transform.position, cellGroup[CellNumber].transform.eulerAngles);
                obstacles[CellNumber] = temporalobj;
                obstacles[CellNumber].SetActive(true);
            }
        }
        else
        {
            if (obstacles[CellNumber] != null)
            {
                obstacles[CellNumber].SetActive(false);
                var temporalobj = obstaclePool.Spawner(groupType.ToString(), cellGroup[CellNumber].floatingPoint, cellGroup[CellNumber].transform.rotation.eulerAngles);
                obstacles[CellNumber] = temporalobj;
                obstacles[CellNumber].SetActive(true);
            }
        }
    }

    // Primera instancia de obstaculos
    public void SpawnObstacleSorter(int cellGroupIterator)
    {
        bool random;
        
        random = Random.value < 0.4;
        if (random)
        {
            if (lastEmptyPositionCount > 2)
            {
                //Debug.Log(lastEmptyPositionCount);
            }
            else
            {
                //Debug.Log(cellGroup + " " + cellGroupIterator);
                var temporalobj = obstaclePool.Spawner(groupType.ToString(), cellGroup[cellGroupIterator].transform.position, cellGroup[cellGroupIterator].transform.eulerAngles);
                obstacles[cellGroupIterator] = temporalobj;
                if (groupType == GroupData.GroupType.Water) cellGroup[cellGroupIterator].interactable = true;
                lastEmptyPositionCount++;
            }
        }
        else
        {
            if (groupType != GroupData.GroupType.SaveZone)
            {
                if (lastEmptyPositionCount < 1)
                {
                    random = Random.value < 0.5;
                    if (random)
                    {
                        var temporalobj = obstaclePool.Spawner(groupType.ToString(), cellGroup[cellGroupIterator].transform.position, Vector3.zero);
                        obstacles[cellGroupIterator] = temporalobj;
                        if (groupType == GroupData.GroupType.Water) cellGroup[cellGroupIterator].interactable = true;
                        lastEmptyPositionCount++;
                    }
                }
                else if (lastEmptyPositionCount < 0)
                {
                    var temporalobj = obstaclePool.Spawner(groupType.ToString(), cellGroup[cellGroupIterator].transform.position, Vector3.zero);
                    obstacles[cellGroupIterator] = temporalobj;
                    if (groupType == GroupData.GroupType.Water) cellGroup[cellGroupIterator].interactable = true;
                    lastEmptyPositionCount++;
                }
                else
                {
                    lastEmptyPositionCount--;
                }
            }
        }
        SpawnDecorationObjectSorter(cellGroupIterator);
        //.Log("CellNum :" + cellGroup[cellGroupIterator].cellNumber + " ObstacleArrayPos :" + cellGroupIterator);
    }



    //Reestablecimeinto a valores iniciales
    public void ResetInitialValues()
    {
        restart = false;
        dataManager.ResetTime();
        for (int i = 0; i < gameGrid.gridWidth; i++)
        {
            if(decoration[i]!=null)decoration[i].SetActive(false);
            cellGroup[i].transform.position = cellFirstPositionAray[i];
            cellGroup[i].floatingPoint = cellGroup[i].transform.position;
            if (obstacles[i] != null)
            {
                obstacles[i].SetActive(false);
                obstacles[i] = null;
                lastEmptyPositionCount = 0;
            }
            if(mySelf_groupData.groupType == GroupData.GroupType.Water) cellGroup[i].interactable = false;
            SpawnObstacleSorter(i);
        }
        player.transform.position = playerInitialPos;
        player.onMovingPlatform = false;
        dataManager.Points = 0;
        dataManager.Coins = 0;
        //player.GetComponent<PlayerController>().lives = 5;
        player.trigger = true;
        player.RestartTarget();
        player.StopParticleSystem();
        player.win = true; ;
        for (int i = 0; i < myCoin.Length; i++)
        {
            if (myCoin[i] != null) myCoin[i].transform.position = new Vector3(myCoin[i].transform.position.x, 2, myCoin[i].transform.position.z);
        }        
    }



    void SetGround(int cellNumber)
    {
        if (mySelf_groupData.groupType == GroupData.GroupType.SaveZone)
        {
            groundPrefab[cellNumber] = Instantiate(gameGrid.saveZoneGroundPrefab, cellFirstPositionAray[cellNumber] - Vector3.up * 0.01f, Quaternion.identity);
        }
        else if (mySelf_groupData.groupType == GroupData.GroupType.Road)
        {
            groundPrefab[cellNumber] = Instantiate(gameGrid.roadGroundPrefab, cellFirstPositionAray[cellNumber] - Vector3.up * 0.01f, Quaternion.identity);
            groundPrefab[cellNumber].GetComponent<MeshRenderer>().material = gameGrid.roadGroundMaterials[Random.Range(0, gameGrid.roadGroundMaterials.Length)];
            float sorter = Random.Range(0f, 4f);
            if (sorter < 1) groundPrefab[cellNumber].transform.Rotate(Vector3.up, 90);
            else if (sorter < 2) groundPrefab[cellNumber].transform.Rotate(Vector3.up, 180);
            else if (sorter < 3) groundPrefab[cellNumber].transform.Rotate(Vector3.up, 270);
        }
        else if((mySelf_groupData.groupType == GroupData.GroupType.Water))
        {
            groundPrefab[cellNumber] = Instantiate(gameGrid.waterGroundPrefab, cellFirstPositionAray[cellNumber] - Vector3.up * 0.01f, Quaternion.identity);
            groundPrefab[cellNumber].transform.Rotate(Vector3.up, 90);
        }
    }



    GameObject [] myCoin;
    static int maxCoins = 3;
    public static int totalCoins;
    void SetCoins(int cellNumber)
    {
        bool random = Random.Range(0f, 1f) < 0.1f ? true : false;
        if ((mySelf_groupData.groupType == GroupData.GroupType.SaveZone) || (mySelf_groupData.groupType == GroupData.GroupType.Road))
        {
            if (random)
            {
                if (totalCoins < maxCoins)
                {
                    if (obstacles[cellNumber] == null)
                    {
                        myCoin[totalCoins] = Instantiate(gameGrid.coinPrefab, cellGroup[cellNumber].transform.position + Vector3.up * 2f, Quaternion.identity);
                        totalCoins++;
                    }
                }
            }
        }
    }


    public void SpawnDecorationObjectSorter(int cellGroupIterator)
    {
        bool random;
        if (groupType == GroupData.GroupType.SaveZone)
        {
            random = Random.value < 0.7;
            if (random)
            {
                float xOffset = Random.Range(-4, -3);
                float zOffset = Random.Range(4, 3);
                var temporalobj = obstaclePool.SpawnerDecorationObject( cellGroup[cellGroupIterator].transform.position + Vector3.right * xOffset + Vector3.forward * zOffset, Vector3.zero);
                decoration[cellGroupIterator] = temporalobj;
                temporalobj.SetActive(true);
                lastEmptyPositionCount++;
            }
        }
    }
}


