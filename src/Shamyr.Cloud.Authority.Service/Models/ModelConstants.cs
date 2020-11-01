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

    public const int _MaxGivenNameLength = 100;
    public const int _MinGivenNameLength = 0;

    public const int _MaxFamilyNameLength = 100;
    public const int _MinFamilyNameLength = 0;

    public const int _MaxClientNameLength = 60;
    public const int _MinClientNameLength = 2;

    public const int _MaxTemplateNameLength = 100;
    public const int _MinTemplateNameLength = 0;

    public const int _MaxTemplateSubjectLength = 100;
    public const int _MinTemplateSubjectLength = 0;

    public const int _MaxTemplateBodyLength = 30_000;
    public const int _MinTemplateBodyLength = 0;
  }
}
