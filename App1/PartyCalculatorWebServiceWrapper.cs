using System;
using System.Net;
using PartyCalculator.PartyWebServices;
using Exception = System.Exception;

namespace PartyCalculator
{
    public class PartyCalculatorWebServiceWrapper
    {
        private static LocalPartyCalculatorService localPartyCalculatorService = new LocalPartyCalculatorService();
        public static IPartyCalculatorService PartyCalculatorService
        {
            get
            {
                var partyCalulatorService = new PartyCalculatorServiceClient();

                try
                {
                    WebRequest request =
                        WebRequest.Create(partyCalulatorService.Endpoint.Address.Uri);
                    var response = request.GetResponseAsync().Result;
                    return new RemotePartyCalculatorService();
                }
                catch (Exception)
                {
                    return localPartyCalculatorService;
                }
            }
        }
    }
}

