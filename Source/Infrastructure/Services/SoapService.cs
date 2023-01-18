using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.ServiceModel;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Polly;
using Domain.Enums;

namespace Infrastructure.Services;

public class SoapService
{
    private readonly HttpClient _httpClient;

    public SoapService()
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        HttpClientHandler handler = new()
        {
            ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
        };
        _httpClient = new HttpClient(handler);
    }

    protected T SendSOAPRequest<T>(string url, string action, object request, Dictionary<string, string> headers = null, AuthType authType = AuthType.Basic, string authValue = null, string contentType = "application/soap+xml", int maxRetries = 3, int retryDelay = 1000, int timeout = 10000, bool followRedirects = true)
    {
        try
        {
            // Create a retry policy with a specific number of retries and a specific delay between retries
            var retryPolicy = Policy.Handle<WebException>().WaitAndRetryAsync(maxRetries, retryAttempt => TimeSpan.FromMilliseconds(retryDelay));

            // Set timeout and redirect options
            _httpClient.Timeout = TimeSpan.FromMilliseconds(timeout);
            _httpClient.DefaultRequestHeaders.ExpectContinue = false;
            _httpClient.DefaultRequestHeaders.ConnectionClose = !followRedirects;

            EndpointAddress endpoint = new(url);
            using (var client = new ChannelFactory<T>(new BasicHttpBinding(), endpoint))
            {
                var channel = client.CreateChannel();
                using (new OperationContextScope((IContextChannel)channel))
                {
                    var requestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(url)
                    };
                    var soapMessage = new HttpRequestMessage(HttpMethod.Post, url);
                    var serializer = new DataContractSerializer(request.GetType());
                    switch (contentType)
                    {
                        case "application/json":
                            soapMessage.Content = new ObjectContent(request.GetType(), request, new JsonMediaTypeFormatter());
                            break;
                        case "multipart/form-data":
                            // Create a multipart form data content
                            var multipartContent = new MultipartFormDataContent();
                            // Add the request object to the content
                            var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8);
                            multipartContent.Add(jsonContent, "request");
                            soapMessage.Content = multipartContent;
                            break;
                        default:
                            soapMessage.Content = new ObjectContent(request.GetType(), request, new XmlMediaTypeFormatter(), contentType);
                            break;
                    }
                    requestMessage.Headers.Add("SOAPAction", action);
                    if (headers != null)
                    {
                        foreach (var header in headers)
                        {
                            requestMessage.Headers.Add(header.Key, header.Value);
                        }
                    }
                    if (!string.IsNullOrEmpty(authValue))
                    {
                        switch (authType)
                        {
                            case AuthType.Basic:
                                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authValue);
                                break;
                            case AuthType.JWT:
                                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authValue);
                                break;
                        }
                    }

                    // Execute the request with the retry policy
                    var response = retryPolicy.ExecuteAsync(() => _httpClient.SendAsync(requestMessage)).Result;

                    // Check for rate limiting
                    var rateLimit = response.Headers.GetValues("X-Rate-Limit-Limit").FirstOrDefault();
                    var rateRemaining = response.Headers.GetValues("X-Rate-Limit-Remaining").FirstOrDefault();
                    if (rateLimit != null && rateRemaining != null)
                    {
                        // Handle rate limiting
                    }

                    // Check for pagination
                    var pagination = response.Headers.GetValues("Link").FirstOrDefault();
                    if (!string.IsNullOrEmpty(pagination))
                    {
                        // Handle pagination
                    }

                    // Check for response status code
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsAsync<T>().Result;
                    }
                    else
                    {
                        // Throw an exception or return a specific error message
                        var error = response.Content.ReadAsStringAsync().Result;
                        throw new Exception(error);
                    }
                }
            }
        }
        catch (FaultException ex)
        {
            // Handle SOAP fault exception
            throw ex;
        }
        catch (WebException ex)
        {
            // Handle web exception
            throw ex;
        }
        catch (Exception)
        {
            // Handle other exception
            throw;
        }
    }
}
