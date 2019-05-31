using DvdLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DvdLibrary.Factories
{
    public static class DvdRepositoryFactory
    {
        public static IDvdRepository Create()
        {
            switch (ConfigurationManager.AppSettings.Get("Mode"))
            {
                case "SampleData":
                    return new DvdRepositoryMock();
                case "EntityFramework":
                    return new DvdRepositoryEF();
                case "ADO":
                    return new DvdRepositoryADO();
                default:
                    throw new ConfigurationErrorsException("A selected mode is not running");
            }
        }
    }
}