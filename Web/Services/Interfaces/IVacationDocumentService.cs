using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Services.Interfaces
{
    public interface IVacationDocumentService
    {
        void GenerateDocument(User user, Vacation vacation);
    }
}