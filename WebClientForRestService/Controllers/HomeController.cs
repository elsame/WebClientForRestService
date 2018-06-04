using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Threading.Tasks;
using System.Net.Http;
using RestfulService.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace WebClientForRestService.Controllers
{
    public class HomeController : Controller
    {
        // GET: Default
        //Hosted web API REST Service base url  
        string Baseurl = "http://localhost:52651";

        private void setClientSettings(HttpClient client)
        {
            //Passing service base url  
            client.BaseAddress = new Uri(Baseurl);

            client.DefaultRequestHeaders.Clear();
            //Define request data format  
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        public async Task<ActionResult> Index()
        {
            List<EmployeeModel> EmpInfo = new List<EmployeeModel>();

            using (var client = new HttpClient())
            {
                setClientSettings(client);
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/EmployeeModels");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    EmpInfo = JsonConvert.DeserializeObject<List<EmployeeModel>>(EmpResponse);

                }
                //returning the employee list to view  
                return View(EmpInfo);
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            EmployeeModel EmpInfo = new EmployeeModel();

            using (var client = new HttpClient())
            {
                setClientSettings(client);
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/EmployeeModels/" + id);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    EmpInfo = JsonConvert.DeserializeObject<EmployeeModel>(EmpResponse);

                }
                //returning the employee list to view  
                return View(EmpInfo);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            EmployeeModel EmpInfo = new EmployeeModel();

            using (var client = new HttpClient())
            {
                setClientSettings(client);
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/EmployeeModels/" + id);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    EmpInfo = JsonConvert.DeserializeObject<EmployeeModel>(EmpResponse);

                }
                //returning the employee list to view  
                return View(EmpInfo);
            }
        }

        // POST: EmpModel/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, EmployeeModel empModel)
        {
            try
            {
                // TODO: Add update logic here
                using (var client = new HttpClient())
                {
                    setClientSettings(client);
                    //serialize object to Json and create the HttpContent
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(empModel));
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.PutAsync("api/EmployeeModels/" + id, content);

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                // TODO: Add update logic here
                using (var client = new HttpClient())
                {
                    setClientSettings(client);
                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.DeleteAsync("api/EmployeeModels/" + id);

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Create()
        {
            EmployeeModel emp = new EmployeeModel();
            return View(emp);
        }

        [HttpPost]
        public async Task<ActionResult> Create(EmployeeModel empModel)
        {
            try
            {
                // TODO: Add update logic here
                using (var client = new HttpClient())
                {
setClientSettings(client);
                    //serialize object to Json and create the HttpContent
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(empModel));
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.PostAsync("api/EmployeeModels/", content);

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}