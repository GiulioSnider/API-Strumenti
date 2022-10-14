using API_Strumenti.Model;
using Microsoft.AspNetCore.Mvc;

namespace API_Strumenti.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MusicInstrumentController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllInstruments()
        {
            var instrumentList = FileHelper.GetMusicInstruments();
            return Ok(instrumentList);
        }

        [HttpPost]
        public IActionResult AddInstrument([FromBody] MusicInstrument musicInstrument)
        {
            return Ok(FileHelper.AddInstrument(musicInstrument));
        }

        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name)
        {
            var instrument = FileHelper.GetByName(name);
            if (instrument is null)
            {
                return BadRequest();
            }
            return Ok(instrument);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var instrument = FileHelper.GetById(id);
            if (instrument is null)
            {
                return BadRequest();
            }
            return Ok(instrument);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var instrument = FileHelper.GetById(id);
            if (instrument is null)
            {
                return BadRequest();
            }
            var instrumentList = FileHelper.GetMusicInstruments().ToList();
            FileHelper.DeleteInstrumentFromList(instrument, instrumentList);
            return NoContent();
        }

        [HttpGet("getByFilter")]
        public IActionResult GetByFilter(string filter, string parameter)
        {
            switch (filter)
            {
                case "Name":
                    {
                        var resultInstrument = FileHelper.GetMusicInstruments().ToList().SingleOrDefault(instrument => instrument.Name.Equals(parameter));
                        return Ok(resultInstrument);
                    }
                case "Model":
                    {
                        var resultInstrument = FileHelper.GetMusicInstruments().ToList().SingleOrDefault(instrument => instrument.Model.Equals(parameter));
                        return Ok(resultInstrument);
                    }
                case "BrandName":
                    {
                        var resultInstrument = FileHelper.GetMusicInstruments().ToList().SingleOrDefault(instrument => instrument.BrandName.Equals(parameter));
                        return Ok(resultInstrument);
                    }
                default:
                    {
                        return BadRequest();
                    }
            }
        }
    }
}
