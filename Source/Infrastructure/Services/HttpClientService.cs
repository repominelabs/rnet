using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.ServiceModel;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Polly;
using Domain.Enums;
using Domain.Constants;

namespace Infrastructure.Services;

/// <summary>
/// Class - Http Client Service
/// </summary>
public class HttpClientService
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Constructor - Http Client Service
    /// </summary>
    public HttpClientService()
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        HttpClientHandler handler = new()
        {
            ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
        };
        _httpClient = new HttpClient(handler);
    }

    /// <summary>
    /// Method - Send SOAP Request
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="action"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <param name="authType"></param>
    /// <param name="authValue"></param>
    /// <param name="contentType"></param>
    /// <param name="maxRetries"></param>
    /// <param name="retryDelay"></param>
    /// <param name="timeout"></param>
    /// <param name="followRedirects"></param>
    /// <returns></returns>
    public T SendSOAPRequest<T>(string url, string action, object request, Dictionary<string, string> headers = null, AuthType authType = AuthType.Basic, string authValue = null, string contentType = ContentTypes.APPLICATION_SOAP_XML, int maxRetries = 3, int retryDelay = 1000, int timeout = 10000, bool followRedirects = true)
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
            using (ChannelFactory<T> client = new(new BasicHttpBinding(), endpoint))
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
                        case ContentTypes.APPLICATION_JSON:
                            soapMessage.Content = new ObjectContent(request.GetType(), request, new JsonMediaTypeFormatter());
                            break;
                        case ContentTypes.MULTIPART_FORM_DATA:
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

    /// <summary>
    /// Method - Send SOAP Request Async
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="action"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <param name="authType"></param>
    /// <param name="authValue"></param>
    /// <param name="contentType"></param>
    /// <param name="maxRetries"></param>
    /// <param name="retryDelay"></param>
    /// <param name="timeout"></param>
    /// <param name="followRedirects"></param>
    /// <returns></returns>
    public async Task<T> SendSOAPRequestAsync<T>(string url, string action, object request, Dictionary<string, string> headers = null, AuthType authType = AuthType.Basic, string authValue = null, string contentType = ContentTypes.APPLICATION_SOAP_XML, int maxRetries = 3, int retryDelay = 1000, int timeout = 10000, bool followRedirects = true)
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
            using (ChannelFactory<T> client = new(new BasicHttpBinding(), endpoint))
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
                        case ContentTypes.APPLICATION_JSON:
                            soapMessage.Content = new ObjectContent(request.GetType(), request, new JsonMediaTypeFormatter());
                            break;
                        case ContentTypes.MULTIPART_FORM_DATA:
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
                    var response = await retryPolicy.ExecuteAsync(() => _httpClient.SendAsync(requestMessage));

                    // Check for rate limiting
                    var rateLimit = response.Headers.GetValues("X-Rate-Limit-Limit").FirstOrDefault();
                    var rateRemaining = response.Headers.GetValues("X-Rate-Limit-Remaining").FirstOrDefault();
                    if (rateLimit != null && rateRemaining != null)
                    {
                        // Handle rate limiting
                    }

                    // check for pagination
                    var pagination = response.Headers.GetValues("Link").FirstOrDefault();
                    if (!string.IsNullOrEmpty(pagination))
                    {
                        // Handle pagination
                    }

                    // Check for response status code
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<T>();
                    }
                    else
                    {
                        // Throw an exception or return a specific error message
                        var error = await response.Content.ReadAsStringAsync();
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

    /// <summary>
    /// Method - Send REST Request
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="method"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <param name="authType"></param>
    /// <param name="authValue"></param>
    /// <param name="contentType"></param>
    /// <param name="maxRetries"></param>
    /// <param name="retryDelay"></param>
    /// <param name="timeout"></param>
    /// <param name="followRedirects"></param>
    /// <returns></returns>
    protected T SendRESTRequest<T>(string url, HttpMethod method, object request = null, Dictionary<string, string> headers = null, AuthType authType = AuthType.Basic, string authValue = null, string contentType = ContentTypes.APPLICATION_JSON, int maxRetries = 3, int retryDelay = 1000, int timeout = 10000, bool followRedirects = true)
    {
        try
        {
            // Create a retry policy with a specific number of retries and a specific delay between retries
            var retryPolicy = Policy.Handle<WebException>().WaitAndRetryAsync(maxRetries, retryAttempt => TimeSpan.FromMilliseconds(retryDelay));

            // Set timeout and redirect options
            _httpClient.Timeout = TimeSpan.FromMilliseconds(timeout);
            _httpClient.DefaultRequestHeaders.ExpectContinue = false;
            _httpClient.DefaultRequestHeaders.ConnectionClose = !followRedirects;

            var requestMessage = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(url)
            };
            switch (contentType)
            {
                case ContentTypes.APPLICATION_JSON:
                    requestMessage.Content = new ObjectContent(request.GetType(), request, new JsonMediaTypeFormatter());
                    break;
                case ContentTypes.MULTIPART_FORM_DATA:
                    // Create a multipart form data
                    var multipartContent = new MultipartFormDataContent();
                    // Add the request object to the content
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8);
                    multipartContent.Add(jsonContent, "request");
                    requestMessage.Content = multipartContent;
                    break;
                default:
                    requestMessage.Content = new ObjectContent(request.GetType(), request, new XmlMediaTypeFormatter(), contentType);
                    break;
            }
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

    /// <summary>
    /// Method - Send REST Request Async
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="method"></param>
    /// <param name="request"></param>
    /// <param name="headers"></param>
    /// <param name="authType"></param>
    /// <param name="authValue"></param>
    /// <param name="contentType"></param>
    /// <param name="maxRetries"></param>
    /// <param name="retryDelay"></param>
    /// <param name="timeout"></param>
    /// <param name="followRedirects"></param>
    /// <returns></returns>
    protected async Task<T> SendRESTRequestAsync<T>(string url, HttpMethod method, object request = null, Dictionary<string, string> headers = null, AuthType authType = AuthType.Basic, string authValue = null, string contentType = ContentTypes.APPLICATION_JSON, int maxRetries = 3, int retryDelay = 1000, int timeout = 10000, bool followRedirects = true)
    {
        try
        {
            // Create a retry policy with a specific number of retries and a specific delay between retries
            var retryPolicy = Policy.Handle<WebException>().WaitAndRetryAsync(maxRetries, retryAttempt => TimeSpan.FromMilliseconds(retryDelay));

            // Set timeout and redirect options
            _httpClient.Timeout = TimeSpan.FromMilliseconds(timeout);
            _httpClient.DefaultRequestHeaders.ExpectContinue = false;
            _httpClient.DefaultRequestHeaders.ConnectionClose = !followRedirects;

            var requestMessage = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(url)
            };
            switch (contentType)
            {
                case ContentTypes.APPLICATION_JSON:
                    requestMessage.Content = new ObjectContent(request.GetType(), request, new JsonMediaTypeFormatter());
                    break;
                case ContentTypes.MULTIPART_FORM_DATA:
                    // Create a multipart form data
                    var multipartContent = new MultipartFormDataContent();
                    // Add the request object to the content
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8);
                    multipartContent.Add(jsonContent, "request");
                    requestMessage.Content = multipartContent;
                    break;
                default:
                    requestMessage.Content = new ObjectContent(request.GetType(), request, new XmlMediaTypeFormatter(), contentType);
                    break;
            }
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
            var response = await retryPolicy.ExecuteAsync(() => _httpClient.SendAsync(requestMessage));

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
                return await response.Content.ReadAsAsync<T>();
            }
            else
            {
                // Throw an exception or return a specific error message
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
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
