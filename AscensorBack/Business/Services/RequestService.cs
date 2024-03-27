using Business.Interfaces;
using Core.Models;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Responses;

namespace Business.Services
{
    public class RequestService : IRequestService
    {
        private AppDbContext _db;

        public RequestService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<PetitionResponse> GetRequests()
        {
            List<Request> requests = await _db.Requests.ToListAsync();
            return new PetitionResponse
            {
                message = "Operación exitosa",
                module = "Request",
                URL = "api/Request/GetRequests",
                result = requests,
                success = true
            };
        }

        public async Task<PetitionResponse> StopElevator()
        {
            Elevator elevator = await _db.Elevators.FirstOrDefaultAsync();
            elevator.Direction = 0;
            _db.Elevators.Update(elevator);
            _db.Requests.RemoveRange(_db.Requests.ToList());
            await _db.SaveChangesAsync();
            return new PetitionResponse
            {
                message = "Operación exitosa",
                module = "Request",
                URL = "api/Request/StopElevator",
                result = null,
                success = true
            };
        }
    }
}
