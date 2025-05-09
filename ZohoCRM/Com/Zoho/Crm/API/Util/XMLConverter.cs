﻿using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Com.Zoho.Crm.API.Util
{
    /// <summary>
    /// This class processes the API response object to the POJO object and POJO object to an XML object.
    /// </summary>
    public class XMLConverter : Converter
    {

        public XMLConverter(CommonAPIHandler commonAPIHandler): base(commonAPIHandler)
        {
        }

        public override object FormRequest(object requestObject, string pack, int? instanceNumber, JObject memberDetail)
        {
            throw new NotImplementedException();
        }

        public override void AppendToRequest(HttpWebRequest requestBase, object requestObject)
        {
            throw new NotImplementedException();
        }

        public override List<Object> GetWrappedResponse(object response, string pack)
        {
            return new List<Object>() { this.GetResponse(response, pack) };
        }

        public override object GetResponse(object response, string pack)
        {
            throw new NotImplementedException();
        }
    }
}
