using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Model.ChatDtos;
using PropertyMgmt.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyMgmt.Application.Features.Conversations.Command.SendMessageCommand;

public record SendMessageCommand(Guid ConversationId, string Content) : IRequest<ChatMessageDto>, IChatRequest;
