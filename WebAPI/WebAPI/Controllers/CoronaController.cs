﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class CoronaController : ApiController
    {
        // GET: api/Corona
        public List<Datum> Get()
        {
            CoronaOperations coop = new CoronaOperations();
            return coop.GetTheRecords("SELECT * FROM theStats");
        }

        // GET: api/Corona
        public List<Datum> Get(string country)
        {
            CoronaOperations coop = new CoronaOperations();
            return coop.GetTheRecords(string.Format("SELECT * FROM theStats WHERE countrycode = '{0}'", country));
        }

        // GET: api/Corona/5
        public string Get(int id)
        {

            return "value";
        }

        // POST: api/Corona
        public bool Post(Datum datum)
        {
           
            CoronaOperations coop = new CoronaOperations();
            bool res = coop.insertRecord(datum);

            return res;
        }

        // PUT: api/Corona/5
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE: api/Corona/5
        public void Delete(int id)
        {
        }
    }
}
