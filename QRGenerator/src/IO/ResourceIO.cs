using System.Reflection;

namespace QRGenerator.IO;

/// <summary>
/// Class holding methods to read in files included in the resources folder. Handles parsing the files into 
/// specific formats.
/// </summary>
public static class ResourceIO {
    /// <summary>
    /// Reads in a text file containing a collection of chars and the integer values they encode into <br/>
    /// The format of these files should be <br/>
    /// {Char value} {int value} <br/>
    /// Ex. <br/>
    /// A 10 <br/>
    /// See Resources/QRAlphanumericEncodingTable.txt for an example
    /// </summary>
    /// <param name="resourceName">The name of the file contaning the encoding tabled</param>
    /// <returns>A Dictionary with the chars read in as the key and the ints they encode to as the values</returns>
    public static Dictionary<char, int> ReadInEncodingTable(string resourceName){
        Dictionary<char, int> readDict = new Dictionary<char, int>();

        //TODO: Verify the file format is correct
        var assembly = Assembly.GetExecutingAssembly();
        using (Stream? stream = assembly.GetManifestResourceStream(resourceName)){
            if (stream == null){
                throw new InvalidOperationException("Could not get resource stream for resource " + resourceName);
            }
            using (StreamReader reader = new StreamReader(stream))
            {
                if(reader == null){
                    throw new InvalidOperationException("Could not create stream reade.");
                }
                while(reader.Peek() != -1){
                    char toEncode = (char) reader.Read();
                    string? encodingStr = reader.ReadLine();

                    if(encodingStr != null){
                        int encoding = int.Parse(encodingStr);

                        readDict.Add(toEncode, encoding);
                    }                    

                }
            }

        }

        return readDict;
    }
}