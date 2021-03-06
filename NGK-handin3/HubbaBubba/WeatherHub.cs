﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGK_handin3.HubbaBubba
{
    public class WeatherHub: Hub
    {
        public async Task SendWeather(object message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
