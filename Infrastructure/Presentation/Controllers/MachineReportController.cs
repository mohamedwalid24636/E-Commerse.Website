using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects;
using Shared.MachineReportDtos;

namespace API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("api/machine-reports")]
    public class MachineReportController : ControllerBase
    {
        private readonly IMachineReportService _service;

        public MachineReportController(IMachineReportService service)
        {
            _service = service;
        }

        // ===================== GET ALL =====================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // ===================== GET BY ID =====================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // ===================== ADD =====================
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MachineReportDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            await _service.AddAsync(dto);

            return Ok(new { message = "Report added successfully" });
        }

        // ===================== UPDATE =====================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MachineReportDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            await _service.UpdateAsync(id, dto);

            return Ok(new { message = "Report updated successfully" });
        }

        // ===================== DELETE =====================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Ok(new { message = "Report deleted successfully" });
        }
    }
}