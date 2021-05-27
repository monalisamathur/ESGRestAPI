using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net;
namespace ESGRESTAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ESGCustomerController : ControllerBase
    {
       

        private CustomerDataInsert customerDataInsert;
        private ApiResponse response;

        [HttpGet]
        public string GetCustomerDetails(int customerref)
        {
            customerDataInsert = new CustomerDataInsert();
            

            response = new ApiResponse();

            //validate customer ref
            if(customerref <= 0)
            {
                response.httpcode = (int)HttpStatusCode.BadRequest;
                response.message = "incorrect id";
                Response.StatusCode = response.httpcode;
                return JsonSerializer.Serialize(response);
            }


            // call database to retrieve 
            CustomerDetails custDetails =  customerDataInsert.fetchRecord(customerref);
            
            if(custDetails == null)
            {
                // return error message 
                response.httpcode = (int) HttpStatusCode.NotFound;
                response.message = "No record found";
                Response.StatusCode = response.httpcode;
                return JsonSerializer.Serialize(response);
            }
              
             return JsonSerializer.Serialize(custDetails); 

        }

        [HttpPost]
        public string RecordCustomerDetails(CustomerDetails custDetails)
        {
            customerDataInsert = new CustomerDataInsert();
            response = new ApiResponse();

            // validation of request
            if (!validateCustomerDetails(custDetails))
            {
                response.httpcode = (int)HttpStatusCode.BadRequest;
                Response.StatusCode = response.httpcode;
                return JsonSerializer.Serialize(response);
            }

            // call database to store
            Boolean resp = customerDataInsert.insertRecord(custDetails);

            if (resp)
            {
                response.httpcode = (int) HttpStatusCode.Created;
                response.message = "Created record";
                Response.StatusCode = response.httpcode;
            }
            else
            {
                response.httpcode = (int) HttpStatusCode.BadRequest;
                response.message = "Unable to Created record";
                Response.StatusCode = response.httpcode;
            }

            return JsonSerializer.Serialize(response); 

        }

        //validation function
        private Boolean validateCustomerDetails(CustomerDetails customerDetails)
        {
            if (customerDetails.CustomerRef <= 0)
            {
                
                response.message = "id should be > 0";

                return false;
            }

            if (String.IsNullOrEmpty(customerDetails.CustomerName) || customerDetails.CustomerName.Length > 45)
            {
                
                response.message = "Incorrect Customer name";
                return false;
            }

            if ( String.IsNullOrEmpty(customerDetails.AddressLine1) || customerDetails.AddressLine1.Length > 100)
            {
                
                response.message = "Incorrect AddressLine1";
                return false;
            }

            if ( String.IsNullOrEmpty(customerDetails.AddressLine2) || customerDetails.AddressLine2.Length > 100)
            {

                response.message = "Incorrect AddressLine2";
                return false;
            }
            if ( String.IsNullOrEmpty(customerDetails.Town) || customerDetails.Town.Length > 45)
            {

                response.message = "Incorrect Town";
                return false;
            }
            if ( String.IsNullOrEmpty(customerDetails.County) || customerDetails.County.Length > 45)
            {

                response.message = "Incorrect County";
                return false;
            }
            if ( String.IsNullOrEmpty(customerDetails.Country) || customerDetails.Country.Length > 45)
            {

                response.message = "Incorrect Country";
                return false;
            }
            if ( String.IsNullOrEmpty(customerDetails.Postcode) || customerDetails.Postcode.Length > 45)
            {

                response.message = "Incorrect PostCode";
                return false;
            }

            return true;
        }
    }
}
