using UnityEngine;
using System.IO;

[System.Serializable]
public class GameState
{
	public static bool newGame;
	public static byte[,] Lab;
	public static byte[,] Desert;
	public static byte[,] Beach;
	public static byte[,] Plains;
	public static int health;
	public static Transform spawnPoint;

	public void Save()
	{
		newGame = false;
		string dataString = JsonUtility.ToJson(this); 
		string filePath = Application.persistentDataPath + "/gamedata.json";
		Debug.Log(filePath);
		Debug.Log(dataString);
		File.WriteAllText(filePath, dataString);
	}
	public void Load()
	{
		string dataString = "";
		string filePath = Application.persistentDataPath + "/gamedata.json";
		if (File.Exists(filePath)) dataString = File.ReadAllText(filePath);
		if (filePath != "")
		{
			GameState dataClone = JsonUtility.FromJson<GameState>(dataString);
			Debug.Log(dataString);
		}
	}
}
