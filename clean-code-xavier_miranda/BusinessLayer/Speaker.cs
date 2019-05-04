using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class Speaker
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? Experience { get; set; }
        public bool HasBlog { get; set; }
        public string BlogURL { get; set; }
        public WebBrowser Browser { get; set; }
        public List<string> Certifications { get; set; }
        public string Employer { get; set; }
        public int RegistrationFee { get; set; }
        public List<BusinessLayer.Session> Sessions { get; set; }


        public int? Register(IRepository repository)
        {
            if (!ValidationSpeaker()) throw new SpeakerDoesntMeetRequirementsException("Speaker doesn't meet our abitrary and capricious standards.");

            int? speakerId = null;
            RegistrationFee = CalculationFee();

            try
            {
                speakerId = repository.SaveSpeaker(this);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error: " + e.Message);
            }

            return speakerId;
        }

        public bool ValidationSpeaker()
        {
            if (string.IsNullOrWhiteSpace(FirstName)) throw new ArgumentNullException("First Name is required");
            if (string.IsNullOrWhiteSpace(LastName)) throw new ArgumentNullException("Last name is required.");
            if (string.IsNullOrWhiteSpace(Email)) throw new ArgumentNullException("Email is required.");
            if (!ValidationDomains()) throw new SpeakerDoesntMeetRequirementsException("Speaker doesn't meet our abitrary and capricious standards.");
            if (Sessions.Count() == 0) throw new ArgumentException("Can't register speaker with no sessions to present.");
            if (!ValidationSessions()) throw new NoSessionsApprovedException("No sessions approved.");

            return true;
        } 

        public int CalculationFee()
        {
            int valueFee = 0;
            switch (Experience)
            {
                case 1:
                    valueFee = 500;
                    break;
                case 2:
                case 3:
                    valueFee = 250;
                    break;
                case 4:
                case 5:
                    valueFee = 100;
                    break;
                case 6:
                case 7:
                case 8:
                case 9:
                    valueFee = 50;
                    break;
            }
            return valueFee;
        }

        public bool ValidationSessions()
        {
            bool approved = false;
            var oldTechs = new List<string>() { "Cobol", "Punch Cards", "Commodore", "VBScript" };

            foreach (var session in Sessions)
            {
                foreach (var item in oldTechs)
                {
                    session.Approved = !(session.Title.Contains(item) || session.Description.Contains(item));
                    if (session.Approved) approved = true;
                }
            }
            return approved;
        }

        public bool ValidationDomains()
        {
            var domains = new List<string>() { "aol.com", "hotmail.com", "prodigy.com", "CompuServe.com" };
            var employers = new List<string>() { "Microsoft", "Google", "Fog Creek Software", "37Signals" };
            if ((Experience > 10 || HasBlog || Certifications.Count() > 3 || employers.Contains(Employer)))
                return true;
            else
            {
                string emailDomain = Email.Split('@').Last();
                return !(domains.Contains(emailDomain) && ((Browser.Name == WebBrowser.BrowserName.InternetExplorer && Browser.MajorVersion < 9)));
            }
        }


        #region Custom Exceptions
        public class SpeakerDoesntMeetRequirementsException : Exception
        {
            public SpeakerDoesntMeetRequirementsException(string message)
                : base(message)
            {
            }

            public SpeakerDoesntMeetRequirementsException(string format, params object[] args)
                : base(string.Format(format, args)) { }
        }

        public class NoSessionsApprovedException : Exception
        {
            public NoSessionsApprovedException(string message)
                : base(message)
            {
            }
        }
        #endregion
    }
}