﻿using Microsoft.AspNetCore.Diagnostics;
using DroneApi.Core.Contracts;
using DroneApi.Core.Dtos.ErrorModel;
using DroneApi.Core.Exceptions;
using System.Net;
namespace DroneApi.Web.Middlewares
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (contextFeature != null)
                        {
                            context.Response.StatusCode = contextFeature.Error switch
                            {
                                NotFoundException => StatusCodes.Status404NotFound,
                                BadRequestException => StatusCodes.Status400BadRequest,
                                _ => StatusCodes.Status500InternalServerError
                            };

                            logger.LogError($"Something went wrong: {contextFeature.Error}");

                            await context.Response.WriteAsync(
                                new ErrorDetailsDto()
                                {
                                    StatusCode = context.Response.StatusCode,
                                    ErrorMessage = contextFeature.Error.Message,
                                }.ToString());
                        }
                    });
            });
        }
    }
}
