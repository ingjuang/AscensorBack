using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IRequestService
    {
        public Task<Request> GetRequest();
        public Task<Request> ClosestPositiveNumber(int currentFloor);
        public Task<Request> ClosestNegativeNumber(int currentFloor);
        public Task<Request> RequestIfIsInCurrentFloor(int currentFloor);
        public void Remove(Request request);
    }
}
