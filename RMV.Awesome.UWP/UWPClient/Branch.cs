﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.9.7.0
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;
using Newtonsoft.Json.Linq;
using RMV.Awesome.UWP;
using RMV.Awesome.UWP.Models;

namespace RMV.Awesome.UWP
{
    internal partial class Branch : IServiceOperations<UWPClient>, IBranch
    {
        /// <summary>
        /// Initializes a new instance of the Branch class.
        /// </summary>
        /// <param name='client'>
        /// Reference to the service client.
        /// </param>
        internal Branch(UWPClient client)
        {
            this._client = client;
        }
        
        private UWPClient _client;
        
        /// <summary>
        /// Gets a reference to the RMV.Awesome.UWP.UWPClient.
        /// </summary>
        public UWPClient Client
        {
            get { return this._client; }
        }
        
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        public async Task<HttpOperationResponse<IList<RMVBranch>>> GetBranchListWithOperationResponseAsync(CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // Tracing
            bool shouldTrace = ServiceClientTracing.IsEnabled;
            string invocationId = null;
            if (shouldTrace)
            {
                invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
                ServiceClientTracing.Enter(invocationId, this, "GetBranchListAsync", tracingParameters);
            }
            
            // Construct URL
            string url = "";
            url = url + "/api/branch";
            string baseUrl = this.Client.BaseUri.AbsoluteUri;
            // Trim '/' character from the end of baseUrl and beginning of url.
            if (baseUrl[baseUrl.Length - 1] == '/')
            {
                baseUrl = baseUrl.Substring(0, baseUrl.Length - 1);
            }
            if (url[0] == '/')
            {
                url = url.Substring(1);
            }
            url = baseUrl + "/" + url;
            url = url.Replace(" ", "%20");
            
            // Create HTTP transport objects
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.Method = HttpMethod.Get;
            httpRequest.RequestUri = new Uri(url);
            
            // Set Credentials
            if (this.Client.Credentials != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await this.Client.Credentials.ProcessHttpRequestAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            }
            
            // Send Request
            if (shouldTrace)
            {
                ServiceClientTracing.SendRequest(invocationId, httpRequest);
            }
            cancellationToken.ThrowIfCancellationRequested();
            HttpResponseMessage httpResponse = await this.Client.HttpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            if (shouldTrace)
            {
                ServiceClientTracing.ReceiveResponse(invocationId, httpResponse);
            }
            HttpStatusCode statusCode = httpResponse.StatusCode;
            cancellationToken.ThrowIfCancellationRequested();
            string responseContent = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (statusCode != HttpStatusCode.OK)
            {
                HttpOperationException<object> ex = new HttpOperationException<object>();
                ex.Request = httpRequest;
                ex.Response = httpResponse;
                ex.Body = null;
                if (shouldTrace)
                {
                    ServiceClientTracing.Error(invocationId, ex);
                }
                throw ex;
            }
            
            // Create Result
            HttpOperationResponse<IList<RMVBranch>> result = new HttpOperationResponse<IList<RMVBranch>>();
            result.Request = httpRequest;
            result.Response = httpResponse;
            
            // Deserialize Response
            if (statusCode == HttpStatusCode.OK)
            {
                IList<RMVBranch> resultModel = new List<RMVBranch>();
                JToken responseDoc = null;
                if (string.IsNullOrEmpty(responseContent) == false)
                {
                    responseDoc = JToken.Parse(responseContent);
                }
                if (responseDoc != null)
                {
                    resultModel = RMVBranchCollection.DeserializeJson(responseDoc);
                }
                result.Body = resultModel;
            }
            
            if (shouldTrace)
            {
                ServiceClientTracing.Exit(invocationId, result);
            }
            return result;
        }
        
        /// <param name='lat'>
        /// Required.
        /// </param>
        /// <param name='lng'>
        /// Required.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        public async Task<HttpOperationResponse<IList<RMVBranch>>> GetBranchListDistanceWithOperationResponseAsync(double lat, double lng, CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // Tracing
            bool shouldTrace = ServiceClientTracing.IsEnabled;
            string invocationId = null;
            if (shouldTrace)
            {
                invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
                tracingParameters.Add("lat", lat);
                tracingParameters.Add("lng", lng);
                ServiceClientTracing.Enter(invocationId, this, "GetBranchListDistanceAsync", tracingParameters);
            }
            
            // Construct URL
            string url = "";
            url = url + "/api/branch/loc";
            List<string> queryParameters = new List<string>();
            queryParameters.Add("lat=" + Uri.EscapeDataString(lat.ToString()));
            queryParameters.Add("lng=" + Uri.EscapeDataString(lng.ToString()));
            if (queryParameters.Count > 0)
            {
                url = url + "?" + string.Join("&", queryParameters);
            }
            string baseUrl = this.Client.BaseUri.AbsoluteUri;
            // Trim '/' character from the end of baseUrl and beginning of url.
            if (baseUrl[baseUrl.Length - 1] == '/')
            {
                baseUrl = baseUrl.Substring(0, baseUrl.Length - 1);
            }
            if (url[0] == '/')
            {
                url = url.Substring(1);
            }
            url = baseUrl + "/" + url;
            url = url.Replace(" ", "%20");
            
