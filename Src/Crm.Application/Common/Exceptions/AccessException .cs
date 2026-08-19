namespace Crm.Application.Common.Exceptions;

public abstract class ApplicationException(string message) : Exception(message);

public sealed class ForbiddenException(string message) : ApplicationException(message);
public sealed class UnauthorizedException(string message) : ApplicationException(message);
public sealed class ConflictException(string message) : ApplicationException(message);
public sealed class NotFoundException( string message) : ApplicationException(message);
