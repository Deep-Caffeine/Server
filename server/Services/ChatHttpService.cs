using System.Data.Entity;
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

    public async Task<List<ChatLogResponse>> ChatLog(long roomId)
    {
        // RoomId가 동일한 채팅 기록들을 모두 받아옴
        var chatLogs = this._context.ChatLogsEntities.Where(log => log.RoomId == roomId).ToList();
        List<ChatLogResponse> chatLogResponses = new List<ChatLogResponse>();

        // RoomId가 동일한 채팅 기록이 없는 경우 null을 반환함.
        if (chatLogs == null || !chatLogs.Any()) return null;

        foreach (var chatLog in chatLogs)
        {
            ChatLogResponse response = new ChatLogResponse
            {
                sender = chatLog.Sender,
                message = chatLog.Message,
                datetime = chatLog.DateTime
            };

            chatLogResponses.Add(response);
        }

        return chatLogResponses;
    }

}
