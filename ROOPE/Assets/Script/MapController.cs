using UnityEngine;
using System.Collections;
using System.IO;

public class MapController : MonoBehaviour {
	private string FILE_PATH;
	private Hashtable MapObjects;
	private int stage = 1;

	// Use this for initialization
	void Start () {
		FILE_PATH = Application.persistentDataPath + "/";

		MapObjects = new Hashtable ();
		readMapFromFile ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void WriteFile()
	{
		StreamWriter fileWriter = new StreamWriter (FILE_PATH);

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
			"map_1A.dat",
			"map_2A.dat",
			"map_3A.dat",
			"map_4A.dat",
			"map_17A.dat",
			"map_6A.dat",
			"map_7A.dat",
			"map_8A.dat",
			"map_9A.dat",
			"map_10A.dat",
			"map_11A.dat",
			"map_12A.dat",
			"map_13A.dat",
			"map_14A.dat",
			"map_15A.dat",
			"map_16A.dat",
			"map_5A.dat",
			"map_1B.dat",
			"map_2B.dat",
			"map_3B.dat",
			"map_4B.dat",
			"map_6B.dat",
			"map_7B.dat",
			"map_8B.dat",
			"map_9B.dat",
			"map_10B.dat",
			"map_11B.dat",
			"map_12B.dat",
			"map_13B.dat",
			"map_14B.dat",
			"map_15B.dat",
			"map_16B.dat",
			"map_17B.dat",
			"map_5B.dat",
			"map_1C.dat", 
			"map_2C.dat",
			"map_3C.dat",
			"map_4C.dat",
			"map_6C.dat",
			"map_7C.dat",
			"map_8C.dat",
			"map_9C.dat",
			"map_10C.dat",
			"map_11C.dat",
			"map_12C.dat",
			"map_13C.dat",
			"map_14C.dat",
			"map_15C.dat",
			"map_16C.dat",
			"map_7C.dat"
		};
		for (pattern = 0; pattern < 45; pattern++) {
			if (pattern % 15 == 0 && pattern != 0)
				stage++;
			if (stage == 1)
				random_pattern = Random.Range (0, 16);
			else if (stage == 2)
				random_pattern = Random.Range (17, 33);
			else
				random_pattern = Random.Range (33, 49);
			StreamReader fileReader = new StreamReader (FILE_PATH + TILE[random_pattern]); 
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

		for (int j = 0; j < 10; j++) {
			string temp = "";
			for (int i = 0; i < 30 * 17; i++) {
				temp += MapObjects [i + "," + j] + " ";
			}
			Debug.Log (temp);
		}

	}

	void MapTerm(int pattern) // making safe zone
	{
		int height = 0;
		StreamReader mapTerm = new StreamReader (FILE_PATH + "map_TERM.dat");
		while (!mapTerm.EndOfStream) {
			string line = mapTerm.ReadLine ();
			for (int i = 0; i<5; i++) {
				MapObjects.Add (((pattern + 1) * 30 - 5 + i) + "," + height, int.Parse (line.ToCharArray () [i] + ""));
			}
			height++;
		}
	}
}
