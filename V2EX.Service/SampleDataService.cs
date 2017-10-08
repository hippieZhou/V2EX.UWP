using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V2EX.Models;

namespace V2EX.Service
{
    public static class SampleDataService
    {
        public static IEnumerable<EmailModel> GetEmails()
        {
            var emails = new List<EmailModel>
            {
                new EmailModel { From = "Steve Johnson", Subject = "Lunch Tomorrow", Body = "Are you available for lunch tomorrow? A client would like to discuss a project with you." },
                new EmailModel { From = "Becky Davidson", Subject = "Kids game", Body = "Don't forget the kids have their soccer game this Friday. We have to supply end of game snacks." },
                new EmailModel { From = "OneDrive", Subject = "Check out your event recap", Body = "Your new album.\r\nYou uploaded some photos to yuor OneDrive and automatically created an album for you." },
                new EmailModel { From = "Twitter", Subject = "Follow randomPerson, APersonYouMightKnow", Body = "Here are some people we think you might like to follow:\r\n.@randomPerson\r\nAPersonYouMightKnow" },
            };
            return emails;
        }
    }
}
