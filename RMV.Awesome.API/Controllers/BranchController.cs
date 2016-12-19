
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
        public List<BranchList.RMVBranch> GetBranchList()
        {
            return BranchList.GetAll();
        }

        [Route("api/branch/loc")]
        public List<BranchList.RMVBranch> GetBranchListDistance(double lat, double lng)
        {
            return BranchList.GetAll(lat, lng);
        }

        [Route("api/branch/refresh")]
        public async Task<bool> GetRefreshTimes()
        {
            return await BranchList.RefreshSamples();
        }
    }
}