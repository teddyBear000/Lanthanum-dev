using System;
using System.Collections.Generic;
using Lanthanum.Data;

namespace Lanthanum.Web.Domain
{
    public class FooterTabItem : IEntity
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Link { get; set; }
        public bool IsDisplaying { get; set; }
    }
}
