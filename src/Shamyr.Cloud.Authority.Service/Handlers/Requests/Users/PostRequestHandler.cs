using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Emails;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Authority.Service.Models;
using Shamyr.Cloud.Authority.Service.Models.Users;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Users;
using Shamyr.Cloud.Authority.Service.Services;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Services;
using Shamyr.ExtensibleLogging;
using Shamyr.Security;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Users
{
  public class PostRequestHandler: IRequestHandler<PostRequest, IdModel>
  {
    private readonly IUserRepository fUserRepository;
    private readonly ISecretService fSecretService;
    private readonly IEmailService fEmailService;
    private readonly IClientRepository fClientRepository;
    private readonly ILoggingContextService fLoggingContextService;

    public PostRequestHandler(
      IUserRepository userRepository,
      ISecretService secretService,
      IEmailService emailService,
      IClientRepository clientRepository,
      ILoggingContextService loggingContextService)
    {
      fUserRepository = userRepository;
      fSecretService = secretService;
      fEmailService = emailService;
      fClientRepository = clientRepository;
      fLoggingContextService = loggingContextService;
    }

    public async Task<IdModel> Handle(PostRequest request, CancellationToken cancellationToken)
    {
      var client = await fClientRepository.GetAsync(request.Model.ClientId, cancellationToken);
      if (client is null)
        throw new BadRequestException($"Client with ID '{request.Model.ClientId}' doesn't exist.");

      if (request.Model.Username != null && await fUserRepository.ExistsByUsernameAsync(request.Model.Username, cancellationToken))
        throw new ConflictException($"User with username '{request.Model.Username}' already exists.");
      if (await fUserRepository.ExistsByEmailAsync(request.Model.Email, cancellationToken))
        throw new ConflictException($"User with email '{request.Model.Email}' already exists.");

      var user = await RegisterAsync(request.Model, client, cancellationToken);

      var context = fLoggingContextService.GetRequestContext();
      await fEmailService.SendEmailAsync(VerifyAccountEmailContext.New(user, client, context), cancellationToken);

      return new IdModel { Id = user.Id };
    }

    public async Task<UserDoc> RegisterAsync(PostModel model, ClientDoc client, CancellationToken cancellationToken)
    {
      var secret = fSecretService.CreateSecret(model.Password);
      var user = new UserDoc
      {
        Username = model.Username,
        NormalizedUsername = model.Username?.CompareNormalize(),
        Email = model.Email,
        NormalizedEmail = model.Email.CompareNormalize(),
        GivenName = model.GivenName,
        FamilyName = model.FamilyName,
        Secret = secret.ToDoc(),
        Verified = !client.RequireEmailVerification,
        EmailToken = SecurityUtils.GetUrlToken()
      };

      await fUserRepository.InsertAsync(user, cancellationToken);
      return user;
    }
  }
}
