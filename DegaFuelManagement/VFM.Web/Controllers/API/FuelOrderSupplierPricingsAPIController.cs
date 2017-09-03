using System.Net.Http;
using System.Web.Http;
using VFM.Web.Models.Responses;
using VFM.Web.Services;
using VFMClasses.FuelOrder;

namespace VFM.Web.Controllers.API
{
    [RoutePrefix("api/FuelOrderSupplierPricings")]
    public class FuelOrderSupplierPricingsAPIController : ApiController
    {
        [Route("{fuelOrderId:int}"), HttpGet]
        public HttpResponseMessage GetFuelOrderSupplierPricingByFuelOrderId(int fuelOrderId)
        {
            ItemsResponse<FuelOrderSupplierPricings> response = new ItemsResponse<FuelOrderSupplierPricings>();
            response.Items = FuelOrderSupplierPricingsService.GetFuelOrderSupplierPricingByFuelOrderId(fuelOrderId);
            return Request.CreateResponse(response);
        }
    }
}