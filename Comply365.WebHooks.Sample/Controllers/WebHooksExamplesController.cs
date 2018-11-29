using Comply365.WebHooks.Sample.Models.Core;
using Comply365.WebHooks.Sample.Models.WebHooksExamples;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Collections.Generic;

namespace Comply365.WebHooks.Sample.Controllers
{
    public class WebHooksExamplesController : Controller
    {
        // These are in plain text just for demo sake.  In production, Comply365 recommends storing these values securely in accordance with your IT security policies
        private string _siteName = "test";
        private string _apiUsername = "";
        private string _apiPassword = "";

        [HttpGet, Route("/FormResponse")]
        public IActionResult ValidateToken(string secret)
        {
            ValidateSecret(secret);

            // There is no need to return anything if you do not want to.  Comply365 logs the body of your response however, so putting information here that helps
            // you troubleshoot isn't a bad idea.
            return Json(string.Empty);
        }

        [HttpPost, Route("/FormResponse")]
        public IActionResult AcceptData(BaseWebHook<FormResponse> webHookData)
        {
            // here's where you can take whatever action you desire using the web hook data that was submitted.

            // There is no need to return anything if you do not want to.  Comply365 logs the body of your response however, so putting information here that helps
            // you troubleshoot isn't a bad idea.  Example, if you store each form response in a database, you might want to return the row's Id/Uid.
            // For this example, we just return what was sent to us
            return Json(webHookData);
        }

        [HttpGet, Route("/PublishXmlComplete")]
        public IActionResult ValidatePublishXmlCompleteToken(string secret)
        {
            ValidateSecret(secret);

            // There is no need to return anything if you do not want to.  Comply365 logs the body of your response however, so putting information here that helps
            // you troubleshoot isn't a bad idea.
            return Json(string.Empty);
        }

        [HttpPost, Route("/PublishXmlComplete")]
        public IActionResult PublishXmlComplete(BaseWebHook<FormResponse> webHookData)
        {
            // here's where you can take whatever action you desire using the web hook data that was submitted.

            // There is no need to return anything if you do not want to.  Comply365 logs the body of your response however, so putting information here that helps
            // you troubleshoot isn't a bad idea.  Example, if you store each form response in a database, you might want to return the row's Id/Uid.
            // For this example, we just return what was sent to us
            return Json(webHookData);
        }

        private void ValidateSecret(string secret)
        {
            // we have to add the secret to a list of the GET parameters.
            var parametersDictionary = new Dictionary<string, string>
            {
                { "secret", secret }
            };

            // now we execute a GET request back to the Comply365 server so that they know we do indeed want to receive web hooks from the Comply365 servers.
            ExecuteGetRequest($"https://{_siteName}-api.comply365.net", "api/WHK/v1/WebHooks/VerificationResponse", parametersDictionary);
        }

        private void ExecuteGetRequest(string baseUrl, string path, Dictionary<string, string> parametersDictionary)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(path, Method.GET);

            // here we're just adding the GET/URL parameters
            foreach(var parameter in parametersDictionary)
            {
                request.AddParameter(parameter.Key, parameter.Value);
            }
            
            var response = client.Execute(request);
        }
    }    
}