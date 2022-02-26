﻿using NationalParkWeb.Models;
using NationalParkWeb.Repository.IRepository;
using System.Net.Http;

namespace NationalParkWeb.Repository
{
    public class NationalParkRepository : Repository<NationalPark>, INationalParkRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NationalParkRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
