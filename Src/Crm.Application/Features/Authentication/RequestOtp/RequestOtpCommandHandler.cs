using Crm.Application.Common.Exceptions;
using Crm.Application.Contracts.Authentication;
using Crm.Domain.Repositories;
using MediatR;

namespace Crm.Application.Features.Authentication.RequestOtp;

public sealed class RequestOtpCommandHandler(IUserRepository userRepository,IOtpService otpService)
    : IRequestHandler<RequestOtpCommand, RequestOtpResponse>
{
    public async Task<RequestOtpResponse> Handle( RequestOtpCommand request,CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByPhoneAsync( request.Phone, cancellationToken);

        if (user is null)
            throw new NotFoundException("کاربری با این شماره موبایل پیدا نشد.");

        if (!user.IsActive)
            throw new ConflictException("حساب کاربری غیرفعال است.");

        await otpService.GenerateAndStoreAsync( request.Phone,cancellationToken);

        return new RequestOtpResponse( "کد تأیید با موفقیت ارسال شد.");
    }
}