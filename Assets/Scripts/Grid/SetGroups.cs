using UnityEngine;


public class SetGroups : MonoBehaviour
{
    public enum GroupType { SaveZone, Water, Road }
    public GroupType[] groupType;
    //[SerializeField] static int numberOfMovingmovingGroups;
    [SerializeField] bool[] movingGroups;
    [Range(-1, 1)]
    [SerializeField] int[] moveDir;
    [SerializeField] float[] movingGroupspeed;
    GroupData[] groupDataArray;
    float timeOffset = 0.1f;
    bool trigger = true;
    GameGrid gameGrid;
    // Start is called before the first frame update
    void Start()
    {
        gameGrid = FindObjectOfType<GameGrid>();
        if ((movingGroups.Length < gameGrid.gridHeight) || (moveDir.Length < gameGrid.gridHeight) || (movingGroupspeed.Length < gameGrid.gridHeight) || (groupType.Length < gameGrid.gridHeight))
        {
            Debug.LogError("SetGroup.movingGroups.length, SetGroup.moveDir.length, SetGroup.movingGroupspeed.length, SetGroup.groupType.Length y GameGrid.gridHeight must be equal");
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetGroupDataArray();

    }

    void GetGroupDataArray()
    {
        if (trigger)
        {
            GroupData[] temporalGroupDataArray;
            timeOffset -= Time.deltaTime;
            groupDataArray = new GroupData[gameGrid.gridHeight];
            if (timeOffset < 0)
            {
                trigger = false;
                temporalGroupDataArray = FindObjectsOfType<GroupData>();
                //Debug.Log(temporalGroupDataArray.Length);
                for (int i = 0; i < temporalGroupDataArray.Length; i++)
                {
                    Debug.Log("# llamados/" + temporalGroupDataArray.Length);
                    groupDataArray[i] = temporalGroupDataArray[(gameGrid.gridHeight - i)-1];
                    //Debug.Log(groupDataArray[i].name);
                }
                SetGroupData();
            }
        }
    }

    void SetGroupData()
    {
        for (int i = 0; i < gameGrid.gridHeight; i++)
        {
            groupDataArray[i].moving = movingGroups[i];
            groupDataArray[i].moveDir = moveDir[i];
            groupDataArray[i].groupSpeed = movingGroupspeed[i];
            groupDataArray[i].interactable = true;
            switch (groupType[i])
            {
                case GroupType.Road:
                    groupDataArray[i].groupType = GroupData.GroupType.Road;
                    groupDataArray[i].moving = false;
                    break;
                case GroupType.Water:
                    groupDataArray[i].groupType = GroupData.GroupType.Water;
                    groupDataArray[i].interactable = false;
                    break;
                case GroupType.SaveZone:
                    groupDataArray[i].groupType = GroupData.GroupType.SaveZone;
                    groupDataArray[i].moving = false;
                    break;
            }
            groupDataArray[i].SetCellData();
        }
    }
}
