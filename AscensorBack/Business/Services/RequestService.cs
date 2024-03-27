using Business.Interfaces;
using Core.Models;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class RequestService : IRequestService
    {
        private AppDbContext _db;

        public RequestService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<Request> GetRequest()
        {
            Request request = await _db.Requests.FirstOrDefaultAsync();
            return request;
        }
        public async Task<Request> ClosestPositiveNumber(int currentFloor)
        {
            Request request = await _db.Requests.Where(x => x.floorToGo > currentFloor).OrderBy(x => x.floorToGo).FirstOrDefaultAsync();
            return request;
        }
        public async Task<Request> ClosestNegativeNumber(int currentFloor)
        {
            Request request = await _db.Requests.Where(x => x.floorToGo < currentFloor).OrderByDescending(x => x.floorToGo).FirstOrDefaultAsync();
            return request;
        }

        public async Task<Request> RequestIfIsInCurrentFloor(int currentFloor)
        {
            Request request = await _db.Requests.Where(x => x.floorToGo == currentFloor).FirstOrDefaultAsync();
            return request;
        }

        public async void Remove(Request request)
        {
            _db.Requests.Remove(request);
            await _db.SaveChangesAsync();
        }
    }
}
