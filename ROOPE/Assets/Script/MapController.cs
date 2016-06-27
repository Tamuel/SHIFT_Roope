using UnityEngine;
using System.Collections;
using System.IO;

public class MapController : MonoBehaviour {
	private string MapPath;
	private Hashtable MapObjects;
	private int stage = 1;

	// Use this for initialization
	void Start () {
		MapPath = "Maps/";
		Debug.Log (MapPath);
		MapObjects = new Hashtable ();
		readMapFromFile ();
	}
	
	// Update is called once per frame
	void Update () {
	
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
			"map_1A",
			"map_2A",
			"map_3A",
			"map_4A",
			"map_17A",
			"map_6A",
			"map_7A",
			"map_8A",
			"map_9A",
			"map_10A"
//			"map_11A.dat",
//			"map_12A.dat",
//			"map_13A.dat",
//			"map_14A.dat",
//			"map_15A.dat",
//			"map_16A.dat",
//			"map_5A.dat",
//			"map_1B.dat",
//			"map_2B.dat",
//			"map_3B.dat",
//			"map_4B.dat",
//			"map_6B.dat",
//			"map_7B.dat",
//			"map_8B.dat",
//			"map_9B.dat",
//			"map_10B.dat",
//			"map_11B.dat",
//			"map_12B.dat",
//			"map_13B.dat",
//			"map_14B.dat",
//			"map_15B.dat",
//			"map_16B.dat",
//			"map_17B.dat",
//			"map_5B.dat",
//			"map_1C.dat", 
//			"map_2C.dat",
//			"map_3C.dat",
//			"map_4C.dat",
//			"map_6C.dat",
//			"map_7C.dat",
//			"map_8C.dat",
//			"map_9C.dat",
//			"map_10C.dat",
//			"map_11C.dat",
//			"map_12C.dat",
//			"map_13C.dat",
//			"map_14C.dat",
//			"map_15C.dat",
//			"map_16C.dat",
//			"map_7C.dat"
		};

		for (pattern = 0; pattern < TILE.Length; pattern++) {
//			if (pattern % 15 == 0 && pattern != 0)
//				stage++;
//			if (stage == 1)
//				random_pattern = Random.Range (0, 16);
//			else if (stage == 2)
//				random_pattern = Random.Range (17, 33);
//			else
//				random_pattern = Random.Range (33, 49);
			TextAsset map = Resources.Load (MapPath + TILE[pattern]) as TextAsset;
			StreamReader fileReader = new StreamReader (new MemoryStream(map.bytes)); 
			height = 0;
			// read file
			while (!fileReader.EndOfStream) {
				string line = fileReader.ReadLine ();
				for (int i = 0; i < 25; i++) {
					MapObjects.Add ((pattern * 30 + i) + "," + height, int.Parse (line.ToCharArray () [i] + ""));
				}
				height++;
			}
			MapTerm (pattern);
			fileReader.Close ();
		}

		string path = "Prefabs/";
		Quaternion rotate = new Quaternion ();
		for (int j = 0; j < 10; j++) {
			for (int i = 0; i < 30 * TILE.Length; i++) {
				Vector3 position = new Vector3 (i, j - 4.5f, 0);
				switch ((int)MapObjects [i + "," + j]) {
				case (int)RObjectType.BLANK:
					break;

				case (int)RObjectType.STANDARD:
					Instantiate (Resources.Load(path + "Wall"), position, rotate);
					break;

				case (int)RObjectType.POINT:
					Instantiate (Resources.Load(path + "Score Item"), position, rotate);
					break;

				case (int)RObjectType.FALLING:
					Instantiate (Resources.Load(path + "Drop_Wall"), position, rotate);
					break;

				case (int)RObjectType.SLIP:
					Instantiate (Resources.Load(path + "Slip_Wall"), position, rotate);
					break;

				case (int)RObjectType.ITEM:
					Instantiate (Resources.Load(path + "Scale Change"), position, rotate);
					break;

				case (int)RObjectType.ARROW:
					break;

				}
			}
		}

	}

	void MapTerm(int pattern) // making safe zone
	{
		int height = 0;
		TextAsset map = Resources.Load<TextAsset> (MapPath + "map_TERM");
		StreamReader mapTerm = new StreamReader (new MemoryStream(map.bytes));
		while (!mapTerm.EndOfStream) {
			string line = mapTerm.ReadLine ();
			for (int i = 0; i < 5; i++) {
				MapObjects.Add (((pattern + 1) * 30 - 5 + i) + "," + height, int.Parse (line.ToCharArray () [i] + ""));
			}
			height++;
		}
	}
}
