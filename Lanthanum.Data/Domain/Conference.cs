using Lanthanum.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanthanum.Web.Data.Domain
{
    public class Conference : IEntity
    {
        public int Id { get; init; }
        public string Name { get; set; }
    }
}
