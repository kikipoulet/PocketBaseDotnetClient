using System.Collections.Generic;

namespace PocketBaseDotnetClient;

    public class AuthAlert
    {
        public bool enabled { get; set; }
        public EmailTemplate emailTemplate { get; set; }
    }

    public class AuthToken
    {
        public int duration { get; set; }
    }

    public class ConfirmEmailChangeTemplate
    {
        public string subject { get; set; }
        public string body { get; set; }
    }

    public class EmailChangeToken
    {
        public int duration { get; set; }
    }

    public class EmailTemplate
    {
        public string subject { get; set; }
        public string body { get; set; }
    }

    public class Field
    {
        public string autogeneratePattern { get; set; }
        public bool hidden { get; set; }
        public string id { get; set; }
        public int max { get; set; }
        public int min { get; set; }
        public string name { get; set; }
        public string pattern { get; set; }
        public bool presentable { get; set; }
        public bool primaryKey { get; set; }
        public bool required { get; set; }
        public bool system { get; set; }
        public string type { get; set; }
        public bool? onCreate { get; set; }
        public bool? onUpdate { get; set; }
        public int? cost { get; set; }
        public object exceptDomains { get; set; }
        public object onlyDomains { get; set; }
        public int? maxSelect { get; set; }
        public int? maxSize { get; set; }
        public List<string> mimeTypes { get; set; }
        public bool? @protected { get; set; }
        public object thumbs { get; set; }
        public bool? convertURLs { get; set; }
        public bool? cascadeDelete { get; set; }
        public string collectionId { get; set; }
        public int? minSelect { get; set; }
    }

    public class FileToken
    {
        public int duration { get; set; }
    }

    public class CollectionSchema
    {
        public string id { get; set; }
        public string listRule { get; set; }
        public string viewRule { get; set; }
        public string createRule { get; set; }
        public string updateRule { get; set; }
        public string deleteRule { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public List<Field> fields { get; set; }
        public List<string> indexes { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
        public bool system { get; set; }
        public string authRule { get; set; }
        public object manageRule { get; set; }
        public AuthAlert authAlert { get; set; }
        public Oauth2 oauth2 { get; set; }
        public PasswordAuth passwordAuth { get; set; }
        public Mfa mfa { get; set; }
        public Otp otp { get; set; }
        public AuthToken authToken { get; set; }
        public PasswordResetToken passwordResetToken { get; set; }
        public EmailChangeToken emailChangeToken { get; set; }
        public VerificationToken verificationToken { get; set; }
        public FileToken fileToken { get; set; }
        public VerificationTemplate verificationTemplate { get; set; }
        public ResetPasswordTemplate resetPasswordTemplate { get; set; }
        public ConfirmEmailChangeTemplate confirmEmailChangeTemplate { get; set; }
    }

    public class MappedFields
    {
        public string id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string avatarURL { get; set; }
    }

    public class Mfa
    {
        public bool enabled { get; set; }
        public int duration { get; set; }
        public string rule { get; set; }
    }

    public class Oauth2
    {
        public List<object> providers { get; set; }
        public MappedFields mappedFields { get; set; }
        public bool enabled { get; set; }
    }

    public class Otp
    {
        public bool enabled { get; set; }
        public int duration { get; set; }
        public int length { get; set; }
        public EmailTemplate emailTemplate { get; set; }
    }

    public class PasswordAuth
    {
        public bool enabled { get; set; }
        public List<string> identityFields { get; set; }
    }

    public class PasswordResetToken
    {
        public int duration { get; set; }
    }

    public class ResetPasswordTemplate
    {
        public string subject { get; set; }
        public string body { get; set; }
    }

    public class ListCollectionResult
    {
        public List<CollectionSchema> items { get; set; }
        public int page { get; set; }
        public int perPage { get; set; }
        public int totalItems { get; set; }
        public int totalPages { get; set; }
    }

    public class VerificationTemplate
    {
        public string subject { get; set; }
        public string body { get; set; }
    }

    public class VerificationToken
    {
        public int duration { get; set; }
    }