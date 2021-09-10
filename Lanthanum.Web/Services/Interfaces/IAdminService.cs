using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanthanum.Web.Domain;

namespace Lanthanum.Web.Services.Interfaces
{
    public interface  IAdminService
    {
        public Task<IEnumerable<Article>> GetAllArticles();
    }
}
