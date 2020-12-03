using likeButtonApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Article.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly DBContext _ct;
        private readonly IDistributedCache _distributedCache;

        public LikeController(DBContext ct)//, IDistributedCache distributedCache)
        {
          //  _distributedCache = distributedCache;
            _ct = ct;
        }

        // GET: api/<LikeController>
        [HttpGet]
        [Route("getCountLike")]   
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var cnt = new Like();
                cnt = _ct.Like.FirstOrDefault();
                if (cnt == null)
                    return Ok(0);
                return Ok(cnt.qtdlikes);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT api/<LikeController>
        [HttpPut]
        [Route("incrementCount")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> incrementCount()
        {
            try
            {
                Like lk = _ct.Like.FirstOrDefault();
                if (lk != null)
                {
                    lk.qtdlikes = lk.qtdlikes + 1;
                    _ct.Entry(lk).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                else
                {
                    lk = new Like()
                    {
                        qtdlikes = 1
                    };

                    _ct.Add(lk);
                }

                await _ct.SaveChangesAsync();

                return Ok(lk.qtdlikes);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}
