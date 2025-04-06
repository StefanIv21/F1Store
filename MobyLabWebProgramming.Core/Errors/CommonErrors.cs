﻿using System.Net;

namespace MobyLabWebProgramming.Core.Errors;

/// <summary>
/// Common error messages that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ErrorMessage FeedbackNotFound => new(HttpStatusCode.NotFound, "Feedback doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage AddressNotFound => new(HttpStatusCode.NotFound, "Address doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage CouldNotSave => new(HttpStatusCode.InternalServerError, "Could not save the entity!", ErrorCodes.TechnicalError);
    public static ErrorMessage BasketItemNotFound => new(HttpStatusCode.NotFound, "BasketItem  doesn't exist!", ErrorCodes.EntityNotFound);
    
    public static ErrorMessage BasketNotFound => new(HttpStatusCode.NotFound, "Basket doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ProducNotFound => new(HttpStatusCode.NotFound, "Product doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage UserNotFound => new(HttpStatusCode.NotFound, "User doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage FileNotFound => new(HttpStatusCode.NotFound, "File not found on disk!", ErrorCodes.PhysicalFileNotFound);
    public static ErrorMessage TechnicalSupport => new(HttpStatusCode.InternalServerError, "An unknown error occurred, contact the technical support!", ErrorCodes.TechnicalError);
}
