using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    public enum GroupType { SaveZone, Water, Road }

    SetGroups setGroups;
    GameGrid gameGrid;


    public GameObject roadObstacle_1;
    public GameObject roadObstacle_2;
    public GameObject roadObstacle_3;

    public GameObject waterObstacle_1;
    public GameObject waterObstacle_2;
    public GameObject waterObstacle_3;

    public GameObject saveZoneObstacle;


    GameObject[] roadObstacles;
    GameObject[] waterObstacles;

    public GameObject[] decoration;
    GameObject[] decorationPool;

    int waterGroups;
    int roadGroups;
    int saveZoneGroups;
    // Start is called before the first frame update
    void Start()
    {
        gameGrid = FindObjectOfType<GameGrid>();
        setGroups = FindObjectOfType<SetGroups>();
        for (int i = 0; i < setGroups.groupType.Length; i++)
        {
            switch (setGroups.groupType[i].ToString())
            {
                case "Water": waterGroups++; Debug.Log("Water"); break;
                case "Road": roadGroups++; Debug.Log("Road"); break;
                case "SaveZone": saveZoneGroups++; Debug.Log("SaveZone"); break;
            }

        }
        roadObstacles = new GameObject[roadGroups * gameGrid.gridWidth + 2];
        waterObstacles = new GameObject[waterGroups * gameGrid.gridWidth + 2];
        decorationPool = new GameObject[(saveZoneGroups * gameGrid.gridWidth) + 2];

        for (int i = 0; i < roadObstacles.Length; i++)
        {
            GameObject temporalObject = Instantiate(ObstacleSorter("Road"), Vector3.zero, Quaternion.identity);
            temporalObject.SetActive(false);
            roadObstacles[i] = temporalObject;
        }
        for (int i = 0; i < waterObstacles.Length; i++)
        {
            GameObject temporalObject = Instantiate(ObstacleSorter("Water"), Vector3.zero, Quaternion.identity);
            temporalObject.SetActive(false);
            waterObstacles[i] = temporalObject;
        }
        for (int i = 0; i < decorationPool.Length; i++)
        {
            GameObject temporalObject = Instantiate(decoration[Random.Range(0, decoration.Length)], Vector3.zero, Quaternion.identity);
            temporalObject.SetActive(false);
            decorationPool[i] = temporalObject;
        }
    }

    GameObject ObstacleSorter(string ObstacleType)
    {
        int selector = Random.Range(1, 4);
        switch (ObstacleType)
        {
            case "Road":
                switch (selector)
                {
                    case 1: return roadObstacle_1;
                    case 2: return roadObstacle_2;
                    case 3: return roadObstacle_3;
                    default: return roadObstacle_1;
                }


            case "Water":
                switch (selector)
                {
                    case 1: return waterObstacle_1;
                    case 2: return waterObstacle_2;
                    case 3: return waterObstacle_3;
                    default: return waterObstacle_1;
                }


            case "SaveZone":
                return saveZoneObstacle;

            default:
                Debug.LogError("ObstacleSorterDefault");
                return saveZoneObstacle;

        }
    }


    // Update is called once per frame
    void Update()
    {

    }

    public GameObject Spawner(string GroupType, Vector3 position, Vector3 rotation)
    {
        var temporalObj = saveZoneObstacle;
        if (GroupType == "Road")
        {
            temporalObj = roadObstacles[0];
            roadObstacles[0].transform.position = position;
            roadObstacles[0].SetActive(true);
            roadObstacles[roadObstacles.Length - 1] = roadObstacles[0];
            for (int i = 0; i < roadObstacles.Length - 1; i++)
            {
                roadObstacles[i] = roadObstacles[i + 1];
            }
            return temporalObj;
        }
        else if (GroupType == "Water")
        {
            temporalObj = waterObstacles[0];
            waterObstacles[0].transform.position = position;
            waterObstacles[0].SetActive(true);
            waterObstacles[waterObstacles.Length - 1] = waterObstacles[0];
            for (int i = 0; i < waterObstacles.Length - 1; i++)
            {
                waterObstacles[i] = waterObstacles[i + 1];
            }
            return temporalObj;
        }
        else return saveZoneObstacle;
    }
    public GameObject SpawnerDecorationObject(Vector3 position, Vector3 rotation)
    {
        var temporalObj = saveZoneObstacle;
        temporalObj = decorationPool[0];
        decorationPool[0].transform.position = position;
        decorationPool[0].SetActive(true);
        decorationPool[decorationPool.Length - 1] = decorationPool[0];
        for (int i = 0; i < decorationPool.Length - 1; i++)
        {
            Debug.Log("# llamados/" + decorationPool.Length);
            decorationPool[i] = decorationPool[i + 1];
        }
        return temporalObj;
    }
}
