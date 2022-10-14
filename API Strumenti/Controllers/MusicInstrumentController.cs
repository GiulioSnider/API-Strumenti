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
            if (FileHelper.GetByName(name) is null)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}
