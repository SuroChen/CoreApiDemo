using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;
using System;
using System.Collections.Generic;

namespace CoreApiDemo.Utils
{
    public class JwtUtil
    {
        // 自定义秘钥，jwt 的生成和解析都需要使用
        private const string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
        //private const string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1aWQiOjUsInVzZXJuYW1lIjoic3VybyJ9.hNzP-rPgoLYdXoI-QfyIpTPhdzmP8bxBwrv8-Odra0w";
        /** token 过期时间: 10天 */
        private const int expiresDay = 10;

        public static string createToken(int id)
        {
            var payload = new Dictionary<string, object>{
                { "id", id }
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, secret);
            return token;
        }

        public static void verifyToken(string token)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                string json = decoder.Decode(token, secret, verify: true);
                Console.WriteLine(json);

                var payload = decoder.DecodeToObject<IDictionary<string, object>>(token);
                Console.WriteLine(payload["id"]);
            }
            catch (TokenExpiredException)
            {
                Console.WriteLine("令牌已过期");
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("令牌的签名无效");
            }
        }

        public static string createTokenByBuilder(int id)
        {
            var token = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(secret)
                .AddClaim("exp", DateTimeOffset.UtcNow.AddDays(expiresDay).ToUnixTimeSeconds())
                .AddClaim("id", id)
                .Build();
            return token;
        }

        public static string verifyTokenByBuilder(string token)
        {
            try
            {
                if(string.IsNullOrEmpty(token))
                {
                    return "4";
                }
                else
                {
                    var json = new JwtBuilder()
                    .WithSecret(secret)
                    .MustVerifySignature()
                    .Decode(token);
                    return "1";
                }
            }
            catch (TokenExpiredException)
            {
                return "2";
                //Console.WriteLine("令牌已过期");
            }
            catch (SignatureVerificationException)
            {
                return "3";
                //Console.WriteLine("令牌的签名无效");
            }
        }

        public static string getIdByToken(string token)
        {
            try
            {
                var payload = new JwtBuilder()
                    .WithSecret(secret)
                    .MustVerifySignature()
                    .Decode<IDictionary<string, object>>(token);
                return payload["id"].ToString();
            }
            catch (TokenExpiredException)
            {
                return "令牌已过期";
            }
            catch (SignatureVerificationException)
            {
                return "令牌的签名无效";
            }
        }
    }
}
