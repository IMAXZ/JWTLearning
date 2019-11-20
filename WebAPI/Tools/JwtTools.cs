using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace WebAPI.Tools
{
    public class JwtTools
    {
        public const string Key = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
        public static string Encoding(Dictionary<string,object>payload,string secret)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            payload.Add("timeout", DateTime.Now.AddHours(2));
            return encoder.Encode(payload, secret);
        }

        public static Dictionary<string, object> Decode(string token, string secret)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
            try
            {
                var json =  decoder.Decode(token, secret, verify: true);
                var rs = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                return rs;
            }
            catch (TokenExpiredException)
            {
                Console.WriteLine("Token has expired");
                throw;
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
                throw;
            }
        }

        public static string ValideLogined(HttpRequestHeaders heads)
        {
            
            if (heads.GetValues("token") == null || !heads.GetValues("token").Any())
                throw new Exception("请登录！");
            return Decode(heads.GetValues("token").FirstOrDefault(), Key)[""].ToString();
        }
    }
}