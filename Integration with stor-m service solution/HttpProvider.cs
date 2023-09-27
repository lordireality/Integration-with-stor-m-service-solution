using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections;
using System.IO;


namespace StormIntegration
{
    public class HttpProvider
    {
        public Response MakeRequest(string url, RequestType requestType, List<QueryParams> queryParams, string queryBody)
        {
            var resps = new Response();
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = requestType.ToString();
            string bodyData = "";

            if (queryParams != null)
            {
                request.ContentType = "application/x-www-form-urlencoded";
                string[] param = new string[0];
                queryParams.ForEach(c => param.Append(c.key + "=" + c.value));
                bodyData = string.Join("&", param);
            }
            if(queryBody!= null)
            {
                request.ContentType = "application/json";

                bodyData = queryBody;
            }
            
            var encoding = new ASCIIEncoding();
            byte[] bodyDataByte = encoding.GetBytes(bodyData);

            try
            {
                var requestStream = request.GetRequestStream();
            requestStream.Write(bodyDataByte, 0, bodyDataByte.Length);
            requestStream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            resps.respStatus = response.StatusCode.ToString();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                resps.success = true;
                using (StreamReader _streamReader = new StreamReader(response.GetResponseStream()))
                {
                    resps.respText = _streamReader.ReadToEnd();
                    _streamReader.Close();
                }
            }
            
            response.Close();
            }
            catch (System.Net.WebException ex)
            {
                
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var resp = ex.Response as HttpWebResponse;
                    if (resp != null) // && resp.StatusCode == HttpStatusCode.NotFound
                    {
                        resps.ex = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd().ToString();
                    }
                }
                resps.success = false;
            }
            return resps;


        }

        /// <summary>
        /// Класс для ответа 
        /// </summary>
        public class Response
        {
            public string respText;
            public string respStatus;
            public bool success;
            public string ex;
        }

        
        public class QueryParams
        {
            public string key;
            public string value;
            public QueryParams(string key, string value)
            {
                this.key = key;
                this.value = value;
            }
        }


    }




}
