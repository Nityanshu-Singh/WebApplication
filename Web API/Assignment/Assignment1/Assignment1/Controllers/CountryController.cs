using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment1.Models;

namespace Assignment1.Controllers
{
    public class CountryController : ApiController
    {
        private static List<Country> countrylist = new List<Country>
        {
            new Country { CountryId = 1, CountryName = "INDIA", Capital = "NEW DELHI" },
            new Country { CountryId = 2, CountryName = "USA", Capital = "WASHINGTON DC" },
            new Country { CountryId = 3, CountryName = "UK", Capital = "LONDON" },
            new Country { CountryId = 4, CountryName = "JAPAN", Capital = "TOKYO" },
            new Country { CountryId = 5, CountryName = "NEPAL", Capital = "KATHMANDU" }

        };

        //To view country

        [HttpGet]
        public IEnumerable<Country> Get()
        {
            return countrylist;
        }

        //To add new country
        [HttpPost]
        public List<Country> Post([FromBody] Country cont)
        {
            countrylist.Add(cont);
            return countrylist;
        }

        //To update the country
        [HttpPut]
        public void Put(int cid, [FromUri] Country country)
        {
            countrylist[cid - 1] = country;
        }

        //To delete the country
        [HttpDelete]
        public void Delete(int cid)
        {
            countrylist.RemoveAt(cid - 1);
        }

    }
}