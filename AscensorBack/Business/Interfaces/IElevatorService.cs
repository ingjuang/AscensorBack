using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Responses;

namespace Business.Interfaces
{
    public interface IElevatorService
    {
        public Task<PetitionResponse> GetElevator();
        public Task<PetitionResponse> AddRequestToGo(int floor);
        public Task<Elevator> Get();
        public Task<Elevator> Update(Elevator elevator);
    }
}
