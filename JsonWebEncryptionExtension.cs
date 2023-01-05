namespace PCPPlugins.BusinessLayer.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.IdentityModel.JsonWebTokens;
    

    public class JsonWebEncryptionExtension
    {
        private string PublicKeySign { get; set; }
        private string PrivateKeyEncrypt { get; set; }

        public IDictionary<string, object> Claims { get; }
        public ClaimsIdentity ClaimsIdentity { get; }
        public Exception Exception { get; }
        public string Issuer { get; }
        public bool IsValid { get; }
        public IDictionary<string, object> PropertyBag { get; }
        public SecurityToken SecurityToken { get; }
        public CallContext TokenContext { get; }
        public string TokenType { get; }

        public JsonWebEncryptionExtension(string _publicKeySign, string _privateKeyEncrypt)
        {
            PublicKeySign = _publicKeySign;
            PrivateKeyEncrypt = _privateKeyEncrypt;
        }

        private static TokenValidationResult DecryptAndValidateJwe(string token, SecurityKey signingKey, SecurityKey encryptionKey)
        {
            var handler = new JsonWebTokenHandler();

            TokenValidationResult result = handler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidAudience = "api1",
                    ValidIssuer = "https://idp.example.com",

                    // public key for signing
                    IssuerSigningKey = signingKey,

                    // private key for encryption
                    TokenDecryptionKey = encryptionKey
                });

            return result;
        }
    }
}

//https://stackoverflow.com/questions/49328317/how-to-decrypt-jwe-source-encrypted-with-rsa1-5-a256cbc-hs512-in-c
//https://www.scottbrady91.com/c-sharp/json-web-encryption-jwe-in-dotnet-core
