using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;

namespace Utility
{ 
    public class JwtHelper
    {

        //私钥  
        public const string secret = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNAmD7RTE2drj6hf3oZjJpMPZUQ1Qjb5H3K3PNwIDAQAB";
        
        /// <summary>
        /// <summary>
        /// 生成JwtToken
        /// </summary>
        /// <param name="payload">不敏感的用户数据</param>
        /// <returns></returns>
        public static string SetJwtEncode(string username,int expiresMinutes)
        {

            //格式如下
            var payload = new Dictionary<string, object>
            {
                { "username",username },
                { "exp ", expiresMinutes },
                { "domain", "" }
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, secret); 
            return token;
        }
       
        /// <summary>
        /// 根据jwtToken  获取实体
        /// </summary>
        /// <param name="token">jwtToken</param>
        /// <returns></returns>
        public static IDictionary<string,object> GetJwtDecode(string token)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
            var dicInfo = decoder.DecodeToObject(token, secret, verify: true);//token为之前生成的字符串
            return dicInfo;
        }
    } 
}
