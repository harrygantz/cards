using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;


namespace LevelManagement.Data
{
    public class JSONSaver
    {
        private static readonly string _filename = "saveData1.sav";

        // This is give us the filename of whatever we want to save or load
        public static string GetSaveFilename()
        {
            // This persistentDataPath is universal to all systems. NOTE: the path is internal to unity so the
            // "/" is used no matter what system your targe buld is. 
            return Application.persistentDataPath + "/" + _filename;
        }
        
        // save our file to disk
        public void Save(SaveData data)
        {
            data.hashValue = String.Empty;
            
            // **STEP 1) Converting the SaveData object into a json formatted string, hash it for security purposes,
            // then we need to reconvert the data to JSON to make sure it gets written to disk properly.
            string json = JsonUtility.ToJson(data);
            data.hashValue = GetSHA256(json);
            json = JsonUtility.ToJson(data);
            
            // **STEP 2) Create a new file stream to prepare us for input and output to a file
            string saveFilename = GetSaveFilename();
            FileStream fileStream = new FileStream(saveFilename, FileMode.Create);

            // ** STEP 3) Open the file, preform the write, and close the file. Ususally we could open the filestream,
            // write to the file, and close it. C# makes is simplified using StreamWriter as seen below creating a temp
            // object that you'll use to write to the file. The using syntax tells the program we are going to dispose of
            // the StreamWriter when we are finished using it.
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }
        }
        
        // load a file from disk. Returns true if data was loaded and false otherwise. 
        public bool Load(SaveData data)
        {
            string loadFileName = GetSaveFilename();
            // Want to check if the file exists.
            if (File.Exists(loadFileName))
            {
                // similar to above (**Step 3) except this time we are reading the file rather than writing of course.
                using (StreamReader reader = new StreamReader(loadFileName))
                {
                    // now we want to do the opposite and read the data and store it into a string.
                    string json = reader.ReadToEnd();

                    if (CheckData(json))
                    {
                        JsonUtility.FromJsonOverwrite(json, data);
                    }
                    else
                    {
                        Debug.LogWarning("JSONSAVER Load: invalid hash. Aborting file read...");
                    }

                }
                
                return true;
            }

            return false;
        }

        // We will probably use this a lot for debugging. 
        public void Delete()
        {
            File.Delete(GetSaveFilename());
        }

        // Checks to see if the data has been tampered with. If true data has been preserved else data is corrupt. 
        private bool CheckData(string json)
        {
            // temp object to read in the JSON string
            SaveData tempSaveData = new SaveData();
            // read JSON string into the temp object to compare hash values
            JsonUtility.FromJsonOverwrite(json, tempSaveData);

            // weve already saved the hash with this data so we extract that string
            string oldHash = tempSaveData.hashValue;
            // we can clear it out since we saved it above
            tempSaveData.hashValue = string.Empty;

            // now we run the hashing algorithm again after we convert the data to a JSON formatted string
            string tempJson = JsonUtility.ToJson(tempSaveData);
            string newHash = GetSHA256(tempJson);

            return (oldHash == newHash);

        }
        public string GetHexStringFromHash(byte[] hash)
        {
            string hexString = String.Empty;

            foreach (byte b in hash)
            {
                // Notice the code "x2" in ToString() -- the x mean you want a hexidecimal string and 2 means two digits.
                // So each byte turns into two digits and we just concatinate them together to make one big hex string.
                hexString += b.ToString("x2");
            }

            return hexString;
        }

        // Converts a piece of text into a hash value expressed as a string
        private string GetSHA256(string text)
        {
            // SHA256 expects to recieve our text not as a string but as an array of bytes. To handle this converstion
            // we'll use the conversion library from System.Text called Encoding.UTF8.GetBytes() to convert our text to
            // an array of bytes so that we can use it in .NETs SHA25g implementation. 
            byte[] textToBytes = Encoding.UTF8.GetBytes(text);
            
            // create a temporary instance to calculate the hash values.
            SHA256Managed mySHA256 = new SHA256Managed();

            // now we store the resulting hash value of mySHA256 is stored in hashValue but it's not in a format 
            // that is friendly to save to disk. 
            byte[] hashValue = mySHA256.ComputeHash(textToBytes);

            return GetHexStringFromHash(hashValue);
        }
    }
}
