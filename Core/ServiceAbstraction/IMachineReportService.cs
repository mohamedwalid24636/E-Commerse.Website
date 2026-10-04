using Shared.DataTransferObjects;
using Shared.MachineReportDtos;

namespace ServiceAbstraction
{
    public interface IMachineReportService
    {
        // ===================== GET ALL =====================
        Task<IEnumerable<MachineReportDto>> GetAllAsync();

        // ===================== GET BY ID =====================
        Task<MachineReportDto?> GetByIdAsync(int id);

        // ===================== ADD =====================
        Task AddAsync(MachineReportDto dto);

        // ===================== UPDATE =====================
        Task UpdateAsync(int id, MachineReportDto dto);

        // ===================== DELETE =====================
        Task DeleteAsync(int id);
    }
}