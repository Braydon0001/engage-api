using System.Text.Json.Serialization;

namespace Engage.Application.Auth.Entities;

public class ClerkUser
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("object")]
    public string Object { get; set; }
    [JsonPropertyName("external_id")]
    public string ExternalId { get; set; }
    [JsonPropertyName("primary_email_address_id")]
    public string PrimaryEmailAddressId { get; set; }
    [JsonPropertyName("primary_phone_number_id")]
    public string PrimaryPhoneNumberId { get; set; }
    [JsonPropertyName("primary_web3_wallet_id")]
    public string PrimaryWeb3WalletId { get; set; }
    [JsonPropertyName("username")]
    public string Username { get; set; }
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
    [JsonPropertyName("profile_image_url")]
    public string ProfileImageUrl { get; set; }
    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; }
    [JsonPropertyName("has_image")]
    public bool HasImage { get; set; }
    [JsonPropertyName("public_metadata")]
    public UserPublicMetadata PublicMetadata { get; set; }
    [JsonPropertyName("private_metadata")]
    public Dictionary<string, object> PrivateMetadata { get; set; }
    [JsonPropertyName("unsafe_metadata")]
    public Dictionary<string, object> UnsafeMetadata { get; set; }
    [JsonPropertyName("gender")]
    public string Gender { get; set; }
    [JsonPropertyName("birthday")]
    public string Birthday { get; set; }

    [JsonPropertyName("email_addresses")]
    public List<EmailAddress> EmailAddresses { get; set; }
    //[JsonPropertyName("phone_numbers")]
    //public List<PhoneNumber> PhoneNumbers { get; set; }
    //[JsonPropertyName("web3_wallets")]
    //public List<Web3Wallet> Web3Wallets { get; set; }

    [JsonPropertyName("password_enabled")]
    public bool? PasswordEnabled { get; set; }
    [JsonPropertyName("two_factor_enabled")]
    public bool? TwoFactorEnabled { get; set; }
    [JsonPropertyName("totp_enabled")]
    public bool? TotpEnabled { get; set; }
    [JsonPropertyName("backup_code_enabled")]
    public bool? BackupCodeEnabled { get; set; }

    //[JsonPropertyName("external_accounts")]
    //public List<object?>? ExternalAccounts { get; set; }
    //[JsonPropertyName("saml_accounts")]
    //public List<SamlAccount?>? SamlAccounts { get; set; }

    [JsonPropertyName("last_sign_in_at")]
    public long LastSignInAt { get; set; }
    [JsonPropertyName("banned")]
    public bool Banned { get; set; }
    [JsonPropertyName("locked")]
    public bool Locked { get; set; }

    //[JsonPropertyName("lockout_expires_in_seconds")]
    //public int LockoutExpiresInSeconds { get; set; }

    [JsonPropertyName("verification_attempts_remaining")]
    public int VerificationAttemptsRemaining { get; set; }
    [JsonPropertyName("updated_at")]
    public long UpdatedAt { get; set; }
    [JsonPropertyName("created_at")]
    public long CreatedAt { get; set; }
    [JsonPropertyName("delete_self_enabled")]
    public bool DeleteSelfEnabled { get; set; }
    [JsonPropertyName("create_organization_enabled")]
    public bool CreateOrganizationEnabled { get; set; }
    [JsonPropertyName("last_active_at")]
    public long? LastActiveAt { get; set; }
}

public class EmailAddress
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("object")]
    public string Object { get; set; }
    [JsonPropertyName("email_address")]
    public string EmailAddressValue { get; set; }
    [JsonPropertyName("reserved")]
    public bool Reserved { get; set; }
    //[JsonPropertyName("verification")]
    //public Verification Verification { get; set; }
    //[JsonPropertyName("linked_to")]
    //public List<LinkedTo> LinkedTo { get; set; }
}

public class PhoneNumber
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("object")]
    public string Object { get; set; }
    [JsonPropertyName("phone_number")]
    public string PhoneNumberValue { get; set; }
    [JsonPropertyName("reserved_for_second_factor")]
    public bool ReservedForSecondFactor { get; set; }
    [JsonPropertyName("default_second_factor")]
    public bool DefaultSecondFactor { get; set; }
    [JsonPropertyName("reserved")]
    public bool Reserved { get; set; }
    [JsonPropertyName("verification")]
    public Verification Verification { get; set; }
    [JsonPropertyName("linked_to")]
    public List<LinkedTo> LinkedTo { get; set; }
    [JsonPropertyName("backup_codes")]
    public List<string> BackupCodes { get; set; }
}

public class Web3Wallet
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("object")]
    public string Object { get; set; }
    [JsonPropertyName("web3_wallet")]
    public string Web3WalletValue { get; set; }
    [JsonPropertyName("verification")]
    public Verification Verification { get; set; }
}

public class LinkedTo
{
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("id")]
    public string Id { get; set; }
}

public class Verification
{
    [JsonPropertyName("status")]
    public string Status { get; set; }
    [JsonPropertyName("strategy")]
    public string Strategy { get; set; }
    [JsonPropertyName("attempts")]
    public int Attempts { get; set; }
    [JsonPropertyName("expire_at")]
    public long ExpireAt { get; set; }
}

public class SamlAccount
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("object")]
    public string Object { get; set; }
    [JsonPropertyName("provider")]
    public string Provider { get; set; }
    [JsonPropertyName("active")]
    public bool Active { get; set; }
    [JsonPropertyName("email_address")]
    public string EmailAddressValue { get; set; }
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
    [JsonPropertyName("provider_user_id")]
    public string ProviderUserId { get; set; }
    [JsonPropertyName("verification")]
    public Verification Verification { get; set; }
}