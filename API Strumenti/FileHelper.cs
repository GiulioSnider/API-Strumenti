using API_Strumenti.Model;
using System.Text.Json;
using System.Xml.Linq;

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
            SerializeAndWrite(instrumentList);
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
        public static MusicInstrument GetById(int id)
        {
            var instrumentList = ReadAndDeserializeFile().ToList();
            return instrumentList.FirstOrDefault(instrument => instrument.Id.Equals(id));
        }
        public static void DeleteInstrumentFromList(MusicInstrument musicInstrument, List<MusicInstrument> musicInstruments)
        {
            musicInstruments.Remove(musicInstrument);
            SerializeAndWrite(musicInstruments);
        }
        private static IEnumerable<MusicInstrument> ReadAndDeserializeFile()
        {
            var fileContent = File.ReadAllText(_path);
            if(fileContent == string.Empty)
            {
                return null;
            }
            return JsonSerializer.Deserialize<List<MusicInstrument>>(fileContent);
        }
        private static void SerializeAndWrite(List<MusicInstrument> musicInstruments)
        {
            var SerializedList = JsonSerializer.Serialize(musicInstruments);
            File.WriteAllText(_path, SerializedList);
        }
    }
}
