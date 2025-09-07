using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace NotesService.Data
{
    public class EncryptedConverter : EncryptedConverter<string>
    {
        public EncryptedConverter(IDataProtectionProvider dataProtectionProvider) : base(dataProtectionProvider) { }
    }

    public class EncryptedConverter<TProperty> : ValueConverter<TProperty, byte[]>
    {
    private static readonly JsonSerializerOptions? options;

    public EncryptedConverter(IDataProtectionProvider dataProtectionProvider) :
        base(
        x => dataProtectionProvider.CreateProtector("encryptedProperty").Protect(JsonSerializer.SerializeToUtf8Bytes(x, options)),
        x => JsonSerializer.Deserialize<TProperty>(dataProtectionProvider.CreateProtector("encryptedProperty").Unprotect(x), options),
        default
        )
    { }
    }
}