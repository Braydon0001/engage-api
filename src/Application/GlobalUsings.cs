﻿global using AutoMapper;
global using AutoMapper.QueryableExtensions;
global using Engage.Application.Behaviours;
global using Engage.Application.CQRS.Commands;
global using Engage.Application.CQRS.Queries;
global using Engage.Application.Exceptions;
global using Engage.Application.Extensions;
global using Engage.Application.Files;
global using Engage.Application.FileUploads;
global using Engage.Application.Interfaces;
global using Engage.Application.Mappings;
global using Engage.Application.Models;
global using Engage.Application.Models.Configuration;
global using Engage.Application.Models.FileStorage;
global using Engage.Application.Pagination;
global using Engage.Application.Pagination.Extensions;
global using Engage.Application.Services.EntityBlobs.Models;
global using Engage.Application.Services.Options.Descriptions;
global using Engage.Application.Services.Options.Models;
global using Engage.Application.Services.Options.Queries;
global using Engage.Application.Services.Shared.Commands;
global using Engage.Application.Services.Shared.Models;
global using Engage.Application.Services.Shared.Queries;
global using Engage.Application.Utils;
global using Engage.Domain.Common;
global using Engage.Domain.Entities;
global using Engage.Domain.Entities.FileEntities;
global using Engage.Domain.Entities.Json;
global using Engage.Domain.Entities.LinkEntities;
global using Engage.Domain.Entities.Views;
global using Engage.Domain.Enums;
global using FluentValidation;
global using MediatR;
global using Microsoft.AspNetCore.Http;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Newtonsoft.Json;
global using System.Collections.Generic;
global using System.IO;
global using System.Linq;
global using System.Linq.Expressions;
global using System.Threading;
global using System.Threading.Tasks;
global using static Engage.Application.Pagination.PaginationUtils;