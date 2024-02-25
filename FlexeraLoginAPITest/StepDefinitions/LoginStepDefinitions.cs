using RestSharp;
using Shouldly;

namespace FlexeraLoginAPITest.StepDefinitions
{
    [Binding]
    public sealed class LoginStepDefinitions
    {
        private readonly RestClient restClient;
        private ScenarioContext scenarioContext;
        public string BaseUri = "https://secure.flexera.com/api/v1";
        public LoginStepDefinitions(ScenarioContext context)
        {
            scenarioContext = context;
            restClient = new RestClient(BaseUri);
        }

        [Given(@"user email is (.*)")]
        public void GivenUserEmailIs_Com(string email)
        {
            scenarioContext["email"] = email;
        }

        [Given(@"user password is (.*)")]
        public void GivenUserPasswordIs(string password)
        {
            scenarioContext["password"] = password;
        }

        [When(@"post the credentials to login")]
        public async Task WhenPostTheCredentialsToLogin()
        {
            var request = new RestRequest("/authn", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br, zstd");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { password = scenarioContext.Get<string>("password"), username = scenarioContext.Get<string>("email") });

            var response = await restClient.ExecuteAsync(request);
            scenarioContext.Set<RestResponse>(response, "response");
        }

        [Then(@"The loging result shows (.*)")]
        public void ThenTheLogingResultShows(string statusCode)
        {
            scenarioContext.Get<RestResponse>("response").StatusDescription.ShouldBe(statusCode);
        }

    }
}
