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
    public class ElevatorService : IElevatorService
    {
        private AppDbContext _db;  
        private IElevatorJob _job;
        private bool initial = false;
        public ElevatorService(AppDbContext db, IElevatorJob elevatorJob) 
        {
            _db = db;
            _job = elevatorJob;
            InitialValues();
        }
        public async Task<PetitionResponse> GetElevator()
        {
            _job.ElevatorProcess();
            Elevator elevator = await _db.Elevators.FirstOrDefaultAsync(x => x.Id == 1);
            return new PetitionResponse
            {
                message = "Operación exitosa",
                module = "Elevator",
                URL = "api/Elevator/GetCurrentFloor",
                result = elevator,
                success = true
            };
        }
        public async Task<PetitionResponse> AddRequestToGo(int floor)
        {
            Request request = new Request
            {
                floorToGo = floor
            };
            _db.Requests.Add(request);
            if(await _db.SaveChangesAsync() <= 0)
            {
                return new PetitionResponse
                {
                    message = "Error al intentar guardar request",
                    module = "Elevator",
                    URL = "api/Elevator/AddRequestToGo",
                    result = null,
                    success = false
                };
            }else 
            {
                return new PetitionResponse
                {
                    message = "Operación exitosa",
                    module = "Elevator",
                    URL = "api/Elevator/AddRequestToGo",
                    result = request,
                    success = true
                };
            }
        }

        public async Task<Elevator> Get()
        {
            return await _db.Elevators.FirstOrDefaultAsync(x => x.Id == 1);
        }

        public async Task<Elevator> Update(Elevator elevator)
        {
            _db.Elevators.Update(elevator);
            await _db.SaveChangesAsync();
            return elevator;
        }



        private void InitialValues()
        {
            Elevator e = _db.Elevators.FirstOrDefault(x => x.Id == 1);
            if (e == null)
            {
                Elevator elevator = new Elevator();
                elevator.Id = 1;
                elevator.Direction = 0;
                elevator.CurrentFloor = 1;
                elevator.State = false;

                _db.Elevators.Add(elevator);
                _db.SaveChanges();
            }
        }
    }
}
