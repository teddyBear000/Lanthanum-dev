using Lanthanum.Data;
using Lanthanum.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanthanum.Web.Data.Domain
{
    public class Picture : IEntity
    {
        public int Id { get; init; }
        public string LogoPath { get; set; }
        public string Filter { get; set; }
        public string Size { get; set; }
        public string Crop { get; set; }
    }
}
