namespace Cinebook.Infrastructure.Errors;

public class NotFoundException(string message) : Exception(message);

public class AlreadyExistsException(string message) : Exception(message);

public class LogicErrorException(string message) : Exception(message);
public class InternalServerException(string message) : Exception(message);