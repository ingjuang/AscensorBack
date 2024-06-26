﻿using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Data;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Business.Interfaces;

namespace Business.Services
{
    public class ElevatorJob : IElevatorJob
    {
        private AppDbContext _db;
        private bool isActiveShedule;
        public ElevatorJob(AppDbContext db)
        {
            _db = db;
        }

        public async void ElevatorProcess()
        {

            if (Validators.isShedulingActivate)
            {
                return;
            }

            await Console.Out.WriteLineAsync("inicia Shedule");
            
            Request RToValidate = await _db.Requests.FirstOrDefaultAsync();
            if (RToValidate == null)
            {
                await Console.Out.WriteLineAsync("Finaliza Shedule");
                return;
            }
            Elevator elevator = await _db.Elevators.FirstOrDefaultAsync(x => x.Id == 1);
            Validators.isShedulingActivate = true;
            Request? rPos = null;
            Request? rNeg = null;
            if (elevator.Direction == 0)
            {
                // cuando Direction = 0 está el ascensor parado
                rPos = await ClosestPositiveNumber(elevator.CurrentFloor);
                rNeg = await ClosestNegativeNumber(elevator.CurrentFloor);
                if (rPos != null && rNeg != null)
                {
                    if (rPos.floorToGo - elevator.CurrentFloor < rNeg.floorToGo - elevator.CurrentFloor)
                    {
                        elevator.Direction = 1;
                    }
                    else if (rPos.floorToGo - elevator.CurrentFloor > rNeg.floorToGo - elevator.CurrentFloor)
                    {
                        elevator.Direction = 2;
                    }
                }
                else if (rPos == null)
                    elevator.Direction = 2;
                else if (rNeg == null)
                    elevator.Direction = 1;
                _db.Elevators.Update(elevator);
                await _db.SaveChangesAsync();
            }
            else if (elevator.Direction == 1)
            {
                // cuando Direction = 1 está el ascensor subiendo

                Request request = await _db.Requests.Where(x => x.floorToGo == elevator.CurrentFloor).FirstOrDefaultAsync();
                if (request != null)
                {
                    try
                    {
                        await Console.Out.WriteLineAsync("Entra +1");
                        _db.Requests.Remove(request);
                        elevator.Direction = 0;
                        elevator.Action = "Abrir puertas";
                        _db.Elevators.Update(elevator);
                        await _db.SaveChangesAsync();
                        await Task.Delay(15000); // Esperamos 3 segundos
                        elevator.Action = "Puertas cerradas";
                        _db.Elevators.Update(elevator);
                        await _db.SaveChangesAsync();
                    }
                    catch(Exception ex)
                    {
                        Console.Out.WriteLine(ex);
                    }

                }
                else
                {
                    if (elevator.CurrentFloor != 9)
                    {
                        elevator.CurrentFloor = elevator.CurrentFloor + 1;
                        _db.Elevators.Update(elevator);
                        await _db.SaveChangesAsync();
                        await Task.Delay(3000); // Esperamos 1 segundo

                    }
                    else
                    {
                        elevator.CurrentFloor = 9;
                        elevator.Direction = 2;
                        _db.Elevators.Update(elevator);
                        await _db.SaveChangesAsync();
                    }

                }
            }
            else if (elevator.Direction == 2)
            {
                // cuando Direction = 2 está el ascensor bajando
                Request request = await _db.Requests.Where(x => x.floorToGo == elevator.CurrentFloor).FirstOrDefaultAsync();
                if (request != null)
                {
                    try
                    {
                        _db.Requests.Remove(request);
                        elevator.Action = "Abrir puertas";
                        elevator.Direction = 0;
                        _db.Elevators.Update(elevator);
                        await _db.SaveChangesAsync();
                        await Task.Delay(15000); // Esperamos 3 segundos
                        elevator.Action = "Puertas cerradas";
                        _db.Elevators.Update(elevator);
                        await _db.SaveChangesAsync();
                    }
                    catch(Exception ex)
                    {
                        Console.Out.WriteLine(ex);
                    }
            }
                else
                {
                    if (elevator.CurrentFloor != 1)
                    {
                        elevator.CurrentFloor = elevator.CurrentFloor - 1;
                        _db.Elevators.Update(elevator);
                        await _db.SaveChangesAsync();
                        await Task.Delay(3000); // Esperamos 1 segundo

                    }
                    else
                    {
                        elevator.CurrentFloor = 1;
                        elevator.Direction = 1;
                        _db.Elevators.Update(elevator);
                        await _db.SaveChangesAsync();
                    }

                }
            }
            Validators.isShedulingActivate = false;
        }


        private async Task<Request> ClosestPositiveNumber(int currentFloor)
        {
            Request request = await _db.Requests.Where(x => x.floorToGo > currentFloor).OrderBy(x => x.floorToGo).FirstOrDefaultAsync();
            return request;
        }
        private async Task<Request> ClosestNegativeNumber(int currentFloor)
        {
            Request request = await _db.Requests.Where(x => x.floorToGo < currentFloor).OrderByDescending(x => x.floorToGo).FirstOrDefaultAsync();
            return request;
        }
    }
}
