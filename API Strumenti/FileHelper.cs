using API_Strumenti.Model;
using System.Text.Json;

namespace API_Strumenti
{
    public static class FileHelper
    {
        private const string _path = "C:\\Users\\user\\Documents\\ACADEMY UNIKEY\\02.BACKEND" +
            "\\Esercizi\\MusicInstrumentsDB\\StrumentiMusicali.txt";
        public static MusicInstrument AddInstrument(MusicInstrument musicInstrument)
        {
            var fileContent = ReadAndDeserializeFile();
            if (fileContent == null)
            {
                List<MusicInstrument> list = new List<MusicInstrument>();
                list.Add(musicInstrument);
                var jsonInstruments = JsonSerializer.Serialize(list);
                File.WriteAllText(_path, jsonInstruments);
                return musicInstrument;
            }
            //var musicInstrumentsDeserialize = JsonSerializer.Deserialize<List<MusicInstrument>>(fileContent);
            //musicInstrumentsDeserialize.Add(musicInstrument);
            var instrumentList = fileContent.ToList();
            instrumentList.Add(musicInstrument);
            var SerializedList = JsonSerializer.Serialize(instrumentList);
            File.WriteAllText(_path, SerializedList);
            return musicInstrument;
        }
        public static IEnumerable<MusicInstrument> GetMusicInstruments()
        {
            var musicInstrumentsDeserialize = JsonSerializer.Deserialize<List<MusicInstrument>>(File.ReadAllText(_path));
            if (musicInstrumentsDeserialize == null)
            {
                throw new Exception("Niente strumenti");
            }
            return musicInstrumentsDeserialize;
        }
        public static MusicInstrument GetByName(string name)
        {
            var instrumentList = ReadAndDeserializeFile().ToList();
            return instrumentList.FirstOrDefault(instrument => instrument.Name.Equals(name));
        }
        public static 
        private static IEnumerable<MusicInstrument> ReadAndDeserializeFile()
        {
            var fileContent = File.ReadAllText(_path);
            if(fileContent == string.Empty)
            {
                return null;
            }
            return JsonSerializer.Deserialize<List<MusicInstrument>>(fileContent);
        }

    }
}
