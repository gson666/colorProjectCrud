using ColorP.Services;
using Microsoft.AspNetCore.Mvc;

namespace ColorP.Controllers
{
    public class ColorController : Controller
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var colors = await _colorService.GetColorsAsync();
            return View("Index",colors);
        }

        [HttpPost]
        public IActionResult Add(Color color)
        {
            try
            {
                _colorService.AddColorAsync(color).Wait();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Update(Color color)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                await _colorService.UpdateColorAsync(color);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public IActionResult UpdateOrder([FromBody] List<DisplayOrderUpdate> updates)
        {
            try
            {
                foreach (var update in updates)
                {
                    _colorService.UpdateDisplayOrderAsync(update.Id, update.DisplayOrder).Wait();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        public class DisplayOrderUpdate // אובייקט עזר לצורך פעולת שינוי מיקום צבע בטבלה
        {
            public int Id { get; set; }
            public int DisplayOrder { get; set; }
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _colorService.DeleteColorAsync(id);
            return Json(new { success = true });
        }
    }
}
