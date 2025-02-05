﻿using Flurl;
using Flurl.Http;
using FunctionMonkey.Tests.Integration.Http.Helpers;
using System.Threading.Tasks;
using Xunit;

namespace FunctionMonkey.Tests.Integration.Http
{
    public class HttpFunctionWithSecurityPropertyShould : AbstractHttpFunctionTest
    {
        private const int Value = 92316;

        private const string Message = "this is some text to be echoed";

        private void ValidateEchoedResponse(SimpleResponse response)
        {
            Assert.Equal(0, response.Value); // the posted value will be ignored and so the response will be the default
            Assert.Equal(Message, response.Message);
        }

        [Fact]
        public async Task ReturnEchoedPayloadWithZeroValueForPOST()
        {
            SimpleResponse response = await Settings.Host
                .AppendPathSegment("securityProperty")
                .PostJsonAsync(new
                {
                    Value,
                    Message
                })
                .ReceiveJson<SimpleResponse>();

            ValidateEchoedResponse(response);
        }

        [Fact]
        public async Task ReturnEchoedPayloadWithZeroValueForGET()
        {
            SimpleResponse response = await Settings.Host
                .AppendPathSegment("securityProperty")
                .SetQueryParam("value", Value)
                .SetQueryParam("message", Message)
                .GetJsonAsync<SimpleResponse>();

            ValidateEchoedResponse(response);
        }

        /*[Fact]
        public async Task ReturnEchoedPayloadForGET()
        {
            SimpleResponse response = await Settings.Host
                .AppendPathSegment("verbs")
                .AppendPathSegment(Value)
                .SetQueryParam("message", Message)
                .GetJsonAsync<SimpleResponse>();

            ValidateEchoedResponse(response);
        }

        [Fact]
        public async Task ReturnEchoedPayloadForDELETE()
        {
            SimpleResponse response = await Settings.Host
                .AppendPathSegment("verbs")
                .AppendPathSegment(Value)
                .SetQueryParam("message", Message)
                .DeleteAsync()
                .ReceiveJson<SimpleResponse>();

            ValidateEchoedResponse(response);
        }

        [Fact]
        public async Task ReturnEchoedPayloadForPOST()
        {
            SimpleResponse response = await Settings.Host
                .AppendPathSegment("verbs")
                .PostJsonAsync(new
                {
                    Value,
                    Message
                })
                .ReceiveJson<SimpleResponse>();

            ValidateEchoedResponse(response);
        }
        
        [Fact]
        public async Task ReturnEchoedPayloadForByteArrayPOST()
        {
            ByteResponse response = await Settings.Host
                .AppendPathSegment("verbs")
                .AppendPathSegment("bytes")
                .PostJsonAsync(new
                {
                    Bytes = Encoding.UTF8.GetBytes(Message)
                })
                .ReceiveJson<ByteResponse>();

            string message = Encoding.UTF8.GetString(response.Bytes);
            Assert.Equal(message, Message);
        }

        [Fact]
        public async Task ReturnEchoedPayloadForPUT()
        {
            SimpleResponse response = await Settings.Host
                .AppendPathSegment("verbs")
                .PutJsonAsync(new
                {
                    Value,
                    Message
                })
                .ReceiveJson<SimpleResponse>();

            ValidateEchoedResponse(response);
        }

        [Fact]
        public async Task ReturnEchoedPayloadForPATCH()
        {
            SimpleResponse response = await Settings.Host
                .AppendPathSegment("verbs")
                .PatchJsonAsync(new
                {
                    Value,
                    Message
                })
                .ReceiveJson<SimpleResponse>();

            ValidateEchoedResponse(response);
        }

        [Fact]
        public async Task ReturnBadRequestOnTypeMismtachForPOST()
        {
            var response = await Settings.Host
                .AllowAnyHttpStatus()
                .AppendPathSegment("verbs")
                .PostJsonAsync(new
                {
                    Value="mismatchedType",
                    Message
                });

            Assert.Equal((int)HttpStatusCode.BadRequest, response.StatusCode);
            // ASP.Net Core returns a different error string for this
            //string responseString = await response.Content.ReadAsStringAsync();
            //Assert.Equal("Invalid type in message body at line 1 for path Value", responseString);
        }
        */
    }
}
