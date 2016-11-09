using UnityEngine;
using System.Collections;
using System.IO;

public class MapController : MonoBehaviour
{
    private string MapPath;
    private Hashtable MapObjects;
    private int stage = 1;
    private Player player;
    int pattern_num = 1;

    private int numberOfWidthBlocks;
    private int numberOfHeightBolcks;

    private float cameraWidth;
    private float cameraHeight;

    private float blockSize;
    private float increasetime;
    private int[] difnum = { 0, 2 };
    private int[] diftime = { 25, 25, 35, 35, 25, 15, 10, 5 };
    private int difcheck = 0;
    private int mapnum;
    private float timer;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player>();
        MapPath = "Maps/";
        Debug.Log(MapPath);
        MapObjects = new Hashtable();
        timer = Time.time;
        increasetime = timer + diftime[0];

        numberOfWidthBlocks = 50;
        numberOfHeightBolcks = 10;

        cameraHeight = 2f * Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Camera.main.aspect;
        blockSize = cameraHeight / 10;

        mapnum = Random.Range(difnum[0], difnum[1] + 1);
        readMapFromFile(mapnum, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            float map_position = player.transform.position.x;
            timer = Time.time;
            if (increasetime < timer && difcheck < 8)
            {
                difcheck++;
                increasetime = timer + diftime[difcheck];
                difnum[0]++; difnum[1]++;
                difnum[0] = Mathf.Min(difnum[0], 7);
                difnum[1] = Mathf.Min(difnum[1], 9);
            }

            if (map_position >= numberOfWidthBlocks * pattern_num - 25)
            {
                pattern_num++;
                mapnum = Random.Range(difnum[0], difnum[1] + 1);
                readMapFromFile(mapnum, pattern_num);
            }
        }
    }

    void WriteFile()
    {
        StreamWriter fileWriter = new StreamWriter(MapPath);

        // write file
        fileWriter.WriteLine("Hello world");
        fileWriter.Flush();
        fileWriter.Close();
    }


    void readMapFromFile(int difficulty, int pattern_num)
    {
        int height = 0;
        string[,] TILE = new string[,]
        {
            {"Training_1", "Training_2", "Training_3", "Training_4", "Training_5", "Training_6" },
            {"Map2A", "Map2A", "Map2A","Map2A","Map2A","Map2A" },
            {"Map3A", "Map3B", "Map3A","Map3B","Map3A","Map3B" },
            {"Map4A", "Map4B", "Map4A","Map4B","Map4A","Map4B" },
            {"Map5A", "Map5B", "Map5A", "Map5B", "Map5A", "Map5B" },
            {"Map6A", "Map6B", "Map6A", "Map6B", "Map6A", "Map6B" },
            {"Map7A", "Map7B", "Map7A", "Map7B", "Map7A", "Map7B" },
            {"Map8A", "Map8B", "Map8A", "Map8B", "Map8A", "Map8B"},
            {"Map9A", "Map9B", "Map9A", "Map9B", "Map9A", "Map9B"},
            {"Map10A", "Map10B", "Map10A", "Map10B", "Map10A", "Map10B"}
        };

        TextAsset map = Resources.Load(MapPath + TILE[difficulty, Random.Range(0, 6)]) as TextAsset;
        StreamReader fileReader = new StreamReader(new MemoryStream(map.bytes));
        height = 0;
        // read file
        while (!fileReader.EndOfStream)
        {
            string line = fileReader.ReadLine();
            string[] temp = line.Split(' ');
            for (int i = 0; i < numberOfWidthBlocks; i++)
            {
                MapObjects.Add(((pattern_num - 1) * numberOfWidthBlocks + i) + "," + height, int.Parse(temp[i]));
            }
            height++;
        }
        //MapTerm (pattern);
        fileReader.Close();
        Map_Create(pattern_num);
    }

    void Map_Create(int map_num)
    {
        string path = "Prefabs/";
        Quaternion rotate = new Quaternion();
        for (int j = 0; j <= numberOfHeightBolcks; j++)
        {
            for (int i = (map_num - 1) * numberOfWidthBlocks; i < map_num * numberOfWidthBlocks; i++)
            {
                Vector3 position = new Vector3(i * blockSize, -j * blockSize + cameraHeight / 2f - blockSize / 2, 0);
                Object currentBlock = null;
                switch ((int)(MapObjects[i + "," + j]))
                {
                    case (int)RObjectType.BLANK:
                        break;

                    case (int)RObjectType.STANDARD:
                        int randBlock = Random.Range(1, 4);
                        if (randBlock != 2)
                            currentBlock = Instantiate(Resources.Load(path + "Wall"), position, rotate);
                        else
                            currentBlock = Instantiate(Resources.Load(path + "Wall2"), position, rotate);
                        break;

                    case (int)RObjectType.POINT:
                        Instantiate(Resources.Load(path + "Score_Item"), position, rotate);
                        break;

                    case (int)RObjectType.FALLING:
                        currentBlock = Instantiate(Resources.Load(path + "Drop_Wall"), position, rotate);
                        break;

                    case (int)RObjectType.SLIP:
                        currentBlock = Instantiate(Resources.Load(path + "Slip_Wall"), position, rotate);
                        break;

                    case (int)RObjectType.ITEM:
                        currentBlock = Instantiate(Resources.Load(path + "Scale_Change"), position, rotate);
                        break;

                    case (int)RObjectType.ARROW:
                        position.y = 0;
                        currentBlock = Instantiate(Resources.Load(path + "ArrowCollider"), position, rotate);
                        break;

                    case (int)RObjectType.TEXT:
                        currentBlock = Instantiate(Resources.Load(path + "TextImagePrefab"), position, rotate);

                        break;

                    case (int)RObjectType.WIND_0:
                    case (int)RObjectType.WIND_UP:
                    case (int)RObjectType.WIND_DOWN:
                    case (int)RObjectType.WIND_NONE_GRAVITY:
                        {
                            position.y = 0;
                            currentBlock = Instantiate(Resources.Load(path + "WindCollider"), position, rotate);
                            WindControl a = ((GameObject)currentBlock).GetComponent<WindControl>();
                            if ((int)MapObjects[i + "," + j] == (int)RObjectType.WIND_UP)
                            {
                                a.x_Strength = 0;
                                a.y_Strength = 60;
                            }
                            else if ((int)MapObjects[i + "," + j] == (int)RObjectType.WIND_DOWN)
                            {
                                a.x_Strength = 0;
                                a.y_Strength = -20;
                            }
                            else if ((int)MapObjects[i + "," + j] == (int)RObjectType.WIND_NONE_GRAVITY)
                            {
                                a.x_Strength = 0;
                                a.y_Strength = -Physics.gravity.y * player.GetComponent<Rigidbody2D>().mass * 1.25f;
                            }
                        }
                        break;

                    case (int)RObjectType.STOP:
                        currentBlock = Instantiate(Resources.Load(path + "WallStopper"), position, rotate);
                        break;

                    case (int)RObjectType.MOVE_UP:
                    case (int)RObjectType.MOVE_RIGHT:
                    case (int)RObjectType.MOVE_DOWN:
                    case (int)RObjectType.MOVE_LEFT:
                        currentBlock = Instantiate(Resources.Load(path + "Wall"), position, rotate);
                        Wall move = ((GameObject)currentBlock).GetComponent<Wall>();
                        move.movable = true;
                        move.speed = 2f;
                        if ((int)MapObjects[i + "," + j] == (int)RObjectType.MOVE_UP)
                        {
                            move.direction = 1;
                            move.transform.Rotate(0, 0, 270);
                        }
                        else if ((int)MapObjects[i + "," + j] == (int)RObjectType.MOVE_RIGHT)
                        {
                            move.direction = 2;
                            move.transform.Rotate(0, 0, 180);
                        }
                        else if ((int)MapObjects[i + "," + j] == (int)RObjectType.MOVE_DOWN)
                        {
                            move.direction = 3;
                            move.transform.Rotate(0, 0, 90);
                        }
                        else if ((int)MapObjects[i + "," + j] == (int)RObjectType.MOVE_LEFT)
                        {
                            move.direction = 0;
                        }
                        break;
                }
                if (currentBlock && (int)MapObjects[i + "," + j] != (int)RObjectType.ARROW)
                    ((GameObject)currentBlock).transform.localScale = new Vector3(0.4f * blockSize, 0.4f * blockSize, 1);
            }
        }
    }
}