using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour
{
    public static GameControl control; 

    public float health;
    public float experience;

    private void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }//END Awake

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 30), "Health: " + health);
        GUI.Label(new Rect(10, 40, 150, 30), "Experience: " + experience);
    }

    //With the exception of web this will work for all platforms
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        // persistentDataPath is where the file is going it ends up in appdata on windows...etc "/playerInfo.dat"
        // is the name of the file we are saving
        FileStream fs = File.Create(Application.persistentDataPath + "/playerInfo.dat");
		
        PlayerData data = new PlayerData();
		
        //This is copying the local data into our serializable class so it can be put in a file
        data.health = health;
        data.experience = experience;
        //This is serializing the data using the file stream and then on the next line closing the file
        //Basically it is just writing the container to a file.
        bf.Serialize(fs, data);
        fs.Close();
    }
	
    //With the exception of web this will work for all platformss
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(fs);
            fs.Close();

            health = data.health;
            experience = data.experience;
        }
    }
}
//Serializable Allows me to write this data to a file
[Serializable]
class PlayerData
{
    public float health;
    public float experience;
}