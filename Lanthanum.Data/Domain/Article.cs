﻿    using System;
using System.Collections.Generic;
using Lanthanum.Data;
using Lanthanum.Web.Data.Domain;

namespace Lanthanum.Web.Domain
{
    public class Article: IEntity
    {
        public int Id { get; init; }
        public List<User> Authors { get; set; }
        public string Headline { get; set; }
        public string Header { get; set; }
        public string HTMLText { get; set; }
        public string MainText { get; set; }
        public string Location { get; set; }
        public string Alternative { get; set; }
        public string LogoPath { get; set; }
        public KindOfSport KindOfSport { get; set; }
        public Team Team { get; set; }
        public List<Comment> Comments { get; set; }
        public int ViewsCount { get; set; }
        public int CommentsCount { get; set; }
        public DateTime DateTimeOfCreation { get; set; }
        public ArticleStatus ArticleStatus { get; set; }
        public Picture LogoPicture { get; set; }
        public Conference Conference { get; set; }
    }

    public enum ArticleStatus
    {
        Published,
        Unpublished
    }
}
