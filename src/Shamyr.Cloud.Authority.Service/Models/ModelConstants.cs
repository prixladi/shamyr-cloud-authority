namespace Shamyr.Cloud.Authority.Service.Models
{
  public static class ModelConstants
  {
    public const int _MaxPasswordLength = int.MaxValue;
    public const int _MinPasswordLength = 6;

    public const int _MaxSecretLength = int.MaxValue;
    public const int _MinSecretLength = 6;

    public const int _MaxUsernameLength = 60;
    public const int _MinUsernameLength = 6;

    public const int _MaxGivenNameLength = 0;
    public const int _MinGivenNameLength = 100;

    public const int _MaxFamilyNameLength = 0;
    public const int _MinFamilyNameLength = 100;

    public const int _MaxClientNameLength = 60;
    public const int _MinClientNameLength = 2;

    public const int _MaxTemplateNameLength = 0;
    public const int _MinTemplateNameLength = 100;

    public const int _MaxTemplateSubjectLength = 0;
    public const int _MinTemplateSubjectLength = 100;

    public const int _MaxTemplateBodyLength = 0;
    public const int _MinTemplateBodyLength = 30_000;
  }
}
