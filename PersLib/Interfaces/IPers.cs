using Microsoft.AspNetCore.Http;
using PersLib.Data;
using PersLib.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PersLib.Interfaces
{
    public interface IPers
    {
        IEnumerable<Pers> GetPers();    
        IEnumerable<Pers> GetOnePers(string nameChar);
        HttpResponseMessage AddPers(AddPersClass newPers);
    }
}
