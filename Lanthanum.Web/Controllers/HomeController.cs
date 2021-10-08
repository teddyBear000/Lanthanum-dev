using Lanthanum.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Lanthanum.Web.Data.Domain;

namespace Lanthanum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbRepository<Team> _teamRepository;
        private readonly DbRepository<KindOfSport> _kindOfSportRepository;
        private readonly DbRepository<Conference> _conferenceRepository;

        public HomeController(ILogger<HomeController> logger, DbRepository<Team> teamRepository, DbRepository<KindOfSport> kindOfSportRepository, DbRepository<Conference> conferenceRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _kindOfSportRepository = kindOfSportRepository;
            _conferenceRepository = conferenceRepository;
        }
        private void Stuff()
        {
            _kindOfSportRepository.AddAsync(new KindOfSport
            {
                Name = "Home"
            }).Wait();
            _kindOfSportRepository.AddAsync(new KindOfSport
            {
                Name = "NBA"
            }).Wait();
            _kindOfSportRepository.AddAsync(new KindOfSport
            {
                Name = "NFL"
            }).Wait();
            _kindOfSportRepository.AddAsync(new KindOfSport
            {
                Name = "MLB"
            }).Wait();
            _kindOfSportRepository.AddAsync(new KindOfSport
            {
                Name = "CBB"
            }).Wait();
            _kindOfSportRepository.AddAsync(new KindOfSport
            {
                Name = "NASCAR"
            }).Wait();
            _kindOfSportRepository.AddAsync(new KindOfSport
            {
                Name = "GOLF"
            }).Wait();
            _kindOfSportRepository.AddAsync(new KindOfSport
            {
                Name = "SOCCER"
            }).Wait();
            _kindOfSportRepository.AddAsync(new KindOfSport
            {
                Name = "TEAMHUB"
            }).Wait();
            _kindOfSportRepository.AddAsync(new KindOfSport
            {
                Name = "LIFESTYLE"
            }).Wait();
            _kindOfSportRepository.AddAsync(new KindOfSport
            {
                Name = "DEALBOOK"
            }).Wait();
            _kindOfSportRepository.AddAsync(new KindOfSport
            {
                Name = "VIDEO"
            }).Wait();

            _teamRepository.AddAsync(new Team
            {
                Name = "Barcelona",
                Location = "Spain"
            }).Wait();
            _teamRepository.AddAsync(new Team
            {
                Name = "Chelsy",
                Location = "England"
            }).Wait();

            _conferenceRepository.AddAsync(new Conference
            {
                Name = "AFC England",
            }).Wait();
            _conferenceRepository.AddAsync(new Conference
            {
                Name = "EFC Africa",
            }).Wait();
        }

        public IActionResult Index()
        {
            Stuff();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
