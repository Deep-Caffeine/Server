﻿using server.DTOs;
using server.Entities;
using server.Interface;

namespace server.Services;

public class ChatHttpService : IChatHttpService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ChatHttpService> _logger;

    public ChatHttpService(ApplicationDbContext context, ILogger<ChatHttpService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool?> Join(long id, ChatJoinRequest chatJoinRequest)
    {
        ChatRoomEntity? room = await this._context.ChatRoomEntities.FindAsync(chatJoinRequest.roomid);

        if (room == null)
        {
            return null;
        }

        var entity = new ChatParticipantsEntity
        {
            UserId = id,
            ChatRoomId = room.Id
        };

        this._context.ChatParticipantsEntities.Add(entity);
        await this._context.SaveChangesAsync();

        return true;
    }
}
