using System;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using Machine.Specifications;
using Microsoft.Owin.Hosting;

namespace SandraFx.Tests.Integration
{
    public class when_making_get_request_to_home_without_parameters
    {
        static HttpClient httpClient;
        static Uri baseUri;
        static HttpResponseMessage httpResponse;

        Cleanup after = () =>
        {
            using (httpClient)
            {
            }
            using (server)
            {
                
            }
        };

        Establish context = () =>
        {
            baseUri = new Uri("http://localhost:9090");
            server = WebApp.Start<Startup>(url: baseUri.ToString());

            httpClient = new HttpClient();
        };

        Because of =
            () => { httpResponse = httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, baseUri)).Result; };

        It should_return_ok = () => httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        It should_contain_hello_world_in_body = () => httpResponse.Content.ReadAsStringAsync().Result.Should().Be("Hello world");

        static IDisposable server;
    }
}