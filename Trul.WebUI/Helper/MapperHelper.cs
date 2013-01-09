﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trul.Application.DTO.Profiles;
using Trul.Infrastructure.Crosscutting.Adapter;
using Trul.Infrastructure.Crosscutting.NetFramework.Adapter;

namespace Trul.WebUI.Helper
{
    public class MapperHelper
    {
        public static void Initialise() {
            TypeAdapterFactory.SetCurrent(new AutomapperTypeAdapterFactory());
            MapperProfile.Configure();
        }
    }
}