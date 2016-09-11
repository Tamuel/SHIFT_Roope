using UnityEngine;
using System.Collections;
using System.IO;

public class MapController : MonoBehaviour {
	private string MapPath;
	private Hashtable MapObjects;
	private int stage = 1;
	private Player player;
	int pattern_num = 0;

	private int numberOfWidthBlocks;
	private int numberOfHeightBolcks;

	private float cameraWidth;
	private float cameraHeight;

	private float blockSize;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player> ();
		MapPath = "Maps/";
		Debug.Log (MapPath);
		MapObjects = new Hashtable ();

		numberOfWidthBlocks = 50;
		numberOfHeightBolcks = 10;

		cameraHeight = 2f * Camera.main.orthographicSize;
		cameraWidth = cameraHeight * Camera.main.aspect;
		blockSize = cameraHeight / 10;

		readMapFromFile ();
	}
	
	// Update is called once per frame
	void Update () {
		float map_position = player.transform.position.x;

		if (map_position >= numberOfWidthBlocks * (pattern_num + 1)) {
			pattern_num++;
			Map_Create (pattern_num + 2);
		}

	}

	void WriteFile()
	{
		StreamWriter fileWriter = new StreamWriter (MapPath);

		// write file
		fileWriter.WriteLine ("Hello world");
		fileWriter.Flush ();
		fileWriter.Close ();
	}


	void readMapFromFile()
	{
		int height = 0, pattern = 0;
		int random_pattern = 0;
		string[] TILE = new string[] {
/*			"map_1A",
			"map_2A",
			"map_3A",
			"map_4A",
			"map_17A",
			"map_6A",
			"map_7A",
			"map_8A",
			"map_9A",
			"map_10A",
			"map_11A",
			"map_12A",
			"map_13A",
			"map_14A",
			"map_15A",
			"map_16A",
			"map_5A",
			"map_1B",
			"map_2B",
			"map_3B",
			"map_4B",
			"map_6B",
			"map_7B",
			"map_8B",
			"map_9B",
			"map_10B",
			"map_11B",
			"map_12B",
			"map_13B",
			"map_14B",
			"map_15B",
			"map_16B",
			"map_17B",
			"map_5B",
			"map_1C", 
			"map_2C",
			"map_3C",
			"map_4C",
			"map_6C",
			"map_7C",
			"map_8C",
			"map_9C",
			"map_10C",
			"map_11C",
			"map_12C",
			"map_13C",
			"map_14C",
			"map_15C",
			"map_16C",
			"map_7C",
			"map_A",
			"map_A1",
			"map_A2",
			"map_A3",
			"map_A4",
			"map_A5",
			"map_B",
			"map_C",
			"map_D"
			*/
			"Training_1",
			"Training_2",
			"Training_3",
			"Training_4",
			"Training_5",
			"Training_6",
			"map2A",
			"map5A",
			"map5B",
			"map6A",
			"map6B",
			"map8A",
			"map8B",
			"map9A",
			"map9B",
			"map10A",
			"map10B"
		};

		for (pattern = 0; pattern < TILE.Length; pattern++) {
			random_pattern = Random.Range (0, 10);
			TextAsset map = Resources.Load (MapPath + TILE[pattern]) as TextAsset;
			StreamReader fileReader = new StreamReader (new MemoryStream(map.bytes)); 
			height = 0;
			// read file
			while (!fileReader.EndOfStream) {
				string line = fileReader.ReadLine ();
				string[] temp = line.Split(' ');
				for (int i = 0; i < numberOfWidthBlocks; i++) {
					MapObjects.Add ((pattern * numberOfWidthBlocks + i) + "," + height, int.Parse (temp[i]));
				}
				height++;
			}
			//MapTerm (pattern);
			fileReader.Close ();
		}
		Map_Create (1);
		Map_Create (2);
	}

	void Map_Create(int map_num)
	{
		string path = "Prefabs/";
		Quaternion rotate = new Quaternion ();
		for (int j = 0; j <= numberOfHeightBolcks; j++) {
			for (int i = (map_num - 1) * numberOfWidthBlocks; i < map_num * numberOfWidthBlocks; i++) {
				Vector3 position = new Vector3 (i * blockSize, -j * blockSize + cameraHeight / 2f - blockSize / 2, 0);
				Object currentBlock = null;
				switch ((int)(MapObjects [((map_num - 1) * numberOfWidthBlocks + i) + "," + j])) {
				case (int)RObjectType.BLANK:
					break;

				case (int)RObjectType.STANDARD:
					int randBlock = Random.Range (1, 4);
					if(randBlock != 2)
						currentBlock = Instantiate (Resources.Load (path + "Wall"), position, rotate);
					else
						currentBlock = Instantiate (Resources.Load (path + "Wall2"), position, rotate);
					break;

				case (int)RObjectType.POINT:
					Instantiate (Resources.Load (path + "Score_Item"), position, rotate);
					break;

				case (int)RObjectType.FALLING:
					currentBlock = Instantiate (Resources.Load (path + "Drop_Wall"), position, rotate);
					break;

				case (int)RObjectType.SLIP:
					currentBlock = Instantiate (Resources.Load (path + "Slip_Wall"), position, rotate);
					break;

				case (int)RObjectType.ITEM:
					currentBlock = Instantiate (Resources.Load (path + "Scale_Change"), position, rotate);
					break;

				case (int)RObjectType.ARROW:
					position.y = 0;
					currentBlock = Instantiate (Resources.Load(path + "ArrowCollider"), position, rotate);
					break;

				case (int)RObjectType.WIND_0:
				case (int)RObjectType.WIND_UP:
				case (int)RObjectType.WIND_DOWN:
				case (int)RObjectType.WIND_NONE_GRAVITY:
					{
						position.y = 0;
						currentBlock = Instantiate (Resources.Load (path + "WindCollider"), position, rotate);
						WindControl a = ((GameObject)currentBlock).GetComponent<WindControl> ();
						if ((int)MapObjects [((map_num-1) * 50 + i) + "," + j] == (int)RObjectType.WIND_UP) {
							a.x_Strength = 0;
							a.y_Strength = 60;
						}
						else if ((int)MapObjects [((map_num-1) * 50 + i) + "," + j] == (int)RObjectType.WIND_DOWN) {
							a.x_Strength = 0;
							a.y_Strength = -20;
						}
						else if ((int)MapObjects [((map_num-1) * 50 + i) + "," + j] == (int)RObjectType.WIND_NONE_GRAVITY) {
							a.x_Strength = 0;
							a.y_Strength = -Physics.gravity.y * player.GetComponent<Rigidbody2D> ().mass * 1.25f;
						}
					}
					break;

				case (int)RObjectType.STOP:
					currentBlock = Instantiate (Resources.Load (path + "WallStopper"), position, rotate);
					break;

				case (int)RObjectType.MOVE_UP:
				case (int)RObjectType.MOVE_RIGHT:
				case (int)RObjectType.MOVE_DOWN:
				case (int)RObjectType.MOVE_LEFT:
					currentBlock = Instantiate (Resources.Load (path + "Wall"), position, rotate);
					Wall move = ((GameObject)currentBlock).GetComponent<Wall> ();
					move.movable = true;
					move.speed = 2f;
					if ((int)MapObjects [((map_num - 1) * numberOfWidthBlocks + i) + "," + j] == (int)RObjectType.MOVE_UP) {
						move.direction = 1;
						move.transform.Rotate (0, 0, 270);
					} else if ((int)MapObjects [((map_num - 1) * numberOfWidthBlocks + i) + "," + j] == (int)RObjectType.MOVE_RIGHT) {
						move.direction = 2;
						move.transform.Rotate (0, 0, 180);
					} else if ((int)MapObjects [((map_num - 1) * numberOfWidthBlocks + i) + "," + j] == (int)RObjectType.MOVE_DOWN) {
						move.direction = 3;
						move.transform.Rotate (0, 0, 90);
					} else if ((int)MapObjects [((map_num - 1) * numberOfWidthBlocks + i) + "," + j] == (int)RObjectType.MOVE_LEFT) {
						move.direction = 0;
					}
					break;
				}
				if(currentBlock != null)
					((GameObject)currentBlock).transform.localScale = new Vector3(0.4f * blockSize, 0.4f * blockSize, 1);
			}
		}
	}


	//	void MapTerm(int pattern) // making safe zone
	//	{
	//		int height = 0;
	//		TextAsset map = Resources.Load<TextAsset> (MapPath + "map_TERM");
	//		StreamReader mapTerm = new StreamReader (new MemoryStream(map.bytes));
	//		while (!mapTerm.EndOfStream) {
	//			string line = mapTerm.ReadLine ();
	//			for (int i = 0; i < 5; i++) {
	//				MapObjects.Add (((pattern + 1) * 30 - 5 + i) + "," + height, int.Parse (line.ToCharArray () [i] + ""));
	//			}
	//			height++;
	//		}
	//	}
}
