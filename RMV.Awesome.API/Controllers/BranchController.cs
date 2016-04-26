
using RMV.Awesome.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using System.Xml.Linq;

namespace RMV.Awesome.API.Controllers
{
    public class BranchController : ApiController
    {
        [Route("api/branch")]
        public async Task<List<Branch>> GetBranchList()
        {
            return await BranchList.GetAll();
        }

        [Route("api/branch/loc")]
        public async Task<List<Branch>> GetBranchListDistance(double lat, double lng)
        {
                return await BranchList.GetAll(lat, lng);
        }
    }
}