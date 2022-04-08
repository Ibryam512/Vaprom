using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.SearchModel
{
    public class VacationSearch
    {
        DateTime FromDate { get; set; }
        public List<Vacation> Result { get; set; }
    }
}
