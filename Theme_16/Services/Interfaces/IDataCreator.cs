using System.Collections.Generic;
using System.Threading.Tasks;
using Theme_16.Models;

namespace Theme_16.Services.Interfaces
{
    internal interface IDataCreator
    {
        Task FillDB();
    }
}