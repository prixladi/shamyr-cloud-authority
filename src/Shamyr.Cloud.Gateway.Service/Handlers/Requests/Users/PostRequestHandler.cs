using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Database;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Emails;
using Shamyr.Cloud.Gateway.Service.Extensions;
using Shamyr.Cloud.Gateway.Service.Models;
using Shamyr.Cloud.Gateway.Service.Models.Users;
using Shamyr.Cloud.Gateway.Service.Repositories.Users;
using Shamyr.Cloud.Gateway.Service.Requests.Users;
using Shamyr.Cloud.Gateway.Service.Services;
using Shamyr.Cloud.Services;
using Shamyr.Security;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Requests.Users
{
  public class PostRequestHandler: IRequestHandler<PostRequest, IdModel>
  {
    private const PermissionKind _DefaultUserPermission = PermissionKind.View;

    private readonly IUserRepository fUserRepository;
    private readonly ISecretService fSecretService;
    private readonly IEmailService fEmailService;

    public PostRequestHandler(
      IUserRepository userRepository,
      ISecretService secretService,
      IEmailService emailService)
    {
      fUserRepository = userRepository;
      fSecretService = secretService;
      fEmailService = emailService;
    }

    public async Task<IdModel> Handle(PostRequest request, CancellationToken cancellationToken)
    {
      if (await fUserRepository.ExistsByUsernameAsync(NormalizeString(request.Model.Username), cancellationToken))
        throw new ConflictException($"User with username '{request.Model.Username}' already exists.");
      if (await fUserRepository.ExistsByEmailAsync(NormalizeString(request.Model.Email), cancellationToken))
        throw new ConflictException($"User with email '{request.Model.Email}' already exists.");

      var user = await RegisterAsync(request.Model, cancellationToken);
      fEmailService.SendEmailAsync(VerifyAccountEmail.New(user));

      return new IdModel(user.Id);
    }

    public async Task<UserDoc> RegisterAsync(UserPostModel model, CancellationToken cancellationToken)
    {
      var secret = fSecretService.CreateSecret(model.Password);
      var user = new UserDoc
      {
        Username = model.Username,
        NormalizedUsername = NormalizeString(model.Username),
        Email = model.Email,
        NormalizedEmail = NormalizeString(model.Email),
        Secret = secret.ToDoc(),
        PasswordToken = null,
        LogoutUtc = null,
        Disabled = false,
        EmailToken = SecurityUtils.GetUrlToken(),
        UserPermission = new UserPermissionDoc { Kind = _DefaultUserPermission }
      };

      await fUserRepository.InsertAsync(user, cancellationToken);

      return user;
    }

    private static string NormalizeString(string username)
    {
      if (username is null)
        throw new ArgumentNullException(nameof(username));

      return username.ToLower();
    }
  }
}
