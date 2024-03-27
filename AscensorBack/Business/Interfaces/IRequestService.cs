﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Responses;

namespace Business.Interfaces
{
    public interface IRequestService
    {
        public Task<PetitionResponse> GetRequests();
        public Task<PetitionResponse> StopElevator();
    }
}
