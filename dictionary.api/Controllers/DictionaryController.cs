using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictionary.Core.Models;
using Dictionary.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dictionary.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DictionaryController : ControllerBase
    {
        private readonly IDictionary _dictionary;
        private readonly ILogger<DictionaryController> _logger;

        public DictionaryController(
            ILogger<DictionaryController> logger,
            IDictionary dictionary)
        {
            _logger = logger;
            _dictionary = dictionary;
        }

        //dictionary/welcome
        [HttpGet("welcome")]
        public string Welcome()
        {
            return "Here is the New Grammatical Dictionary of Polish";
        }

        //dictionary/browser/find?form=        
        [HttpGet("browser/find")]
        public IEnumerable<Entry> Find(string form = "")
        {
            if (form != "")
            {
                return _dictionary.GetEntries(form);
            }
            else
            {
                return null;
            }
        }
    }
}
