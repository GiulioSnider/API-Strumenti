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
            var fileContent = File.ReadAllText(_path);
            if (fileContent == null)
            {
                List<MusicInstrument> list = new List<MusicInstrument>();
                list.Add(musicInstrument);
                var jsonInstruments = JsonSerializer.Serialize(list);
                File.WriteAllText(_path, jsonInstruments);
                return musicInstrument;
            }
            var musicInstrumentsDeserialize = JsonSerializer.Deserialize<List<MusicInstrument>>(fileContent);
            musicInstrumentsDeserialize.Add(musicInstrument);
            var SerializedList = JsonSerializer.Serialize(musicInstrumentsDeserialize);
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

    }
}
