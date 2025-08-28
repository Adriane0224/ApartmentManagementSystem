using Property.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property.Application.Queries
{
    public interface IApartmentQueries
    {
        //Task<ApartmentResponse?> GetApartmentByUnitAsync(string? unit);
        Task<ApartmentResponse?> GetApartmentByIdAsync(Guid id);
        Task<List<ApartmentResponse>> GetAllApartmentsAsync();
        Task<List<ApartmentResponse>> GetAllVacantApartmentsAsync();
        Task<List<ApartmentResponse>> GetAllOccupiedApartmentsAsync();
        Task<List<ApartmentResponse>> GetAllUnderMaintenanceApartments();
        Task <List<ApartmentResponse>> GetAllAvailableApartments();
    }
}
