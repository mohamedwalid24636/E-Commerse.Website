using AutoMapper;
using DomanLayer.Contracts;
using DomanLayer.Exceptions;
using DomanLayer.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared.DataTransferObjects;
using Shared.MachineReportDtos;

namespace Service
{
    public class MachineReportService : IMachineReportService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;

        public MachineReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ===================== GET ALL =====================
        public async Task<IEnumerable<MachineReportDto>> GetAllAsync()
        {
            var Repo = _UnitOfWork.GetRepository<MachineReport, int>();
            var specification = new MachineInfoSpecification();
            var Reports = await Repo.GetAllAsync(specification);

            var Data = _mapper.Map<IEnumerable<MachineReport>, IEnumerable<MachineReportDto>>(Reports);

            return Data;
        }

        // ===================== GET BY ID =====================
        public async Task<MachineReportDto?> GetByIdAsync(int id)
        {
            var Repo = _UnitOfWork.GetRepository<MachineReport, int>();
            var specification = new MachineInfoSpecification(id);
            var Report = await Repo.GetByIdAsync(specification);

            if (Report == null)
                throw new MachineNotFoundException(id);

            var Data = _mapper.Map<MachineReport, MachineReportDto>(Report);

            return Data;
        }

        // ===================== ADD =====================
        public async Task AddAsync(MachineReportDto dto)
        {
            var Repo = _UnitOfWork.GetRepository<MachineReport, int>();

            // 🔍 دور على الجهاز بالـ MachineId
            var existing = (await Repo.GetAllAsync())
                .FirstOrDefault(x => x.MachineId == dto.MachineId);

            if (existing == null)
            {
                // 🆕 جهاز جديد
                var entity = _mapper.Map<MachineReport>(dto);
                entity.CreatedAt = DateTime.UtcNow;

                await Repo.AddAsync(entity);
            }
            else
            {
                // 🔄 جهاز موجود → نعمل update

                // 🟢 تحديث البرامج
                foreach (var softwareDto in dto.Softwares)
                {
                    if (softwareDto.Status == "Added")
                    {
                        existing.Softwares.Add(new Software
                        {
                            Name = softwareDto.Name,
                            Version = softwareDto.Version
                        });
                    }
                    else if (softwareDto.Status == "Removed")
                    {
                        var toRemove = existing.Softwares
                            .FirstOrDefault(s =>
                                s.Name == softwareDto.Name &&
                                s.Version == softwareDto.Version);

                        if (toRemove != null)
                            existing.Softwares.Remove(toRemove);
                    }
                }

                // 🟢 إضافة الأخطاء (errors بتتضاف بس)
                foreach (var errorDto in dto.Errors)
                {
                    existing.Errors.Add(new Error
                    {
                        Type = Enum.Parse<DomanLayer.Models.ErrorType>(errorDto.Type.ToString()),
                        Source = errorDto.Source,
                        Message = errorDto.Message,
                        Time = errorDto.Time
                    });
                }

                Repo.Update(existing);
            }

            await _UnitOfWork.SaveChangesAsync();
        }

        // ===================== UPDATE =====================
        public async Task UpdateAsync(int id, MachineReportDto dto)
        {
            var Repo = _UnitOfWork.GetRepository<MachineReport, int>();

            var Existing = await Repo.GetByIdAsync(id);

            if (Existing == null)
                throw new MachineNotFoundException(id);

            _mapper.Map(dto, Existing); // update existing entity

            Repo.Update(Existing);

            await _UnitOfWork.SaveChangesAsync();
        }

        // ===================== DELETE =====================
        public async Task DeleteAsync(int id)
        {
            var Repo = _UnitOfWork.GetRepository<MachineReport, int>();

            var Entity = await Repo.GetByIdAsync(id);

            if (Entity == null)
                throw new MachineNotFoundException(id);

            Repo.Delete(Entity);

            await _UnitOfWork.SaveChangesAsync();
        }
    }
}