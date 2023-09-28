using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.Security;
using Microsoft.AspNetCore.Mvc;

namespace QuickBookOauthAPI.Controllers;

[Route("api/[controller]/[action]")]
public class CustomerController : ControllerBase
{
    private const string AccessToken =
        "eyJlbmMiOiJBMTI4Q0JDLUhTMjU2IiwiYWxnIjoiZGlyIn0..-NU3-kHW6_0eM-MfRPZATA.q1rf3I1DG5bgn9BEoU4mL0IMz1VxmxAh0S6xXLgpDz2QeGABGXU5BiirOwkRmfzA1pEzz8eQe16iGZ1pxaPCvSZ_S771HMQHfv8xHLrkzcptPUTl2yROPQ7iuMRkStWUfGGV6H4lWKpXkWsNpRlmlLJO-AdTYSoB-cN1eg5yStTOAgjBI7txKqIkpqy_I2D3HDa7UDoiJxUSztuskLyY42WG-_Qob-Jkib4Ccrm1iINrEe6OZTTszrYuyy1yULyRggRL__uAKf7kiU4WwqvPt7XJ134aXxSo27hk6heHCWoomYLQnfT55OWekYW1_1eHbvBae0WaBRKF2lvvL7UTPJiSI01aGK64N5_HK-5e24eJqEYG-n9VAkSoyvWqVCNyrQeDcHPRoP7tSzSskTt6o7fgI4x3vlMdG9t2Cyx-r-H-dymBMGRgh5urrUgfW-UbKicCctr1Mj6lWN7NpAMVlHnLH_b5f46OSPlpKz3L9JAgJ427ePYXDoVG26klRj73_6aPqX1pagBz-FUw_9xRslvXfksivvfORB0OeJkSjXCQF3hoUeB1ElF2X5TEtG-Ve836vGBz661_o4x4PQEEyWY8pArYd_HheaxtsCAfpIFK3DtgdBHaquyTvjF1QljckYoeRwrvzw2s2mKAqbtcJ-LsJO5EX0o7-ShZYVOtX5tMg9thEhQ_NDKAto9DPkKeytclJgNYLakp_zmWOXbPgV1DcjzmeV7_dkyHSARgoBz6MRhYnzp9rQZCgFCWL3Pq.9kQfMVnZL03BuIfGGLWyMg";

    private const string RealmId = "4620816365342237080";

    [HttpPost]
    public Customer Create(Customer customer)
    {
        var validator = new OAuth2RequestValidator(AccessToken);
        var serviceContext = new ServiceContext(RealmId, IntuitServicesType.QBO, validator);
        serviceContext.IppConfiguration.MinorVersion.Qbo = "23";
        serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";

        DataService service = new DataService(serviceContext);

        var addedCustomer = service.Add(customer);

        return addedCustomer;
    }

    [HttpGet("{customerId:int}")]
    public Customer GetById(int customerId)
    {
        var validator = new OAuth2RequestValidator(AccessToken);
        var serviceContext = new ServiceContext(RealmId, IntuitServicesType.QBO, validator);
        serviceContext.IppConfiguration.MinorVersion.Qbo = "69";
        serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";

        var service = new DataService(serviceContext);
        var customer = new Customer
        {
            Id = customerId.ToString()
        };
        var result = service.FindById(customer);
        return result;
    }

    [HttpGet]
    public IEnumerable<Customer> GetAll()
    {
        var validator = new OAuth2RequestValidator(AccessToken);
        var serviceContext = new ServiceContext(RealmId, IntuitServicesType.QBO, validator);
        serviceContext.IppConfiguration.MinorVersion.Qbo = "69";
        serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";

        var service = new DataService(serviceContext);
        var result = service.FindAll(new Customer());
        return result;
    }
    
    [HttpPut]
    public Customer FullUpdate([FromBody] Customer customer)
    {
        var validator = new OAuth2RequestValidator(AccessToken);
        var serviceContext = new ServiceContext(RealmId, IntuitServicesType.QBO, validator);
        serviceContext.IppConfiguration.MinorVersion.Qbo = "23";
        serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
        var service = new DataService(serviceContext);
        return service.Update(customer);
    }

    [HttpPatch]
    public Customer SparseUpdate([FromBody] Customer customer)
    {
        customer.sparse = true;
        customer.sparseSpecified = true;
        customer.SyncToken = "0";
        var validator = new OAuth2RequestValidator(AccessToken);
        var serviceContext = new ServiceContext(RealmId, IntuitServicesType.QBO, validator);
        serviceContext.IppConfiguration.MinorVersion.Qbo = "23";
        serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";

        var service = new DataService(serviceContext);
        return service.Update(customer);
    }
}