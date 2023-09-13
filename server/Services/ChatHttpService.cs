using server.DTOs;
using server.Entities;
using server.Interface;

namespace server.Services;

public class ChatHttpService : IChatHttpService
{
    private readonly ApplicationDbContext _context;

    public ChatHttpService(ApplicationDbContext context)
    {
        _context = context;
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
            ChatRoomId = chatJoinRequest.roomid
        };

        this._context.ChatParticipantsEntities.Add(entity);
        await this._context.SaveChangesAsync();

        return true;
    }
    
    public async Task<ChatLogResponse> ChatLog(long id, long roomId)
    {
        var chatLog = await this._context.ChatLogsEntities.FindAsync(id, roomId);
            
        ChatLogResponse chatLogs = new ChatLogResponse
        {
            sender = chatLog.Sender,
            receiver = chatLog.Receiver,
            message = chatLog.Message,
            datatime = chatLog.DateTime
        };
        
        return chatLogs;
    }

}
