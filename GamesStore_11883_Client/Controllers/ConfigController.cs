using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace GamesStore_11883_Client.Controllers
{
    public class ConfigController
    {
        // PROD env url
        public readonly string baseurl = "http://ec2-54-193-21-173.us-west-1.compute.amazonaws.com";

        // DEV env url
        // public readonly string baseurl = "http://localhost:32768";
    }
}