            // Create HTTP transport objects
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.Method = HttpMethod.Get;
            httpRequest.RequestUri = new Uri(url);
            
            // Set Credentials
            if (this.Client.Credentials != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await this.Client.Credentials.ProcessHttpRequestAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            }
            
            // Send Request
            if (shouldTrace)
            {
                ServiceClientTracing.SendRequest(invocationId, httpRequest);
            }
            cancellationToken.ThrowIfCancellationRequested();
            HttpResponseMessage httpResponse = await this.Client.HttpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            if (shouldTrace)
            {
                ServiceClientTracing.ReceiveResponse(invocationId, httpResponse);
            }
            HttpStatusCode statusCode = httpResponse.StatusCode;
            cancellationToken.ThrowIfCancellationRequested();
            string responseContent = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (statusCode != HttpStatusCode.OK)
            {
                HttpOperationException<object> ex = new HttpOperationException<object>();
                ex.Request = httpRequest;
                ex.Response = httpResponse;
                ex.Body = null;
                if (shouldTrace)
                {
                    ServiceClientTracing.Error(invocationId, ex);
                }
                throw ex;
            }
            
            // Create Result
            HttpOperationResponse<IList<RMVBranch>> result = new HttpOperationResponse<IList<RMVBranch>>();
            result.Request = httpRequest;
            result.Response = httpResponse;
            
            // Deserialize Response
            if (statusCode == HttpStatusCode.OK)
            {
                IList<RMVBranch> resultModel = new List<RMVBranch>();
                JToken responseDoc = null;
                if (string.IsNullOrEmpty(responseContent) == false)
                {
                    responseDoc = JToken.Parse(responseContent);
                }
                if (responseDoc != null)
                {
                    resultModel = RMVBranchCollection.DeserializeJson(responseDoc);
                }
                result.Body = resultModel;
            }
            
            if (shouldTrace)
            {
                ServiceClientTracing.Exit(invocationId, result);
            }
            return result;
        }
        
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        public async Task<HttpOperationResponse<bool>> GetRefreshTimesWithOperationResponseAsync(CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // Tracing
            bool shouldTrace = ServiceClientTracing.IsEnabled;
            string invocationId = null;
            if (shouldTrace)
            {
                invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
                ServiceClientTracing.Enter(invocationId, this, "GetRefreshTimesAsync", tracingParameters);
            }
            
            // Construct URL
            string url = "";
            url = url + "/api/branch/refresh";
            string baseUrl = this.Client.BaseUri.AbsoluteUri;
            // Trim '/' character from the end of baseUrl and beginning of url.
            if (baseUrl[baseUrl.Length - 1] == '/')
            {
                baseUrl = baseUrl.Substring(0, baseUrl.Length - 1);
            }
            if (url[0] == '/')
            {
                url = url.Substring(1);
            }
            url = baseUrl + "/" + url;
            url = url.Replace(" ", "%20");
            
            // Create HTTP transport objects
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.Method = HttpMethod.Get;
            httpRequest.RequestUri = new Uri(url);
            
            // Set Credentials
            if (this.Client.Credentials != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await this.Client.Credentials.ProcessHttpRequestAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            }
            
            // Send Request
            if (shouldTrace)
            {
                ServiceClientTracing.SendRequest(invocationId, httpRequest);
            }
            cancellationToken.ThrowIfCancellationRequested();
            HttpResponseMessage httpResponse = await this.Client.HttpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            if (shouldTrace)
            {
                ServiceClientTracing.ReceiveResponse(invocationId, httpResponse);
            }
            HttpStatusCode statusCode = httpResponse.StatusCode;
            cancellationToken.ThrowIfCancellationRequested();
            string responseContent = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (statusCode != HttpStatusCode.OK)
            {
                HttpOperationException<object> ex = new HttpOperationException<object>();
                ex.Request = httpRequest;
                ex.Response = httpResponse;
                ex.Body = null;
                if (shouldTrace)
                {
                    ServiceClientTracing.Error(invocationId, ex);
                }
                throw ex;
            }
            
            // Create Result
            HttpOperationResponse<bool> result = new HttpOperationResponse<bool>();
            result.Request = httpRequest;
            result.Response = httpResponse;
            
            // Deserialize Response
            if (statusCode == HttpStatusCode.OK)
            {
                bool resultModel = default(bool);
                JToken responseDoc = null;
                if (string.IsNullOrEmpty(responseContent) == false)
                {
                    responseDoc = JToken.Parse(responseContent);
                }
                if (responseDoc != null)
                {
                    resultModel = ((bool)responseDoc);
                }
                result.Body = resultModel;
            }
            
            if (shouldTrace)
            {
                ServiceClientTracing.Exit(invocationId, result);
            }
            return result;
        }
    }
}
