using AutoMapper;
using FluentResults;
using Property.Application.Commands;
using Property.Application.Errors;
using Property.Application.Response;
using Property.Domain.Entities;
using Property.Domain.Repositories;
using Property.Domain.Services;
using Property.Domain.ValueObject;

namespace Property.Application.CommandHandler
{
    public class ApartmentCommands : IApartmentCommands
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApartmentCommands(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<ApartmentResponse>> AddApartmentAsync(string unit, int floor, string description, CancellationToken cancellationToken)
        {
            // Check if the unit already exists
            List<ApartmentUnit> apartments = await _unitOfWork.Apartments.GetAllAsync();
            if (apartments.Any(a => a.Unit == unit))
            {
                return Result.Fail(new ApartmentError($"Apartment unit '{unit}' already exists."));
            }
            ApartmentUnit apartment = ApartmentUnit.Create(unit, floor, description);
            await _unitOfWork.Apartments.AddAsync(apartment, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Ok(_mapper.Map<ApartmentResponse>(apartment));
        }

        public async Task<Result> DeleteApartmentAsync(string unit, CancellationToken cancellationToken)
        {
            List<ApartmentUnit> apartments = await _unitOfWork.Apartments.GetAllAsync();
            ApartmentUnit? apartment = apartments.FirstOrDefault(p => p.Unit == unit);
            if (apartment == null)
            {
                return Result.Fail(new ApartmentError("error Apartment."));
            }   
            await _unitOfWork.Apartments.DeleteAsync(apartment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Ok();

        }

        public async Task<Result> OccupyApartmentAsync(Guid id, CancellationToken cancellationToken)
        {
            var apartments = await _unitOfWork.Apartments.GetAllAsync();
                var apartment = apartments.FirstOrDefault(p => p.Id.Value == id);
                if (apartment == null)
                    return Result.Fail(new ApartmentError("This property doesn't exist."));

            var apartmentService = new ApartmentStatusService();
            ApartmentUnit occupiedApartment = apartmentService.Occupy(apartment);
            await _unitOfWork.Apartments.UpdateAsync(occupiedApartment, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }

        public async Task<Result> UnderMaintenanceApartmentAsync(string unit, CancellationToken cancellationToken)
        {
            List<ApartmentUnit> apartments = await _unitOfWork.Apartments.GetAllAsync();
            ApartmentUnit? apartment = apartments.FirstOrDefault(p => p.Unit == unit);
            if (apartment == null)
            {
                return Result.Fail(new ApartmentError("This property doesn't exist."));
            }
            var apartmentService = new ApartmentStatusService();
            ApartmentUnit occupiedProperty = apartmentService.MarkAsUnderMaintenance(apartment);
            await _unitOfWork.Apartments.UpdateAsync(occupiedProperty, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }

        public async Task<Result> VacantApartmentByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var apartments = await _unitOfWork.Apartments.GetAllAsync();
            var apartment = apartments.FirstOrDefault(a => a.Id.Value == id);
            if (apartment == null)
                return Result.Fail(new ApartmentError("Apartment not found."));

            var apartmentService = new ApartmentStatusService();
            ApartmentUnit vacantApartment = apartmentService.MarkAsVacant(apartment);
            await _unitOfWork.Apartments.UpdateAsync(vacantApartment, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }

        public async Task<ApartmentUnit?> GetByIdForUpdateAsync(ApartmentId id, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Apartments.GetByIdForUpdateAsync(id, cancellationToken);
        }
        
    }
}
